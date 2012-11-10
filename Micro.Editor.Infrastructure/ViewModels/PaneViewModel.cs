using System;
using Caliburn.Micro;

namespace Micro.Editor.Infrastructure.ViewModels
{
    public abstract class PaneViewModel : PropertyChangedBase
    {
        private string title;
        private string contentId;

        public string Title
        {
            get { return this.title; }
            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    NotifyOfPropertyChange(() => Title);
                }
            }
        }

        public virtual Uri IconSource
        {
            get { return null; }
        }

        public string ContentId
        {
            get { return this.contentId; }
            protected set
            {
                if (this.contentId != value)
                {
                    this.contentId = value;
                    NotifyOfPropertyChange(() => ContentId);
                }
            }
        }
    }
}
