using System;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using AvalonDock;
using Micro.Editor.Infrastructure.RegionAdapters;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace Micro.Editor
{
    [CLSCompliant(false)]
    public class Bootstrapper : MefBootstrapper
    {
        private readonly EnterpriseLibraryLoggerAdapter logger = new EnterpriseLibraryLoggerAdapter();

        protected override System.Windows.DependencyObject CreateShell()
        {
            return Container.GetExportedValue<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Shell)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }

        protected override void ConfigureAggregateCatalog()
        {
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(DockingManagerRegionAdapter).Assembly));
        }

        protected override Microsoft.Practices.Prism.Logging.ILoggerFacade CreateLogger()
        {
            return this.logger;
        }

        protected override Microsoft.Practices.Prism.Regions.RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            base.ConfigureRegionAdapterMappings();

            var regionAdapterMappings = ServiceLocator.Current.GetInstance<RegionAdapterMappings>();
            if (regionAdapterMappings != null)
            {
                regionAdapterMappings.RegisterMapping(typeof(DockingManager), ServiceLocator.Current.GetInstance<DockingManagerRegionAdapter>());
            }
            return regionAdapterMappings;
        }
    }
}
