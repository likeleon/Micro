using System.ComponentModel.Composition;
using Micro.Editor.Infrastructure.Controllers;
using Micro.Editor.Infrastructure.Models;
using Micro.Editor.Infrastructure.Services;
using Micro.Editor.Modules.AssetBrowser.ViewModels;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;

namespace Micro.Editor.Modules.AssetBrowser
{
    [ModuleExport(typeof(AssetBrowserModule))]
    public sealed class AssetBrowserModule : IModule
    {
        private readonly IDocumentsController documentsController;
        private readonly IMenuService menuService;

        [ImportingConstructor]
        public AssetBrowserModule(IDocumentsController documentsController, IMenuService menuService)
        {
            this.documentsController = documentsController;
            this.menuService = menuService;
        }

        public void Initialize()
        {
            var showAssetBrowserMenu = new MenuItem()
            {
                Name = "_Asset Browser",
                Command = new DelegateCommand(OnShowAssetBrowser)
            };
            menuService.AddViewMenu(showAssetBrowserMenu);
        }

        private void OnShowAssetBrowser()
        {
            var assetBrowser = ServiceLocator.Current.GetInstance<AssetBrowserViewModel>();
            this.documentsController.AddDocument(assetBrowser);
        }
    }
}
