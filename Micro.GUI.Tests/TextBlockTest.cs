using Micro.Core;
using Micro.Core.Math;
using Micro.Graphic;
using Micro.Graphic.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.GUI.Tests
{
    [TestClass]
    public class TextBlockTest
    {
        [TestMethod]
        public void TextBlock_Constructor()
        {
            var textBlock = new TextBlock();
            Assert.IsNull(textBlock.Font);
            Assert.AreEqual(Color.Black, textBlock.Foreground);
            Assert.AreEqual(string.Empty, textBlock.Text);
            Assert.AreEqual(Vector2.Zero, textBlock.Position);
        }

        [TestMethod()]
        public void TextBlock_Draw_Test()
        {
            var textBlock = new TextBlock()
            {
                Font = TestHelpers.Font,
                Text = "Text will be displayed",
                Foreground = Color.White
            };

            TestHelpers.RenderSprite(s => Assert.IsTrue(textBlock.Draw(s)));
        }

        [TestMethod()]
        public void TextBlock_Draw_Fail_Test()
        {
            var textBlock = new TextBlock();
            TestHelpers.RenderSprite(s => Assert.IsFalse(textBlock.Draw(s), "Should fail if font is null"));

            textBlock.Font = TestHelpers.Font;
            textBlock.Text = null;
            TestHelpers.RenderSprite(s => Assert.IsFalse(textBlock.Draw(s), "Should fail if Text is null"));
        }
    }
}
