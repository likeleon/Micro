using Micro.Core;
using Micro.Core.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Graphic.Tests
{
    [TestClass]
    public class SpriteRendererTest
    {
        [TestMethod]
        public void SpriteRenderer_Draw_Texture()
        {
            var texture = Texture.Create(TestHelpers.Device, new Size(16, 16));
            TestHelpers.RenderSprite(r => Assert.IsTrue(r.Draw(texture, new Rectangle(0, 0, texture.Width, texture.Height), Color.Red, Matrix4.Identity)));
        }

        [TestMethod()]
        public void SpriteRenderer_Draw_String()
        {
            var font = new TrueTypeFont(TestHelpers.Device, "Arial", 12);
            TestHelpers.RenderSprite(r => Assert.IsTrue(r.Draw("Text", font, 0, 0, Color.Black)));
        }
    }
}
