using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace Micro.Editor.Infrastructure.ViewModels
{
    public abstract class DocumentViewModel : PaneViewModel
    {
        public event EventHandler CloseRequested = delegate { };

        private DelegateCommand closeCommand;

        public ICommand CloseCommand
        {
            get { return this.closeCommand ?? (this.closeCommand = new DelegateCommand(OnClose, OnCanClose)); }
        }

        protected DocumentViewModel(string title, string contentId)
            : base(title, contentId)
        {
        }

        protected abstract bool OnCanClose();

        private void OnClose()
        {
            CloseRequested(this, EventArgs.Empty);
        }
    }
}
