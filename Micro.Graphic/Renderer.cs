using System;
using System.Collections.Generic;
using Micro.Core;
using Micro.Core.Math;
using D3D = SlimDX.Direct3D9;

namespace Micro.Graphic
{
    public sealed class Renderer
    {
        #region Fields
        private readonly Device device;
        private readonly D3D.Sprite d3dSprite;
        #endregion

        #region Properties
        public bool ManualPresent { get; set; }
        public RenderTarget PrimaryRenderTarget { get; private set; }
        #endregion

        public Renderer(Device device)
        {
            if (device == null)
                throw new ArgumentNullException("device");

            this.device = device;
            this.d3dSprite = new D3D.Sprite(device.D3DDevice);
            PrimaryRenderTarget = new RenderTarget();
        }

        #region Public methods
        public bool Render(IRenderTarget renderTarget, IEnumerable<IRenderable> renderables, IEnumerable<ISprite> sprites, Camera camera, Light light, bool present)
        {
            if (renderables == null && sprites == null)
                return true;

            if (camera == null || light == null)
                throw new ArgumentNullException("Argument camera or light is null");

            bool renderSuccess = false;
            try
            {
                // Save primary
                PrimaryRenderTarget.TargetSurface = this.device.D3DDevice.GetRenderTarget(0);
                PrimaryRenderTarget.DepthStencilSurface = this.device.D3DDevice.DepthStencilSurface;

                // Render this render target
                renderSuccess = RenderToRenderTarget(renderTarget, renderables, sprites, present, camera, light);
            }
            finally
            {
                // Restore primary
                this.device.RenderTarget = PrimaryRenderTarget;
            }

            return renderSuccess;
        }

        public bool BeginDraw()
        {
            return this.d3dSprite.Begin(D3D.SpriteFlags.AlphaBlend).IsSuccess;
        }

        public bool EndDraw()
        {
            return this.d3dSprite.End().IsSuccess;
        }

        public bool Draw(Texture texture, Rectangle sourceRect, Color modulateColor, Matrix4 transformMatrix)
        {
            this.d3dSprite.Transform = transformMatrix.ToD3DMatrix();
            var ret = this.d3dSprite.Draw(texture.RawTexture, sourceRect.ToD3DRectangle(), modulateColor.ToD3DColor4());
            this.d3dSprite.Transform = SlimDX.Matrix.Identity;
            return ret.IsSuccess;
        }

        public bool Draw(string text, TrueTypeFont font, Vector2 position, Color color)
        {
            return (font.D3DFont.DrawString(this.d3dSprite, text, (int)position.x, (int)position.y, color.ToArgb()) != 0);
        }
        #endregion

        #region Private methods
        private bool RenderToRenderTarget(IRenderTarget target, IEnumerable<IRenderable> renderables, IEnumerable<ISprite> sprites, bool doPresent, Camera camera, Light light)
        {
            this.device.RenderTarget = target;

            if (target.ClearBackGround)
                this.device.D3DDevice.Clear(target.ClearOptions, target.ClearColor.ToArgb(), 1.0f, 0);

            SlimDX.Result result = this.device.D3DDevice.BeginScene();
            if (result.IsFailure)
            {
                Log.Error("BeginScene failed: " + result.ToString());
                return false;
            }

            try
            {
                if (renderables != null)
                {
                    foreach (var renderable in renderables)
                    {
                        renderable.Render(camera, light);
                    }
                }

                if (sprites != null)
                {
                    BeginDraw();
                    foreach (var sprite in sprites)
                    {
                        sprite.Draw(this);
                    }
                    EndDraw();
                }
            }
            finally
            {
                this.device.D3DDevice.EndScene();
            }

            if (doPresent)
                this.device.D3DDevice.Present();

            target.OnRender();

            return true;
        }
        #endregion
    }
}
