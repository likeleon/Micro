using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.GameplayFoundation.Tests
{
    [TestClass]
    public class IAssetFactoryTest
    {
        private class AssetFactoryMock : AssetFactoryBase
        {
            public AssetFactoryMock(string name, Type assetType, params string[] fileExtensions)
                : base(name, assetType, fileExtensions)
            {
            }

            public override IAsset LoadAsset(string fullPath)
            {
                return null;
            }
        }

        [TestMethod]
        public void AssetFactoryBase_Constructor()
        {
            var factory = new AssetFactoryMock("Factory", typeof(int), ".file");
            Assert.AreEqual("Factory", factory.Name);
            Assert.AreEqual(typeof(int), factory.AssetType);
            Assert.AreEqual(1, factory.FileExtensions.Length);
            Assert.AreEqual(".file", factory.FileExtensions[0]);
        }
    }
}
