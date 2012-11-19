using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Micro.Core;

namespace Micro.GameplayFoundation
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

        public Dictionary<string, Group> Groups { get; private set; }
        public List<IAssetFactory> AssetFactories { get; private set; }
        private readonly Dictionary<string, IAsset> assets;

        public AssetManager()
        {
            Groups = new Dictionary<string, Group>(StringComparer.CurrentCultureIgnoreCase);
            AssetFactories = new List<IAssetFactory>();
            assets = new Dictionary<string, IAsset>(StringComparer.CurrentCultureIgnoreCase);
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

        public string GetFullPath(string groupName, string assetPath)
        {
            if (!Groups.ContainsKey(groupName))
            {
                Log.WarnFormat("Failed to get full path for asset {0}, asset group named {1} not exists", assetPath, groupName);
                return null;
            }

            return Path.Combine(Groups[groupName].RootPath, assetPath);
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

        public IAsset LoadAsset(string groupName, string assetPath)
        {
            string fullPath = GetFullPath(groupName, assetPath);
            if (string.IsNullOrWhiteSpace(fullPath))
            {
                Log.WarnFormat("LoadAsset with group name {0} and asset {1} failed, empty full path returned", groupName, assetPath);
                return null;
            }
            return LoadAsset(fullPath);
        }

        public IAsset LoadAsset(string fullPath)
        {
            if (this.assets.ContainsKey(fullPath))
                return this.assets[fullPath];

            string fileExtension = Path.GetExtension(fullPath);
            var factory = AssetFactories.Find(f => f.FileExtensions.Contains(fileExtension, StringComparer.CurrentCultureIgnoreCase));
            if (factory == null)
            {
                Log.WarnFormat("LoadAsset {0} failed, unknown file extension {1}", fullPath, fileExtension);
                return null;
            }

            if (!File.Exists(fullPath))
            {
                Log.WarnFormat("LoadAsset {0} failed, file not exists", fullPath, fullPath);
                return null;
            }

            this.assets[fullPath] = factory.LoadAsset(fullPath);
            return this.assets[fullPath];
        }
    }
}
