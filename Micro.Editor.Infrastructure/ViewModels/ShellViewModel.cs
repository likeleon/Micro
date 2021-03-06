﻿using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using System.Windows.Input;
using Micro.Editor.Infrastructure.Models;
using Micro.GameplayFoundation;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace Micro.Editor.Infrastructure.ViewModels
{
    [Export]
    public sealed class ShellViewModel : NotificationObject
    {
        private AssetManager assetManager = new AssetManager();

        [ImportingConstructor]
        public ShellViewModel(CompositionContainer container)
        {
            this.assetManager.AddGroup("Engine Core", @"C:\Toy\Micro\Assets\EngineCore");
            this.assetManager.AddGroup("Test", @"C:\Toy\Micro\Assets\Test");

            container.ComposeExportedValue("AssetManager", this.assetManager);
        }

        private DelegateCommand exitCommand;
        private ObservableCollection<MenuItem> viewCommands = new ObservableCollection<MenuItem>();
        private ReadOnlyObservableCollection<MenuItem> readonlyViewCommands;
        
        public ICommand ExitCommand
        {
            get { return this.exitCommand ?? (this.exitCommand = new DelegateCommand(OnExit)); }
        }

        public ReadOnlyObservableCollection<MenuItem> ViewCommands
        {
            get { return this.readonlyViewCommands ?? (this.readonlyViewCommands = new ReadOnlyObservableCollection<MenuItem>(viewCommands)); }
        }

        public void AddViewMenu(MenuItem item)
        {
            if (!this.viewCommands.Contains(item))
                this.viewCommands.Add(item);
        }

        private void OnExit()
        {
            Application.Current.Shutdown();
        }
    }
}
