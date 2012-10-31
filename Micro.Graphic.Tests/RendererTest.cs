using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Graphic.Tests
{
    [TestClass()]
    public class RendererTest
    {
        private Device Device { get; set; }

        [TestInitialize()]
        public void SetUp()
        {
            Device = TestHelpers.GetDevice();
        }

        [TestMethod()]
        public void Renderer_Constructor()
        {
            var rs = new Renderer(Device);
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
            var rs = new Renderer(Device);

            var renderable = TestHelpers.CreateRenderableMock(Device);
            int numRenderCalled = 0;
            renderable.RenderCalled += ((o, e) => ++numRenderCalled);
            var renderables = new List<IRenderable>() { renderable };

            Assert.IsTrue(rs.Render(rs.PrimaryRenderTarget, renderables, new Camera(), new Light(), false));
            Assert.AreEqual(1, numRenderCalled);
            Assert.AreEqual(0.0f, rs.PrimaryRenderTarget.LastFps);
        }

        [TestMethod()]
        public void Renderer_RenderFail()
        {
            var rs = new Renderer(Device);

            Assert.IsTrue(
                TestHelpers.CatchException(typeof(ArgumentNullException),
                () => rs.Render(rs.PrimaryRenderTarget, null, null, new Light(), false)));
            Assert.IsTrue(
                TestHelpers.CatchException(typeof(ArgumentNullException),
                () => rs.Render(rs.PrimaryRenderTarget, null, new Camera(), null, false)));
        }

        class RenderTargetMock : RenderTarget
        {
            public int NumRenderCalled { get; set; }

            protected override void OnRender()
            {
                ++NumRenderCalled;
            }
        }
    }
}
