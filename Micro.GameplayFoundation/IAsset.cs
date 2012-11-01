using System.IO;

namespace Micro.GameplayFoundation
{
    public interface IAsset
    {
        string Name { get; }
        string FullPath { get; }
    }

    public abstract class AssetBase : IAsset
    {
        public string Name { get; private set; }
        public string FullPath { get; private set; }

        protected AssetBase(string fullPath)
        {
            FullPath = fullPath;
            Name = Path.GetFileName(FullPath);
        }
    }
}
