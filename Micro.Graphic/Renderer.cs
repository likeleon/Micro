using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Micro.Core;
using SlimDX;

namespace Micro.Graphic
{
    public sealed class Renderer
    {
        #region Fields
        private readonly Device device;
        private readonly SpriteRenderer spriteRenderer;
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
            this.spriteRenderer = new SpriteRenderer(device);
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
        #endregion

        #region Private methods
        private bool RenderToRenderTarget(IRenderTarget target, IEnumerable<IRenderable> renderables, IEnumerable<ISprite> sprites, bool doPresent, Camera camera, Light light)
        {
            this.device.RenderTarget = target;

            if (target.ClearBackGround)
                this.device.D3DDevice.Clear(target.ClearOptions, target.ClearColor.ToArgb(), 1.0f, 0);

            Result result = this.device.D3DDevice.BeginScene();
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
                    this.spriteRenderer.Begin();
                    foreach (var sprite in sprites)
                    {
                        sprite.Draw(this.spriteRenderer);
                    }
                    this.spriteRenderer.End();
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
