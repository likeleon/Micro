using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using AvalonDock;
using Micro.Editor.Infrastructure.Behaviors;
using Micro.Editor.Infrastructure.RegionAdapters;
using Micro.Editor.Modules.AssetBrowser;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace Micro.Editor
{
    [CLSCompliant(false)]
    public class Bootstrapper : MefBootstrapper
    {
        private readonly EnterpriseLibraryLoggerAdapter logger = new EnterpriseLibraryLoggerAdapter();

        [ImportMany("ApplicationResources", typeof(ResourceDictionary), AllowRecomposition = true)]
        private IEnumerable<ResourceDictionary> ApplicationResources { get; set; }

        protected override System.Windows.DependencyObject CreateShell()
        {
            return Container.GetExportedValue<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            ApplicationResources = Container.GetExportedValues<ResourceDictionary>("ApplicationResources");
            foreach (var resourceDictionary in ApplicationResources)
            {
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }

            Application.Current.MainWindow = (Shell)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.ComposeExportedValue<CompositionContainer>(Container);
        }

        protected override void ConfigureAggregateCatalog()
        {
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(DockingManagerRegionAdapter).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(AssetBrowserModule).Assembly));
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

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var factory = base.ConfigureDefaultRegionBehaviors();
            factory.AddIfMissing(AutoPopulateExportedViewsBehavior.BehaviorKey, typeof(AutoPopulateExportedViewsBehavior));
            return factory;
        }
    }
}
