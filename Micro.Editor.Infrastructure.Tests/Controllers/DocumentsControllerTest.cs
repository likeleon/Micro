using System;
using Micro.Editor.Infrastructure.Controllers;
using Micro.Editor.Infrastructure.Models;
using Micro.Editor.Infrastructure.Tests.Mocks;
using Micro.Editor.Infrastructure.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Micro.Editor.Infrastructure.Tests.Controllers
{
    [TestClass]
    public class DocumentsControllerTest
    {
        private MockRegion docsRegion;
        private MockRegionManager regionManager;

        [TestInitialize]
        public void SetUp()
        {
            this.docsRegion = new MockRegion();
            this.regionManager = new MockRegionManager();
            regionManager.Regions.Add(RegionNames.AvalonDocumentRegion, docsRegion);
        }

        private class MockDocumentViewModel : IDocumentViewModel
        {
            public event EventHandler CloseRequested = delegate {};

            public void RaiseCloseRequested()
            {
                CloseRequested(this, EventArgs.Empty);
            }
        }

        [TestMethod]
        public void DocumentsController_AddAndRemoveDocument()
        {
            try
            {
                var mockDocumentViewModel = new MockDocumentViewModel();
                var mockServiceLocator = new Mock<ServiceLocatorImplBase>();
                var controller = new DocumentsController(this.regionManager);

                controller.AddDocument(mockDocumentViewModel);
                var region = (MockRegion)this.regionManager.Regions[RegionNames.AvalonDocumentRegion];
                Assert.AreEqual<int>(1, region.addedViews.Count);
                mockDocumentViewModel.RaiseCloseRequested();
                Assert.AreEqual<int>(0, region.addedViews.Count);
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }
        }
    }
}
