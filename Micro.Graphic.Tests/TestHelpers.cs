using System;
using Micro.Core.Math;

namespace Micro.Graphic.Tests
{
    public static class TestHelpers
    {
        static TestHelpers()
        {
            Window window = new Window("TestHelpers.Device", 320, 240);
            Device = new Device(window.Handle, window.Width, window.Height);
            SpriteRenderer = new SpriteRenderer(Device);
            Font = new TrueTypeFont(Device, "Arial", 12);
        }

        public static Device Device { get; private set; }
        public static SpriteRenderer SpriteRenderer { get; private set; }
        public static TrueTypeFont Font { get; private set; }

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

        public static void RenderSprite(Action<SpriteRenderer> action)
        {
            try
            {
                Device.BeginScene();
                SpriteRenderer.Begin();

                action(SpriteRenderer);
            }
            finally
            {
                SpriteRenderer.End();
                Device.EndScene();
            }
        }
    }
}
