using System;
using Micro.Core.Math;

namespace Micro.Graphic.Tests
{
    public class TestHelpers
    {
        private static Device device;

        public static Device Device
        {
            get
            {
                if (device == null)
                {
                    Window window = new Window("TestHelpers.GetDevice", 640, 480);
                    device = new Device(window.Handle, window.Width, window.Height);
                }
                return device;
            }
        }

        public static bool CatchException(Type targetException, Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                if (e.GetType() == targetException)
                    return true;
            }

            return false;
        }

        // IRenderable mock
        public class RenderableMock : IRenderable
        {
            public event EventHandler RenderCalled = delegate { };

            bool IRenderable.Render(Camera camera, Light light)
            {
                RenderCalled(this, EventArgs.Empty);
                ++NumRenderCalled;
                return true;
            }

            public Matrix4 WorldMatrix { get { return Matrix4.Identity; } }
            public int NumRenderCalled { get; private set; }
        };

        public static RenderableMock CreateRenderableMock(Device device)
        {
            return new RenderableMock();
        }
    }
}
