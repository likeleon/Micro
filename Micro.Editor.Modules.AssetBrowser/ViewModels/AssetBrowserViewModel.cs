using System.ComponentModel.Composition;
using Micro.Editor.Infrastructure.Controllers;
using Micro.Editor.Infrastructure.ViewModels;

namespace Micro.Editor.Modules.AssetBrowser.ViewModels
{
    [Export(typeof(AssetBrowserViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AssetBrowserViewModel : DocumentViewModel
    {
        public static readonly string AssetBrowserContentId = "Asset Browser";

        [ImportingConstructor]
        public AssetBrowserViewModel()
            : base("Asset Browser", AssetBrowserContentId)
        {
        }

        public string TextContent { get; set; }

        protected override bool OnCanClose()
        {
            return true;
        }
    }
}
