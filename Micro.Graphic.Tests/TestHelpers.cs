using System;
using Micro.Core.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Graphic.Tests
{
    internal class TestHelpers
    {
        internal static Device device;

        internal static Device GetDevice()
        {
            if (device == null)
            {
                Window window = new Window("TestHelpers.GetDevice", 640, 480);
                device = new Device(window.Handle, window.Width, window.Height);
            }
            return device;
        }

        internal static bool CatchException(Type targetException, Action action)
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
        internal class RenderableMock : IRenderable
        {
            public event EventHandler RenderCalled = delegate { };

            bool IRenderable.Render(Camera camera, Light light)
            {
                RenderCalled(this, EventArgs.Empty);
                return true;
            }

            public Matrix4 WorldMatrix { get { return Matrix4.Identity; } }
        };

        internal static RenderableMock CreateRenderableMock(Device device)
        {
            return new RenderableMock();
        }
    }
}
