using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Micro.GameplayFoundation.Tests
{
    [TestClass()]
    public class AssetManagerTest
    {
        [TestMethod()]
        public void AssetManager_AssetGroup()
        {
            var assetManager = new AssetManager();
            Assert.AreEqual(0, assetManager.Groups.Count);

            Assert.IsTrue(assetManager.AddGroup("Test", TestHelpers.TestAssetsPath));
            Assert.AreEqual(TestHelpers.TestAssetsPath, assetManager.Groups["Test"].RootPath);

            Assert.IsTrue(assetManager.AddGroup("TestAlias", TestHelpers.TestAssetsPath), "별칭 허용");
            Assert.AreEqual(TestHelpers.TestAssetsPath, assetManager.Groups["TestAlias"].RootPath);

            Assert.IsFalse(assetManager.AddGroup("Test", TestHelpers.EngineCoreAssetsPath), "이름은 유니크해야 한다");

            Assert.IsFalse(assetManager.AddGroup("RelativePathName", "RelativePath/"), "상대경로는 사용할 수 없다");
            Assert.IsFalse(assetManager.Groups.ContainsKey("RelativePathName"));

            Assert.IsTrue(assetManager.RemoveGroup("Test"));
            Assert.IsFalse(assetManager.RemoveGroup("Test"));
        }

        [TestMethod()]
        public void AssetManager_GetFullPath()
        {
            var assetManager = new AssetManager();
            assetManager.AddGroup("TestGroup", TestHelpers.TestAssetsPath);

            Assert.AreEqual(Path.Combine(TestHelpers.TestAssetsPath, TestHelpers.TestAsset), assetManager.GetFullPath("TestGroup", TestHelpers.TestAsset));
            Assert.IsNull(assetManager.GetFullPath("InvalidGroupName", TestHelpers.TestAsset));
        }

        private class AssetFactoryMock : AssetManager.AssetFactoryBase
        {
            public AssetFactoryMock(string name, string assetType, params string[] fileExtensions)
                : base(name, assetType, fileExtensions)
            {
            }
        }

        [TestMethod()]
        public void AssetManager_AssetFactory()
        {
            var assetManager = new AssetManager();
            var factory1 = new AssetFactoryMock("FactoryName", "Factory1", ".test", ".test2");
            
            Assert.IsTrue(assetManager.RegisterAssetFactory(factory1));
            Assert.IsFalse(assetManager.RegisterAssetFactory(factory1), "중복 등록 불가");
            Assert.IsTrue(assetManager.AssetFactories.Contains(factory1));

            var factory2 = new AssetFactoryMock("FactoryName", factory1.AssetType, ".test3");
            Assert.IsFalse(assetManager.RegisterAssetFactory(factory2), "같은 이름의 AssetType으로 등록 불가");

            var factory3 = new AssetFactoryMock("Factoryname", "Factory3", factory1.FileExtensions[0].ToUpper(), ".test4");
            Assert.IsFalse(assetManager.RegisterAssetFactory(factory3), "같은 확장자가 발견되면 등록 불가, 대소문자 구별 없음");

            var factory4 = new AssetFactoryMock("FactoryName", "Factory4");
            Assert.IsFalse(assetManager.RegisterAssetFactory(factory4), "null이나 빈 확장자 리스트로는 등록 불가");

            Assert.IsTrue(assetManager.UnRegisterAssetFactory(factory1));
            Assert.IsFalse(assetManager.UnRegisterAssetFactory(factory1));
        }
    }
}
