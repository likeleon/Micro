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
            mockFileService.AddFile(path + "/SubFolder1/File1A");
            mockFileService.AddFile(path + "/SubFolder1/File1B");

            mockFileService.AddFolder(path + "/SubFolder2");
            mockFileService.AddFile(path + "/SubFolder2/File2A");
            mockFileService.AddFolder(path + "/SubFolder2/SubSubFolder");

            var folder = new AssetFolder(mockFileService, "AssetFolder Name", path);
            Assert.IsFalse(folder.IsExpanded);

            Assert.AreEqual(mockFileService.GetFullPath(path), folder.FullPath);
            Assert.AreEqual("AssetFolder Name", folder.Name);
            Assert.AreEqual(2, folder.ChildAssetFolders.Count);
            Assert.AreEqual(0, folder.Files.Count);

            var subFolder1 = folder.ChildAssetFolders.Find(f => f.Name == "SubFolder1");
            Assert.AreEqual(0, subFolder1.ChildAssetFolders.Count);
            Assert.AreEqual(2, subFolder1.Files.Count);

            var subFolder2 = folder.ChildAssetFolders.Find(f => f.Name == "SubFolder2");
            Assert.AreEqual(1, subFolder2.ChildAssetFolders.Count);
            Assert.AreEqual(1, subFolder2.Files.Count);
        }
    }
}
