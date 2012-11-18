using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Micro.Editor.Infrastructure.Services;
using Micro.Editor.Infrastructure.ViewModels;
using Micro.Editor.Modules.AssetBrowser.Models;

namespace Micro.Editor.Modules.AssetBrowser.ViewModels
{
    [Export(typeof(AssetBrowserViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AssetBrowserViewModel : DocumentViewModel
    {
        public static readonly string AssetBrowserContentId = "Asset Browser";

        private ObservableCollection<AssetFolder> assetGroups = new ObservableCollection<AssetFolder>();
        private ReadOnlyObservableCollection<AssetFolder> readonlyAssetGroups;
        private AssetFolder selectedAssetFolder;

        public ReadOnlyObservableCollection<AssetFolder> AssetGroups
        {
            get { return this.readonlyAssetGroups ?? (this.readonlyAssetGroups = new ReadOnlyObservableCollection<AssetFolder>(this.assetGroups)); }
        }

        public AssetFolder SelectedAssetFolder
        {
            get { return this.selectedAssetFolder; }
            set
            {
                if (this.selectedAssetFolder != value)
                {
                    this.selectedAssetFolder = value;
                    RaisePropertyChanged(() => FilteredFiles);
                }
            }
        }

        public IEnumerable<string> FilteredFiles
        {
            get
            {
                List<string> files = new List<string>();

                Action<AssetFolder, List<string>> addFilesAction = null;
                addFilesAction = (folder, resultFiles) =>
                {
                    resultFiles.AddRange(folder.Files);
                    foreach (var childFolder in folder.ChildAssetFolders)
                    {
                        addFilesAction(childFolder, resultFiles);
                    }
                };

                if (SelectedAssetFolder != null)
                {
                    addFilesAction(SelectedAssetFolder, files);
                }

                return files;
            }
        }

        [ImportingConstructor]
        public AssetBrowserViewModel(IFileService fileService)
            : base("Asset Browser", AssetBrowserContentId)
        {
            this.assetGroups.Add(new AssetFolder(fileService, @"C:\Toy\Micro\Micro.Editor"));
            this.assetGroups[0].IsExpanded = true;
            this.assetGroups[0].IsSelected = true;
            SelectedAssetFolder = this.assetGroups[0];
        }

        protected override bool OnCanClose()
        {
            return true;
        }
    }
}
