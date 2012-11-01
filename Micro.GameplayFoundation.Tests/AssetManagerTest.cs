using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;

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

        private class AssetMock : AssetBase
        {
            public AssetMock(string fullPath)
                : base(fullPath)
            { 
            }
        }

        private class AssetMock2 : AssetBase
        {
            public AssetMock2(string fullPath)
                : base(fullPath)
            { 
            }
        }

        private class AssetMock3 : AssetBase
        {
            public AssetMock3(string fullPath)
                : base(fullPath)
            { 
            }
        }

        private class AssetFactoryMock<T> : AssetFactoryBase<T> where T : IAsset 
        {
            public AssetFactoryMock(string name, params string[] fileExtensions)
                : base(name, fileExtensions)
            {
            }

            public override IAsset LoadAsset(string fullPath)
            {
                return (IAsset)Activator.CreateInstance(AssetType, fullPath);
            }
        }

        [TestMethod()]
        public void AssetManager_AssetFactory()
        {
            var assetManager = new AssetManager();
            var factory1 = new AssetFactoryMock<AssetMock>("FactoryName", ".test", ".test2");
            
            Assert.IsTrue(assetManager.RegisterAssetFactory(factory1));
            Assert.IsFalse(assetManager.RegisterAssetFactory(factory1), "중복 등록 불가");
            Assert.IsTrue(assetManager.AssetFactories.Contains(factory1));

            var factory2 = new AssetFactoryMock<AssetMock>("FactoryName", ".test3");
            Assert.IsFalse(assetManager.RegisterAssetFactory(factory2), "같은 에셋 타입으로 등록 불가");

            var factory3 = new AssetFactoryMock<AssetMock2>("Factoryname", factory1.FileExtensions[0].ToUpper(), ".test4");
            Assert.IsFalse(assetManager.RegisterAssetFactory(factory3), "같은 확장자가 발견되면 등록 불가, 대소문자 구별 없음");

            var factory4 = new AssetFactoryMock<AssetMock3>("FactoryName");
            Assert.IsFalse(assetManager.RegisterAssetFactory(factory4), "null이나 빈 확장자 리스트로는 등록 불가");

            Assert.IsTrue(assetManager.UnRegisterAssetFactory(factory1));
            Assert.IsFalse(assetManager.UnRegisterAssetFactory(factory1));
        }

        [TestMethod()]
        public void AssetManager_LoadAsset()
        {
            var assetManager = new AssetManager();
            assetManager.AddGroup("TestGroup", TestHelpers.TestAssetsPath);
            var factory = new AssetFactoryMock<AssetMock>("Factory", Path.GetExtension(TestHelpers.TestAsset));
            assetManager.RegisterAssetFactory(factory);

            var asset = assetManager.LoadAsset("TestGroup", TestHelpers.TestAsset);
            Assert.IsNotNull(asset);
            Assert.AreEqual(factory.AssetType, asset.GetType());

            Assert.AreEqual(asset, assetManager.LoadAsset("TestGroup", TestHelpers.TestAsset.ToUpper()), "동일 인스턴스를 반환, 대소문자 구분도 없다");
            Assert.AreEqual(asset, assetManager.LoadAsset("TestGroup", asset.FullPath), "절대 경로도 유효하다면 OK");

            Assert.IsNull(assetManager.LoadAsset("InvalidGroupName", TestHelpers.TestAsset), "잘못된 그룹 이름");
            Assert.IsNull(assetManager.LoadAsset("TestGroup", Path.ChangeExtension(TestHelpers.TestAsset, ".x")), "등록되지 않은 확장자");
            Assert.IsNull(assetManager.LoadAsset("TestGroup", "NonExistingAsset.file"), "존재하지 않는 에셋");
        }
    }
}
