
namespace Micro.Editor.Infrastructure.ViewModels
{
    public abstract class DocumentViewModel : PaneViewModel
    {
        protected DocumentViewModel(string title, string contentId)
            : base(title, contentId)
        {
        }
    }
}
