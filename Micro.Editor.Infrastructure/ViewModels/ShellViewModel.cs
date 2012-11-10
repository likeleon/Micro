using System.Collections.ObjectModel;
using Caliburn.Micro;

namespace Micro.Editor.Infrastructure.ViewModels
{
    public sealed class ShellViewModel : PropertyChangedBase
    {
        private BindableCollection<DocumentViewModel> documents = new BindableCollection<DocumentViewModel>();
        private ReadOnlyObservableCollection<DocumentViewModel> readonlyDocuments;
        public ReadOnlyObservableCollection<DocumentViewModel> Documents
        {
            get { return this.readonlyDocuments ?? (this.readonlyDocuments = new ReadOnlyObservableCollection<DocumentViewModel>(this.documents)); }
        }
    }
}
