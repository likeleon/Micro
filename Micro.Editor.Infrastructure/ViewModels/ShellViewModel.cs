using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using Micro.Editor.Infrastructure.Models;
using Microsoft.Practices.Prism.Commands;

namespace Micro.Editor.Infrastructure.ViewModels
{
    [Export]
    public sealed class ShellViewModel : PropertyChangedBase
    {
        private DelegateCommand exitCommand;
        private BindableCollection<MenuItem> viewCommands = new BindableCollection<MenuItem>();
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
