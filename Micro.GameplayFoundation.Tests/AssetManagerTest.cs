using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Micro.GameplayFoundation.Tests
{
    [TestClass()]
    public class AssetManagerTest
    {
        [TestMethod()]
        public void AssetManager_AssetGroup_Test()
        {
            var assetManager = new AssetManager();
            Assert.AreEqual(0, assetManager.AssetGroups.Count);

            Assert.IsTrue(assetManager.AddAssetGroup("Test", TestHelpers.TestAssetsPath));
            Assert.AreEqual(TestHelpers.TestAssetsPath, assetManager.AssetGroups["Test"].RootPath);

            Assert.IsTrue(assetManager.AddAssetGroup("TestAlias", TestHelpers.TestAssetsPath), "별칭 허용");
            Assert.AreEqual(TestHelpers.TestAssetsPath, assetManager.AssetGroups["TestAlias"].RootPath);

            Assert.IsFalse(assetManager.AddAssetGroup("Test", TestHelpers.EngineCoreAssetsPath), "이름은 유니크해야 한다");

            Assert.IsFalse(assetManager.AddAssetGroup("RelativePathName", "RelativePath/"), "상대경로는 사용할 수 없다");
            Assert.IsFalse(assetManager.AssetGroups.ContainsKey("RelativePathName"));
        }
    }
}
