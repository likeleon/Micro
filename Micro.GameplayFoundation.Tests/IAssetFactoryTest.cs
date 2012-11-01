using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.GameplayFoundation.Tests
{
    [TestClass]
    public class IAssetFactoryTest
    {
        private class AssetMock : AssetBase
        {
            public AssetMock(string fullPath)
                : base(fullPath)
            {
            }
        }

        private class AssetFactoryMock : AssetFactoryBase<AssetMock>
        {
            public AssetFactoryMock(string name, params string[] fileExtensions)
                : base(name, fileExtensions)
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
            var factory = new AssetFactoryMock("Factory", ".file");
            Assert.AreEqual("Factory", factory.Name);
            Assert.AreEqual(typeof(AssetMock), factory.AssetType);
            Assert.AreEqual(1, factory.FileExtensions.Length);
            Assert.AreEqual(".file", factory.FileExtensions[0]);
        }
    }
}
