using System;
using Micro.Editor.Infrastructure.Tests.Mocks;
using Micro.Editor.Modules.AssetBrowser.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Editor.Modules.AssetBrowser.Tests.Models
{
    [TestClass]
    public class AssetFolderTest
    {
        [TestMethod]
        public void AssetFolder_Constructor()
        {
            string path = "TestFolder/AssetFolder";
            
            var mockFileService = new MockFileService();
            mockFileService.AddFolder(path + "/SubFolder1");
            mockFileService.AddFolder(path + "/SubFolder2");
            mockFileService.AddFolder(path + "/SubFolder2/SubSubFolder");

            var folder = new AssetFolder(mockFileService, path);
            Assert.IsFalse(folder.IsExpanded);

            Assert.AreEqual(mockFileService.GetFullPath(path), folder.FullPath);
            Assert.AreEqual("AssetFolder", folder.Name);
            Assert.AreEqual(2, folder.ChildAssetFolders.Count);
            Assert.AreEqual(0, folder.ChildAssetFolders.Find(f => f.Name == "SubFolder1").ChildAssetFolders.Count);
            Assert.AreEqual(1, folder.ChildAssetFolders.Find(f => f.Name == "SubFolder2").ChildAssetFolders.Count);
        }
    }
}
