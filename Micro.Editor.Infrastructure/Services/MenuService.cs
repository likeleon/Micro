using System.ComponentModel.Composition;
using Micro.Editor.Infrastructure.Models;
using Micro.Editor.Infrastructure.ViewModels;

namespace Micro.Editor.Infrastructure.Services
{
    [Export(typeof(IMenuService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public sealed class MenuService : IMenuService
    {
        private readonly ShellViewModel shell;

        [ImportingConstructor]
        public MenuService(ShellViewModel shell)
        {
            this.shell = shell;
        }

        void IMenuService.AddViewMenu(MenuItem item)
        {
            this.shell.AddViewMenu(item);
        }
    }
}
