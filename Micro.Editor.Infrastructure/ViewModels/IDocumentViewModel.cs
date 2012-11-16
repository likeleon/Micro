using System;

namespace Micro.Editor.Infrastructure.ViewModels
{
    public interface IDocumentViewModel
    {
        event EventHandler CloseRequested;
    }
}
