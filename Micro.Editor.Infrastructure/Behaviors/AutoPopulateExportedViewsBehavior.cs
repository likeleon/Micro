using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;

namespace Micro.Editor.Infrastructure.Behaviors
{
    [Export(typeof(AutoPopulateExportedViewsBehavior))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AutoPopulateExportedViewsBehavior : RegionBehavior, IPartImportsSatisfiedNotification
    {
        public static readonly string BehaviorKey = "AutoPopulateExportedViewsBehavior";

        protected override void OnAttach()
        {
            AddRegisteredViews();
        }

        void IPartImportsSatisfiedNotification.OnImportsSatisfied()
        {
            AddRegisteredViews();
        }

        private void AddRegisteredViews()
        {
            if (Region == null)
                return;
             
            foreach (var viewEntry in RegisteredViews)
            {
                if (viewEntry.Metadata.RegionName == Region.Name)
                {
                    var view = viewEntry.Value;

                    if (!Region.Views.Contains(view))
                    {
                        Region.Add(view);
                    }
                }
            }
        }

        [ImportMany(AllowRecomposition = true)]
        public Lazy<object, IViewRegionRegistration>[] RegisteredViews { get; set; }
    }
}
