﻿using System;
using Micro.Core.Math;
using D3D = SlimDX.Direct3D9;

namespace Micro.Graphic.Tests
{
    public static class TestHelpers
    {
        static TestHelpers()
        {
            Window window = new Window("TestHelpers.Device", 320, 240);
            Device = new Device(window.Handle, window.Width, window.Height);
            Renderer = new Renderer(Device);
            Font = new TrueTypeFont(Device, "Arial", 12);
        }

        public static Device Device { get; private set; }
        public static Renderer Renderer { get; private set; }
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

            public int NumRenderCalled { get; private set; }
        };

        public static RenderableMock CreateRenderable()
        {
            return new RenderableMock();
        }

        // ISprite mock
        public class SpriteMock : ISprite
        {
            public event EventHandler DrawCalled = delegate { };

            bool ISprite.Draw(Renderer renderer)
            {
                DrawCalled(this, EventArgs.Empty);
                ++NumDrawCalled;
                return true;
            }

            public int NumDrawCalled { get; private set; }
        }

        public static SpriteMock CreateSprite()
        {
            return new SpriteMock();
        }

        public static void RenderSprite(Action<Renderer> action)
        {
            try
            {
                Device.BeginScene();
                Renderer.BeginDraw();

                action(Renderer);
            }
            finally
            {
                Renderer.EndDraw();
                Device.EndScene();
            }
        }
    }
}
