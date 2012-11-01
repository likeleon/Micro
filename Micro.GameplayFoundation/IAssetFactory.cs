using System;

namespace Micro.GameplayFoundation
{
    public interface IAssetFactory
    {
        string Name { get; }
        Type AssetType { get; }
        string[] FileExtensions { get; }

        IAsset LoadAsset(string fullPath);
    }

    public abstract class AssetFactoryBase : IAssetFactory
    {
        public string Name { get; private set; }
        public Type AssetType { get; private set; }
        public string[] FileExtensions { get; private set; }

        public AssetFactoryBase(string name, Type assetType, params string[] fileExtensions)
        {
            Name = name;
            AssetType = assetType;
            FileExtensions = fileExtensions;
        }

        public abstract IAsset LoadAsset(string fullPath);

        public override string ToString()
        {
            return string.Format("{0}: ({1}) ({2})", Name, AssetType, string.Join(", ", FileExtensions));
        }
    }
}
