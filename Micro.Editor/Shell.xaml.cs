using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using Elysium;
using Micro.Editor.Infrastructure.ViewModels;

namespace Micro.Editor
{
    [Export]
    public partial class Shell
    {
        public Shell()
        {
            InitializeComponent();
            Application.Current.Apply(Theme.Dark, AccentBrushes.Blue, Brushes.White);
            //Application.Current.Apply(Theme.Dark, GetRandomAccentBrush(), Brushes.White);
        }

        [Import]
        ShellViewModel ViewModel
        {
            set { DataContext = value; }
        }

        //private SolidColorBrush GetRandomAccentBrush()
        //{
        //    var rand = new Random();
        //    var properties = typeof(AccentBrushes).GetProperties(BindingFlags.Public | BindingFlags.Static)
        //                                          .Where(p => p.PropertyType == typeof(SolidColorBrush));
        //    return (SolidColorBrush)properties.ElementAt(rand.Next(properties.Count())).GetValue(null, null);
        //}
    }
}
