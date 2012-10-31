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
        private readonly IList<IRenderTarget> renderTargets = new List<IRenderTarget>();
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
            PrimaryRenderTarget = new RenderTarget();
        }

        #region Public methods
        public bool Render(IRenderTarget renderTarget, IList<IRenderable> renderables, Camera camera, Light light, bool present)
        {
            if (camera == null || light == null)
                throw new ArgumentNullException("Argument camera or light is null");

            bool renderSuccess = false;
            try
            {
                // Save primary
                PrimaryRenderTarget.TargetSurface = this.device.RawDevice.GetRenderTarget(0);
                PrimaryRenderTarget.DepthStencilSurface = this.device.RawDevice.DepthStencilSurface;

                // Render this render target
                renderSuccess = RenderToRenderTarget(renderTarget, renderables, present, camera, light);
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
        private bool RenderToRenderTarget(IRenderTarget target, IList<IRenderable> renderables, bool doPresent, Camera camera, Light light)
        {
            this.device.RenderTarget = target;

            if (target.ClearBackGround)
                this.device.RawDevice.Clear(target.ClearOptions, target.ClearColor.ToArgb(), 1.0f, 0);

            Result result = this.device.RawDevice.BeginScene();
            if (result.IsFailure)
            {
                Log.Error("BeginScene failed: " + result.ToString());
                return false;
            }

            try
            {
                if (renderables != null && renderables.Count > 0)
                {
                    foreach (var renderable in renderables)
                    {
                        renderable.Render(camera, light);
                    }
                }
            }
            finally
            {
                this.device.RawDevice.EndScene();
            }

            if (doPresent)
                this.device.RawDevice.Present();

            target.OnRender();

            return true;
        }
        #endregion
    }
}
