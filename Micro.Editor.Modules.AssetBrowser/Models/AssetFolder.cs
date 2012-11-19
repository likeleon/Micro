using System.Collections.Generic;
using System.Linq;
using Micro.Editor.Infrastructure.Services;

namespace Micro.Editor.Modules.AssetBrowser.Models
{
    public class AssetFolder
    {
        public string Name { get; private set; }
        public string FullPath { get; private set; }
        public List<AssetFolder> ChildAssetFolders { get; private set; }
        public bool IsExpanded { get; set; }
        public bool IsSelected { get; set; }
        public List<string> Files { get; private set; }

        public AssetFolder(IFileService fileService, string name, string path)
        {
            Name = name;
            FullPath = fileService.GetFullPath(path);

            ChildAssetFolders = fileService.GetDirectories(FullPath)
                .Select(d => new AssetFolder(fileService, fileService.GetFileName(d), d))
                .ToList();

            Files = fileService.GetFiles(FullPath).ToList();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
