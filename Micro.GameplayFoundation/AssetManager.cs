using System;
using System.Collections.Generic;
using System.IO;
using Micro.Core;
using System.Linq;

namespace Micro.GameplayFoundation.Tests
{
    public sealed class AssetManager
    {
        #region Group class
        public sealed class Group
        {
            public string Name { get; private set; }
            public string RootPath { get; private set; }

            public Group(string name, string rootPath)
            {
                if (!PathUtils.IsAbsolutePath(rootPath))
                {
                    string msg = string.Format("AssetGroup path {0} named {1} is not an absolute path", rootPath, name);
                    throw new ArgumentException(msg);
                }

                Name = name;
                RootPath = rootPath;
            }
        }
        #endregion

        #region IAssetFactory interface
        public interface IAssetFactory
        {
            string Name { get; }
            string AssetType { get; }
            string[] FileExtensions { get; }
        }
        #endregion

        #region AssetFactoryBase
        public abstract class AssetFactoryBase : IAssetFactory
        {
            public string Name { get; private set; }
            public string AssetType { get; private set; }
            public string[] FileExtensions { get; private set; }

            public AssetFactoryBase(string name, string assetType, params string[] fileExtensions)
            {
                Name = name;
                AssetType = assetType;
                FileExtensions = fileExtensions;
            }

            public override string ToString()
            {
                return string.Format("{0}: ({1}) ({2})", Name, AssetType, string.Join(", ", FileExtensions));
            }
        }
        #endregion

        public Dictionary<string, Group> Groups { get; private set; }
        public List<IAssetFactory> AssetFactories { get; private set; }

        public AssetManager()
        {
            Groups = new Dictionary<string, Group>();
            AssetFactories = new List<IAssetFactory>();
        }

        public bool AddGroup(string name, string rootPath)
        {
            try
            {
                if (Groups.ContainsKey(name))
                    throw new ArgumentException("Asset group with same name " + name + " already added", "name");

                Groups.Add(name, new Group(name, rootPath));
                return true;
            }
            catch (Exception e)
            {
                Log.WarnFormat(e, "Failed to add asset group named {0} with path {1}", name, rootPath);
                return false;
            }
        }

        public bool RemoveGroup(string name)
        {
            return Groups.Remove(name);
        }

        public string GetFullPath(string groupName, string assetName)
        {
            if (!Groups.ContainsKey(groupName))
            {
                Log.WarnFormat("Failed to get full path for asset {0}, asset group named {1} not exists", assetName, groupName);
                return null;
            }

            return Path.Combine(Groups[groupName].RootPath, assetName);
        }

        public bool RegisterAssetFactory(IAssetFactory factory)
        {
            if (AssetFactories.Any(f => f.AssetType == factory.AssetType))
            {
                Log.WarnFormat("Factory {0} with same asset type {0} already registered", factory, factory.AssetType);
                return false;
            }

            if (factory.FileExtensions == null || factory.FileExtensions.Length <= 0)
            {
                Log.WarnFormat("Factory {0} has null or zero-length file extensions", factory);
                return false;
            }

            foreach (var registeredFactory in AssetFactories)
            {
                var duplicatedExts = registeredFactory.FileExtensions.Intersect(factory.FileExtensions, StringComparer.CurrentCultureIgnoreCase).ToList();
                if (duplicatedExts.Count > 0)
                {
                    Log.WarnFormat("Factory {0} has duplicated file extensions {1}", factory, string.Join(", ", duplicatedExts));
                    return false;
                }
            }

            AssetFactories.Add(factory);
            return true;
        }

        public bool UnRegisterAssetFactory(IAssetFactory factory)
        {
            if (!AssetFactories.Contains(factory))
            {
                Log.WarnFormat("Factory {0} was not registered", factory.ToString());
                return false;
            }
            return AssetFactories.Remove(factory);
        }
    }
}
