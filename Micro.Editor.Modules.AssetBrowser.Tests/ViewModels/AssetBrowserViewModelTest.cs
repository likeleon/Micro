using Micro.Editor.Infrastructure.Services;
using Micro.Editor.Modules.AssetBrowser.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Micro.Editor.Modules.AssetBrowser.Tests.ViewModels
{
    [TestClass]
    public class AssetBrowserViewModelTest
    {
        [TestMethod]
        public void AssetBrowserViewModel_CanInitPresenter()
        {
            var viewModel = new AssetBrowserViewModel(new Mock<IFileService>().Object);
            Assert.IsNotNull(viewModel);
        }
    }
}
