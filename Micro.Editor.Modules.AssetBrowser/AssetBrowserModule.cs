using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

namespace Micro.Editor.Modules.AssetBrowser
{
    [ModuleExport(typeof(AssetBrowserModule))]
    public sealed class AssetBrowserModule : IModule
    {
        public void Initialize()
        {
        }
    }
}
