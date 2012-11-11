using System;
using System.ComponentModel.Composition;
using AvalonDock;
using Micro.Editor.Infrastructure.Behaviors;
using Microsoft.Practices.Prism.Regions;

namespace Micro.Editor.Infrastructure.RegionAdapters
{
    [Export(typeof(DockingManagerRegionAdapter))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class DockingManagerRegionAdapter : RegionAdapterBase<DockingManager>
    {
        [ImportingConstructor]
        public DockingManagerRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, DockingManager regionTarget)
        {
        }

        protected override IRegion CreateRegion()
        {
            return new Region();
        }

        protected override void AttachBehaviors(IRegion region, DockingManager regionTarget)
        {
            if (region == null)
                throw new ArgumentNullException("region");

            region.Behaviors.Add(DockingManagerDocumentsSourceSyncBehavior.BehaviorKey, new DockingManagerDocumentsSourceSyncBehavior() { HostControl = regionTarget });

            base.AttachBehaviors(region, regionTarget);
        }
    }
}
