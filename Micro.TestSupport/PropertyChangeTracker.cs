using System.Collections.Generic;
using System.ComponentModel;

namespace Micro.TestSupport
{
    public sealed class PropertyChangeTracker
    {
        private INotifyPropertyChanged changer;
        private List<string> notifications = new List<string>();

        public PropertyChangeTracker(INotifyPropertyChanged changer)
        {
            this.changer = changer;
            changer.PropertyChanged += (o, e) => { this.notifications.Add(e.PropertyName); };
        }

        public string[] ChangedProperties
        {
            get { return this.notifications.ToArray(); }
        }

        public void Reset()
        {
            this.notifications.Clear();
        }
    }
}
