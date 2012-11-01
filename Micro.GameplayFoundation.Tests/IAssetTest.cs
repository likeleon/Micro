using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.GameplayFoundation.Tests
{
    [TestClass]
    public class IAssetTest
    {
        private class AssetBaseMock : AssetBase
        {
            public AssetBaseMock(string fullPath)
                : base(fullPath)
            {
            }
        }

        [TestMethod]
        public void AssetBase_Constructor()
        {
            string assetPath = "AssetFile.file";
            var asset = new AssetBaseMock(Path.GetFullPath(assetPath));
            Assert.AreEqual(Path.GetFullPath(assetPath), asset.FullPath);
            Assert.AreEqual("AssetFile.file", asset.Name);
        }
    }
}
