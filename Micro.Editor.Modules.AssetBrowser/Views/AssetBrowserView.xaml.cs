using System.ComponentModel.Composition;
using System.Windows.Controls;
using Micro.Editor.Infrastructure;
using Micro.Editor.Infrastructure.Models;
using Micro.Editor.Modules.AssetBrowser.ViewModels;

namespace Micro.Editor.Modules.AssetBrowser.Views
{
    [Export(typeof(AssetBrowserView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
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
