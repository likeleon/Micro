using Micro.Editor.Infrastructure.Services;
using Micro.Editor.Infrastructure.Tests.Mocks;
using Micro.Editor.Modules.AssetBrowser.ViewModels;
using Micro.GameplayFoundation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace Micro.Editor.Modules.AssetBrowser.Tests.ViewModels
{
    [TestClass]
    public class AssetBrowserViewModelTest
    {
        private MockFileService mockFileService;
        private AssetManager assetManager;

        [TestInitialize]
        public void SetUp()
        {
            this.mockFileService = new MockFileService();
            mockFileService.AddFolder("Folder1/FolderA");
            mockFileService.AddFolder("Folder1/FolderB");
            mockFileService.AddFile("Folder1/FolderB/File1");

            this.assetManager = new AssetManager();
            assetManager.AddGroup("Folder A", mockFileService.GetFullPath("Folder1/FolderA"));
            assetManager.AddGroup("Folder B", mockFileService.GetFullPath("Folder1/FolderB"));
        }

        [TestMethod]
        public void AssetBrowserViewModel_CanInitPresenter()
        {
            var assetBrowser = new AssetBrowserViewModel(mockFileService, assetManager);
            Assert.IsNotNull(assetBrowser);
        }

        [TestMethod]
        public void AssetBrowserViewModel_AssetGroups()
        {
            var assetBrowser = new AssetBrowserViewModel(mockFileService, assetManager);
            Assert.AreEqual(2, assetBrowser.AssetGroups.Count);

            var folderA = assetBrowser.AssetGroups.FirstOrDefault(f => f.FullPath == mockFileService.GetFullPath("Folder1/FolderA"));
            Assert.IsNotNull(folderA);
            Assert.AreEqual("Folder A", folderA.Name);
        }

        [TestMethod]
        public void AssetBrowserViewModel_SelectedAssetFolder()
        {
            var assetBrowser = new AssetBrowserViewModel(mockFileService, assetManager);
            Assert.AreEqual(assetBrowser.AssetGroups[0], assetBrowser.SelectedAssetFolder);
            Assert.AreEqual(0, assetBrowser.FilteredAssetFiles.Count());

            assetBrowser.SelectedAssetFolder = assetBrowser.AssetGroups.FirstOrDefault(f => f.FullPath == mockFileService.GetFullPath("Folder1/FolderB"));
            Assert.AreEqual(1, assetBrowser.FilteredAssetFiles.Count());
            Assert.AreEqual(mockFileService.GetFullPath("Folder1/FolderB/File1"), assetBrowser.FilteredAssetFiles.First().Asset.FullPath);
            Assert.AreEqual("File1", assetBrowser.FilteredAssetFiles.First().Asset.Name);
        }
    }
}
