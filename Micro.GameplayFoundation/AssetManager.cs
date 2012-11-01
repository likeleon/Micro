using System;
using System.Collections.Generic;
using Micro.Core;

namespace Micro.GameplayFoundation.Tests
{
    public sealed class AssetManager
    {
        public sealed class AssetGroup
        {
            public string Name { get; private set; }
            public string RootPath { get; private set; }

            public AssetGroup(string name, string rootPath)
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

        public Dictionary<string, AssetGroup> AssetGroups { get; private set; }

        public AssetManager()
        {
            AssetGroups = new Dictionary<string, AssetGroup>();
        }

        public bool AddAssetGroup(string name, string rootPath)
        {
            try
            {
                if (AssetGroups.ContainsKey(name))
                    throw new ArgumentException("Asset group with same name " + name + " already added", "name");

                AssetGroups.Add(name, new AssetGroup(name, rootPath));
                return true;
            }
            catch (Exception e)
            {
                Log.WarnFormat(e, "Failed to add asset group named {0} with path {1}", name, rootPath);
                return false;
            }
        }
    }
}
