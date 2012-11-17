using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Micro.Editor.Infrastructure.Services;
using Micro.Editor.Infrastructure.ViewModels;
using Micro.Editor.Modules.AssetBrowser.Models;

namespace Micro.Editor.Modules.AssetBrowser.ViewModels
{
    [Export(typeof(AssetBrowserViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AssetBrowserViewModel : DocumentViewModel
    {
        public static readonly string AssetBrowserContentId = "Asset Browser";

        private ObservableCollection<AssetFolder> assetGroups = new ObservableCollection<AssetFolder>();
        private ReadOnlyObservableCollection<AssetFolder> readonlyAssetGroups;

        public ReadOnlyObservableCollection<AssetFolder> AssetGroups
        {
            get { return this.readonlyAssetGroups ?? (this.readonlyAssetGroups = new ReadOnlyObservableCollection<AssetFolder>(this.assetGroups)); }
        }

        [ImportingConstructor]
        public AssetBrowserViewModel(IFileService fileService)
            : base("Asset Browser", AssetBrowserContentId)
        {
            this.assetGroups.Add(new AssetFolder(fileService, @"C:\Toy\Micro\Micro.Editor"));
        }

        protected override bool OnCanClose()
        {
            return true;
        }
    }
}
