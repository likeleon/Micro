using System.Windows.Input;

namespace Micro.Editor.Infrastructure.Models
{
    public sealed class MenuItem
    {
        public string Name { get; set; }
        public ICommand Command { get; set; }
    }
}
