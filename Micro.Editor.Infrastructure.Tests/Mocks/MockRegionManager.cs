using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using Microsoft.Practices.Prism.Regions;

namespace Micro.Editor.Infrastructure.Tests.Mocks
{
    public sealed class MockRegionManager : IRegionManager
    {
        private MockRegionCollection regions = new MockRegionCollection();

        public IRegionCollection Regions
        {
            get { return this.regions; }
        }

        public IRegion AttachNewRegion(object regionTarget, string regionName)
        {
            throw new NotImplementedException();
        }

        public IRegionManager CreateRegionManager()
        {
            throw new NotImplementedException();
        }

        public bool Navigate(Uri source)
        {
            throw new NotImplementedException();
        }
    }

    internal sealed class MockRegionCollection : IRegionCollection
    {
        private Dictionary<string, IRegion> regions = new Dictionary<string, IRegion>();

        public IEnumerator<IRegion> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IRegion this[string regionName]
        {
            get { return this.regions[regionName]; }
        }

        public void Add(IRegion region)
        {
            this.regions[region.Name] = region;
        }

        public bool Remove(string regionName)
        {
            throw new NotImplementedException();
        }

        public bool ContainsRegionWithName(string regionName)
        {
            throw new NotImplementedException();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged = delegate { };
    }

    public sealed class MockRegion : IRegion
    {
        public readonly List<object> addedViews = new List<object>();
        private readonly List<object> activeViews = new List<object>();

        public string Name { get; set; }

        public IRegionManager Add(object view)
        {
            this.addedViews.Add(view);
            return null;
        }

        public void Remove(object view)
        {
            this.addedViews.Remove(view);
        }

        public IViewsCollection Views
        {
            get { return new MockViewsCollection(this.addedViews); }
        }

        public void Activate(object view)
        {
            SelectedItem = view;
            this.activeViews.Add(view);
        }

        public void Deactivate(object view)
        {
            this.activeViews.Remove(view);
        }

        public IRegionManager Add(object view, string viewName)
        {
            Add(view);
            return null;
        }

        public object GetView(string viewName)
        {
            return this.addedViews.Count > 0 ? this.addedViews[0] : null;
        }

        public IRegionManager Add(object view, string viewName, bool createRegionManagerScope)
        {
            throw new NotImplementedException();
        }

        public IRegionManager RegionManager
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IRegionBehaviorCollection Behaviors
        {
            get { throw new NotImplementedException(); }
        }

        public object SelectedItem { get; set; }

        public IViewsCollection ActiveViews
        {
            get { return new MockViewsCollection(this.activeViews); }
        }

        public object Context
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void RequestNavigate(Uri source, Action<NavigationResult> navigationCallback)
        {
            throw new NotImplementedException();
        }

        public IRegionNavigationService NavigationService
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Comparison<object> SortComparison
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void MoveFrom(IRegion sourceRegion, object view)
        {
            throw new NotImplementedException();
        }

        public void MoveFrom(IRegion sourceRegion, object view, string viewName)
        {
            throw new NotImplementedException();
        }
    }

    internal class MockViewsCollection : IViewsCollection
    {
        private readonly IList<object> views;

        public MockViewsCollection(IList<object> views)
        {
            this.views = views;
        }

        public bool Contains(object value)
        {
            return this.views.Contains(value);
        }

        public IEnumerator<object> GetEnumerator()
        {
            return this.views.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged = delegate { };
    }
}
