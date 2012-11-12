using System.ComponentModel.Composition;
using System.Windows;

namespace Micro.Editor.Modules.AssetBrowser.Resources
{
    [Export("ApplicationResources", typeof(ResourceDictionary))]
    public partial class ApplicationResources : ResourceDictionary
    {
        public ApplicationResources()
        {
            InitializeComponent();
        }
    }
}
