using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Micro.Editor.Infrastructure.Services;
using Micro.Editor.Infrastructure.ViewModels;
using Micro.Editor.Modules.AssetBrowser.Models;
using Micro.GameplayFoundation;

namespace Micro.Editor.Modules.AssetBrowser.ViewModels
{
    [Export(typeof(AssetBrowserViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AssetBrowserViewModel : DocumentViewModel
    {
        #region UnknownAsset
        public sealed class UnknownAsset : IAsset
        {
            public string Name { get; set; }
            public string FullPath { get; set; }
        }
        #endregion

        #region Fields
        public static readonly string AssetBrowserContentId = "Asset Browser";

        private ObservableCollection<AssetFolder> assetGroups = new ObservableCollection<AssetFolder>();
        private ReadOnlyObservableCollection<AssetFolder> readonlyAssetGroups;
        private AssetFolder selectedAssetFolder;
        private readonly AssetManager assetManager;
        private readonly IFileService fileService;
        #endregion

        #region Properties
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
                    RaisePropertyChanged(() => FilteredAssetFiles);
                }
            }
        }

        public IEnumerable<AssetFile> FilteredAssetFiles
        {
            get
            {
                List<AssetFile> assetFiles = new List<AssetFile>();

                Action<AssetFolder, List<AssetFile>> addFilesAction = null;
                addFilesAction = (folder, resultFiles) =>
                {
                    foreach (string file in folder.Files)
                    {
                        var asset = this.assetManager.LoadAsset(file);
                        if (asset == null)
                        {
                            asset = new UnknownAsset()
                            {
                                Name = this.fileService.GetFileName(file),
                                FullPath = file
                            };
                        }

                        resultFiles.Add(new AssetFile(asset));
                    }
                    
                    foreach (var childFolder in folder.ChildAssetFolders)
                    {
                        addFilesAction(childFolder, resultFiles);
                    }
                };

                if (SelectedAssetFolder != null)
                {
                    addFilesAction(SelectedAssetFolder, assetFiles);
                }

                return assetFiles;
            }
        }
        #endregion

        [ImportingConstructor]
        public AssetBrowserViewModel(IFileService fileService, [Import("AssetManager")]AssetManager assetManager)
            : base("Asset Browser", AssetBrowserContentId)
        {
            this.fileService = fileService;
            this.assetManager = assetManager;

            foreach (var assetGroup in assetManager.Groups)
            {
                var groupFolder = new AssetFolder(fileService, assetGroup.Value.Name, assetGroup.Value.RootPath);
                this.assetGroups.Add(groupFolder);
            }

            if (this.assetGroups.Count > 0)
            {
                this.assetGroups[0].IsExpanded = true;
                this.assetGroups[0].IsSelected = true;
                SelectedAssetFolder = this.assetGroups[0];
            }
        }

        protected override bool OnCanClose()
        {
            return true;
        }
    }
}
