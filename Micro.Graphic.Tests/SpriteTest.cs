using Micro.Core;
using Micro.Core.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Graphic.Tests
{
    [TestClass()]
    public class SpriteTest
    {
        private Sprite CreateSprite()
        {
            return new Sprite(Texture.Create(TestHelpers.Device, new Size(16, 16)));
        }

        [TestMethod()]
        public void Sprite_Constructor()
        {
            var texture = Texture.Create(TestHelpers.Device, new Size(16, 16));
            var sprite = new Sprite(texture);
            Assert.AreEqual(texture, sprite.Texture);
            Assert.AreEqual(Color.White, sprite.ModulateColor);
        }

        [TestMethod()]
        public void Sprite_Render_Test()
        {
            var sprite = CreateSprite();
            TestHelpers.RenderSprite(s => Assert.IsTrue(sprite.Draw(s)));
        }

        [TestMethod()]
        public void Sprite_Transform_Test()
        {
            var sprite = CreateSprite();

            Assert.AreEqual(Vector2.Zero, sprite.Position);
            var newPos = new Vector2(1.0f, 2.0f);
            sprite.Position = newPos;
            Assert.AreEqual(newPos, sprite.Position);

            Assert.AreEqual(new Vector2(1.0f, 1.0f), sprite.Scale);
            var newScale = new Vector2(0.5f, 2.0f);
            sprite.Scale = newScale;
            Assert.AreEqual(newScale, sprite.Scale);

            Assert.AreEqual(new Radian(0.0f), sprite.Rotation);
            var newRotation = new Radian(MathUtils.PI);
            sprite.Rotation = newRotation;
            Assert.AreEqual(newRotation, sprite.Rotation);
        }
    }
}
