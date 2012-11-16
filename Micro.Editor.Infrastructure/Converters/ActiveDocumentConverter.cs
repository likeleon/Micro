using System;
using System.Windows.Data;
using Micro.Editor.Infrastructure.ViewModels;

namespace Micro.Editor.Infrastructure.Converters
{
    public sealed class ActiveDocumentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is IDocumentViewModel)
                return value;

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is IDocumentViewModel)
                return value;

            return Binding.DoNothing;
        }
    }
}
