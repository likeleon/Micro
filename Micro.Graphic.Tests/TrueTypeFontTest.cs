using Micro.Core;
using Micro.Core.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Graphic.Tests
{
    [TestClass]
    public class TrueTypeFontTest
    {
        [TestMethod]
        public void TrueTypeFont_Constructor()
        {
            Assert.IsNotNull(new TrueTypeFont(TestHelpers.Device, "Arial", 10));
            Assert.IsNotNull(new TrueTypeFont(TestHelpers.Device, "NonExistingFaceName", 10));
        }
    }
}
