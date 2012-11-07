using System.ComponentModel.Composition;
using System.Windows;
using Elysium.Theme;

namespace Micro.Editor
{
    [Export]
    public partial class Shell
    {
        public Shell()
        {
            InitializeComponent();
            ThemeManager.Instance.Dark(ThemeManager.Instance.Accent);
        }
    }
}
