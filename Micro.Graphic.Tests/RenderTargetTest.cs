using Micro.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using D3D = SlimDX.Direct3D9;

namespace Micro.Graphic.Tests
{
    [TestClass()]
    public class RenderTargetTest
    {
        [TestMethod()]
        public void RenderTarget_Constructor()
        {
            var rt = new RenderTarget();
            Assert.IsTrue(rt.ClearBackGround);
            Assert.AreEqual(D3D.ClearFlags.Target | D3D.ClearFlags.ZBuffer, rt.ClearOptions);
            Assert.AreEqual(Color.Black, rt.ClearColor);
        }
    }
}
