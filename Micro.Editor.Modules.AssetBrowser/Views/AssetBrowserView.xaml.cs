using System.ComponentModel.Composition;
using System.Windows.Controls;
using Micro.Editor.Infrastructure;
using Micro.Editor.Infrastructure.Models;
using Micro.Editor.Modules.AssetBrowser.ViewModels;

namespace Micro.Editor.Modules.AssetBrowser.Views
{
    [ViewExport(RegionName = RegionNames.AvalonDocumentRegion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AssetBrowserView : UserControl
    {
        public AssetBrowserView()
        {
            InitializeComponent();
        }

        [Import]
        public AssetBrowserViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}
