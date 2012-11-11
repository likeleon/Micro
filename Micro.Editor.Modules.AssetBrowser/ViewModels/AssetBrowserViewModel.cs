using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace Micro.Editor.Modules.AssetBrowser.ViewModels
{
    [Export(typeof(AssetBrowserViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AssetBrowserViewModel : PropertyChangedBase
    {
        public string TextContent { get; set; }
    }
}
