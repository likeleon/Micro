using System.ComponentModel.Composition;
using Caliburn.Micro;
using Micro.Editor.Infrastructure.ViewModels;

namespace Micro.Editor.Modules.AssetBrowser.ViewModels
{
    [Export(typeof(AssetBrowserViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AssetBrowserViewModel : DocumentViewModel
    {
        public static readonly string AssetBrowserContentId = "Asset Browser";

        public AssetBrowserViewModel()
            : base("Asset Browser", AssetBrowserContentId)
        {
        }

        public string TextContent { get; set; }
    }
}
