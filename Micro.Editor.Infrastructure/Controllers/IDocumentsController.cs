using Micro.Editor.Infrastructure.ViewModels;

namespace Micro.Editor.Infrastructure.Controllers
{
    public interface IDocumentsController
    {
        void AddDocument(IDocumentViewModel doc);
        void RemoveDocument(IDocumentViewModel doc);
    }
}
