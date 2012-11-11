using System.ComponentModel.Composition;
using Micro.Editor.Infrastructure.Models;
using Micro.Editor.Infrastructure.Services;
using Micro.Editor.Modules.AssetBrowser.Views;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace Micro.Editor.Modules.AssetBrowser
{
    [ModuleExport(typeof(AssetBrowserModule))]
    public sealed class AssetBrowserModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IMenuService menuService;

        [ImportingConstructor]
        public AssetBrowserModule(IRegionManager regionManager, IMenuService menuService)
        {
            this.regionManager = regionManager;
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
            IRegion region = this.regionManager.Regions[RegionNames.AvalonDocumentRegion];
            
            var assetBrowserView = ServiceLocator.Current.GetInstance<AssetBrowserView>();
            if (!region.Views.Contains(assetBrowserView))
                region.Add(assetBrowserView);

            region.Activate(assetBrowserView);
        }
    }
}
