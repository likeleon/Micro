using Micro.Core;
using Micro.Core.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Graphic.Tests
{
    [TestClass]
    public class SpriteRendererTest
    {
        [TestMethod]
        public void SpriteRenderer_Draw()
        {
            var device = TestHelpers.Device;
            var renderer = new SpriteRenderer(device);
            var texture = Texture.Create(TestHelpers.Device, new Size(16, 16));

            try
            {
                device.BeginScene();
                renderer.Begin();
                Assert.IsTrue(renderer.Draw(texture, new Rectangle(0, 0, texture.Width, texture.Height), Color.Red, Matrix4.Identity));
            }
            finally
            {
                renderer.End();
                device.EndScene();
            }
        }
    }
}
