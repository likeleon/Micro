using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Micro.Editor.Infrastructure.Behaviors;
using Microsoft.Practices.Prism.Regions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Editor.Infrastructure.Tests.Behaviors
{
    [TestClass]
    public class AutoPopulateExportedViewsBehaviorTest
    {
        [ViewExport(RegionName = "region1")]
        public class View1 { }

        [ViewExport(RegionName = "region2")]
        public class View2 { }

        [ViewExport(RegionName = "region1")]
        public class View3 { }

        [ViewExport(RegionName = "region2")]
        public class View4 { }

        [TestMethod]
        public void AutoPopulateExportedViewsBehavior_WhenAttached_ThenAddsViewsRegisteredToTheRegion()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(AutoPopulateExportedViewsBehavior).Assembly));
            catalog.Catalogs.Add(new TypeCatalog(typeof(View1), typeof(View2)));

            var container = new CompositionContainer(catalog);

            var behavior = container.GetExportedValue<AutoPopulateExportedViewsBehavior>();

            var region = new Region() { Name = "region1" };

            region.Behaviors.Add("", behavior);

            Assert.AreEqual(1, region.Views.Cast<object>().Count());
            Assert.IsTrue(region.Views.Cast<object>().Any(e => e.GetType() == typeof(View1)));
        }

        [TestMethod]
        public void AutoPopulateExportedViewsBehavior_WhenRecomposed_ThenAddsNewViewsRegisteredToTheRegion()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(AutoPopulateExportedViewsBehavior).Assembly));
            catalog.Catalogs.Add(new TypeCatalog(typeof(View1), typeof(View2)));

            var container = new CompositionContainer(catalog);

            var behavior = container.GetExportedValue<AutoPopulateExportedViewsBehavior>();

            var region = new Region() { Name = "region1" };

            region.Behaviors.Add("", behavior);

            catalog.Catalogs.Add(new TypeCatalog(typeof(View3), typeof(View4)));

            Assert.AreEqual(2, region.Views.Cast<object>().Count());
            Assert.IsTrue(region.Views.Cast<object>().Any(e => e.GetType() == typeof(View1)));
            Assert.IsTrue(region.Views.Cast<object>().Any(e => e.GetType() == typeof(View3)));
        }
    }
}
