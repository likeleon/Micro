using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Elysium;
using Elysium.Notifications;

namespace Micro.Editor
{
    [Export]
    public partial class Shell
    {
        public Shell()
        {
            InitializeComponent();
            Application.Current.Apply(Theme.Dark, AccentBrushes.Blue, Brushes.White);
        }
    }
}
