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
            if (item is IDocumentViewModel)
                return DocumentStyle;

            return base.SelectStyle(item, container);
        }
    }
}
