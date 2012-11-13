using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using AvalonDock;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Regions.Behaviors;

namespace Micro.Editor.Infrastructure.Behaviors
{
    [Export(typeof(DockingManagerDocumentsSourceSyncBehavior))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class DockingManagerDocumentsSourceSyncBehavior : RegionBehavior, IHostAwareRegionBehavior
    {
        public static readonly string BehaviorKey = "DockingManagerDocumentsSourceSyncBehavior";
        private DockingManager dockingManager;
        private ObservableCollection<object> documents = new ObservableCollection<object>();
        private ReadOnlyObservableCollection<object> readonlyDocuments;
        private bool updatingActiveViewsInManagerActiveContentChanged;

        #region Implements IHostAwareRegionBehavior
        public DependencyObject HostControl
        {
            get { return this.dockingManager; }
            set { this.dockingManager = value as DockingManager; }
        }
        #endregion

        public ReadOnlyObservableCollection<object> Documents
        {
            get { return this.readonlyDocuments ?? (this.readonlyDocuments = new ReadOnlyObservableCollection<object>(this.documents)); }
        }

        // Starts to monitor IRegion to keep it in sync with the items of the HostControl
        protected override void OnAttach()
        {
            bool isItemsSourceSet = this.dockingManager.DocumentsSource != null;
            if (isItemsSourceSet)
            {
                throw new InvalidOperationException();
            }

            BindingOperations.SetBinding(this.dockingManager, DockingManager.DocumentsSourceProperty, new Binding("Documents") { Source = this });

            foreach (var view in Region.Views)
            {
                this.documents.Add(view);
            }

            this.dockingManager.ActiveContentChanged += dockingManager_ActiveContentChanged;
            Region.ActiveViews.CollectionChanged += ActiveViews_CollectionChanged;
            Region.Views.CollectionChanged += Views_CollectionChanged;
        }

        private void dockingManager_ActiveContentChanged(object sender, EventArgs e)
        {
            try
            {
                this.updatingActiveViewsInManagerActiveContentChanged = true;

                if (sender != this.dockingManager)
                    return;

                var activeContent = this.dockingManager.ActiveContent;
                foreach (var view in Region.ActiveViews.Where(v => v != activeContent))
                {
                    Region.Deactivate(view);
                }

                if (Region.Views.Contains(activeContent) && !Region.ActiveViews.Contains(activeContent))
                {
                    Region.Activate(activeContent);
                }
            }
            finally
            {
                this.updatingActiveViewsInManagerActiveContentChanged = false;
            }
        }

        private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.updatingActiveViewsInManagerActiveContentChanged)
                return;

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (this.dockingManager.ActiveContent != null &&
                    this.dockingManager.ActiveContent != e.NewItems[0] &&
                    Region.ActiveViews.Contains(this.dockingManager.ActiveContent))
                {
                    Region.Deactivate(this.dockingManager.ActiveContent);
                }

                this.dockingManager.ActiveContent = e.NewItems[0];
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove &&
                e.OldItems.Contains(this.dockingManager.ActiveContent))
            {
                this.dockingManager.ActiveContent = null;
            }
        }

        private void Views_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var startIndex = e.NewStartingIndex;
                    foreach (var newItem in e.NewItems)
                    {
                        this.documents.Insert(startIndex++, newItem);
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (var oldItem in e.OldItems)
                    {
                        this.documents.Remove(oldItem);
                    }
                    break;
            }
        }
    }
}
