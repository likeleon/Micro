using System;
using Microsoft.Practices.Prism.ViewModel;

namespace Micro.Editor.Infrastructure.ViewModels
{
    public abstract class PaneViewModel : NotificationObject
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
                    RaisePropertyChanged(() => Title);
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
                    RaisePropertyChanged(() => ContentId);
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
                    RaisePropertyChanged(() => ToolTip);
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
