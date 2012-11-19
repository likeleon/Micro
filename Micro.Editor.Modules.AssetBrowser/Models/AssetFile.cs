using Micro.GameplayFoundation;

namespace Micro.Editor.Modules.AssetBrowser.Models
{
    public sealed class AssetFile
    {
        public IAsset Asset { get; private set; }

        public AssetFile(IAsset asset)
        {
            Asset = asset;
        }
    }
}
