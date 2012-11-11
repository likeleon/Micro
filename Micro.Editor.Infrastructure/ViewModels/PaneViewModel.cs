using System;
using Caliburn.Micro;

namespace Micro.Editor.Infrastructure.ViewModels
{
    public abstract class PaneViewModel : PropertyChangedBase
    {
        private string title;
        private string contentId;
        private string toolTip;

        public string Title
        {
            get { return this.title; }
            private set
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
            private set
            {
                if (this.contentId != value)
                {
                    this.contentId = value;
                    NotifyOfPropertyChange(() => ContentId);
                }
            }
        }

        public string ToolTip
        {
            get { return this.toolTip; }
            protected set
            {
                if (this.toolTip != value)
                {
                    this.toolTip = value;
                    NotifyOfPropertyChange(() => ToolTip);
                }
            }
        }

        protected PaneViewModel(string title, string contentId)
        {
            Title = title;
            ContentId = contentId;
            ToolTip = Title;
        }
    }
}
