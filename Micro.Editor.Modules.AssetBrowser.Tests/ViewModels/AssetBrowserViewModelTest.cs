using Micro.Editor.Modules.AssetBrowser.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Editor.Modules.AssetBrowser.Tests.ViewModels
{
    [TestClass]
    public class AssetBrowserViewModelTest
    {
        [TestMethod]
        public void AssetBrowserViewModel_CanInitPresenter()
        {
            var viewModel = new AssetBrowserViewModel();
            Assert.IsNotNull(viewModel);
        }
    }
}
