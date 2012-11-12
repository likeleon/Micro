using Micro.Editor.Infrastructure.ViewModels;

namespace Micro.Editor.Infrastructure.Controllers
{
    public interface IDocumentsController
    {
        void AddDocument(DocumentViewModel doc);
        void RemoveDocument(DocumentViewModel doc);
    }
}
