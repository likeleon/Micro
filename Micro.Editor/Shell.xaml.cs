using System.ComponentModel.Composition;
using System.Windows;

namespace Micro.Editor
{
    [Export]
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();
        }
    }
}
