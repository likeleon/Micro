using System;
using System.Collections.Generic;
using Micro.Core;
using Micro.Core.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Graphic.Tests
{
    [TestClass()]
    public class RendererTest
    {
        [TestMethod()]
        public void Renderer_Constructor()
        {
            var rs = new Renderer(TestHelpers.Device);
            Assert.IsNotNull(rs.PrimaryRenderTarget);
        }

        [TestMethod()]
        public void Renderer_ConstructorFail()
        {
            Assert.IsTrue(TestHelpers.CatchException(typeof(ArgumentNullException), () => new Renderer(null)));
        }

        [TestMethod()]
        public void Renderer_Render()
        {
            var rs = new Renderer(TestHelpers.Device);

            var renderable = TestHelpers.CreateRenderable();
            var renderables = new List<IRenderable>() { renderable };

            var sprite = TestHelpers.CreateSprite();
            var sprites = new List<ISprite>() { sprite };

            Assert.IsTrue(rs.Render(rs.PrimaryRenderTarget, renderables, sprites, new Camera(), new Light(), false));
            Assert.AreEqual(1, renderable.NumRenderCalled);
            Assert.AreEqual(1, sprite.NumDrawCalled);
            Assert.AreEqual(0.0f, rs.PrimaryRenderTarget.LastFps);
        }

        [TestMethod()]
        public void Renderer_RenderFail()
        {
            var rs = new Renderer(TestHelpers.Device);
            var renderables = new List<IRenderable>();
            var sprites = new List<ISprite>();
            var camera = new Camera();
            var light = new Light();

            Assert.IsTrue(rs.Render(rs.PrimaryRenderTarget, null, null, camera, light, false));
            Assert.IsTrue(rs.Render(rs.PrimaryRenderTarget, renderables, null, camera, light, false));
            Assert.IsTrue(rs.Render(rs.PrimaryRenderTarget, null, sprites, camera, light, false));

            Assert.IsTrue(
                TestHelpers.CatchException(typeof(ArgumentNullException),
                () => rs.Render(rs.PrimaryRenderTarget, renderables, sprites, null, light, false)));
            Assert.IsTrue(
                TestHelpers.CatchException(typeof(ArgumentNullException),
                () => rs.Render(rs.PrimaryRenderTarget, renderables, sprites, camera, null, false)));
        }

        class RenderTargetMock : RenderTarget
        {
            public int NumRenderCalled { get; set; }

            protected override void OnRender()
            {
                ++NumRenderCalled;
            }
        }
    
        [TestMethod()]
        public void Renderer_DrawString()
        {
            TestHelpers.RenderSprite(r => r.Draw("Tesxt", TestHelpers.Font, Vector2.Zero, Color.Red));
        }
    }
}
