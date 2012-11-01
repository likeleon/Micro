using System.IO;
using Micro.Core;
using Micro.Graphic;
using Micro.Graphic.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Graphics.Tests
{
    [TestClass()]
    public class TextureTest
    {
        [TestMethod()]
        public void Texture_LoadFromFileAndStream()
        {
            var expected = Texture.Create(TestHelpers.Device, new Size(8, 8));
            Assert.IsNotNull(expected.RawTexture);

            var stream = new MemoryStream();
            expected.ToStream().CopyTo(stream);
            stream.Position = 0;

            var actual = Texture.LoadFromStream(TestHelpers.Device, stream);
            Assert.AreEqual(expected.Width, actual.Width);
        }

        [TestMethod()]
        public void Texture_SaveToFile_Test()
        {
            var texture = Texture.Create(TestHelpers.Device, new Size(8, 8));
            Assert.IsTrue(texture.SaveToFile("saved.jpg"));
            Assert.IsTrue(File.Exists("saved.jpg"));
        }
    }
}
