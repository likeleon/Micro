using System;
using System.ComponentModel.Composition;
using Micro.Editor.Infrastructure.Models;
using Micro.Editor.Infrastructure.ViewModels;
using Microsoft.Practices.Prism.Regions;

namespace Micro.Editor.Infrastructure.Controllers
{
    [Export(typeof(IDocumentsController))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public sealed class DocumentsController : IDocumentsController
    {
        private IRegionManager regionManager;

        [ImportingConstructor]
        public DocumentsController(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        #region Implements IDocumentsController
        public void AddDocument(DocumentViewModel doc)
        {
            IRegion region = this.regionManager.Regions[RegionNames.AvalonDocumentRegion];

            if (!region.Views.Contains(doc))
            {
                doc.CloseRequested += doc_CloseRequested;
                region.Add(doc);
            }

            region.Activate(doc);
        }

        public void RemoveDocument(DocumentViewModel doc)
        {
            IRegion region = this.regionManager.Regions[RegionNames.AvalonDocumentRegion];

            if (!region.Views.Contains(doc))
                return;

            if (region.ActiveViews.Contains(doc))
            {
                region.Deactivate(doc);
            }

            doc.CloseRequested -= doc_CloseRequested;
            region.Remove(doc);
        }
        #endregion

        private void doc_CloseRequested(object sender, EventArgs e)
        {
            RemoveDocument(sender as DocumentViewModel);
        }
    }
}
