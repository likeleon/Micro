using System.Collections.Generic;
using System.Linq;
using Micro.Editor.Infrastructure.Services;

namespace Micro.Editor.Modules.AssetBrowser.Models
{
    public sealed class AssetFolder
    {
        public string Name { get; private set; }
        public string FullPath { get; private set; }
        public List<AssetFolder> ChildAssetFolders { get; private set; }
        public bool IsExpanded { get; set; }
        public bool IsSelected { get; set; }

        public AssetFolder(IFileService fileService, string path)
        {
            FullPath = fileService.GetFullPath(path);
            Name = fileService.GetFileName(path);

            ChildAssetFolders = fileService.GetDirectories(FullPath)
                .Select(d => new AssetFolder(fileService, d))
                .ToList();
        }
    }
}
