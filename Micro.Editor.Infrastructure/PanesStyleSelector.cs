using System.Windows;
using System.Windows.Controls;
using Micro.Editor.Infrastructure.ViewModels;

namespace Micro.Editor.Infrastructure
{
    public sealed class PanesStyleSelector : StyleSelector
    {
        public Style DocumentStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            var frameworkElement = item as FrameworkElement;
            if (frameworkElement == null)
                return base.SelectStyle(item, container);

            if (frameworkElement.DataContext is DocumentViewModel)
                return DocumentStyle;

            return base.SelectStyle(item, container);
        }
    }
}
