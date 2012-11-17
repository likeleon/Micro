using System.ComponentModel.Composition;
using System.IO;

namespace Micro.Editor.Infrastructure.Services
{
    [Export(typeof(IFileService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public sealed class FileService : IFileService
    {
        #region IFileService
        public string GetFullPath(string path)
        {
            return Path.GetFullPath(path);
        }

        public string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        public string[] GetDirectories(string path)
        {
            return Directory.GetDirectories(path);
        }
        #endregion
    }
}
