using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Micro.Editor.Infrastructure.Services;

namespace Micro.Editor.Infrastructure.Tests.Mocks
{
    public sealed class MockFileService : IFileService
    {
        private static readonly char folderSeparator = '\\';
        private readonly string workingDirectory = Directory.GetCurrentDirectory();
        private readonly List<string> folders = new List<string>();
        private readonly List<string> files = new List<string>();

        public void AddFolder(string folderPath)
        {
            this.folders.Add(GetFullPath(folderPath));
        }

        public void AddFile(string filePath)
        {
            this.files.Add(GetFullPath(filePath));
        }

        #region IFileService
        public string GetFullPath(string path)
        {
            return Path.Combine(this.workingDirectory, path).Replace('/', folderSeparator);
        }

        public string GetFileName(string path)
        {
            return Path.GetFileName(GetFullPath(path));
        }

        public string[] GetDirectories(string path)
        {
            string rootPath = GetFullPath(path);
            var result = this.folders.Where(p => !p.Equals(rootPath, StringComparison.CurrentCultureIgnoreCase))
                                   .Where(p => p.StartsWith(rootPath, StringComparison.CurrentCultureIgnoreCase))
                                   .Where(p => p.Replace(rootPath, string.Empty).LastIndexOf(folderSeparator) <= 0)
                                   .ToArray();
            return result;
        }

        public string[] GetFiles(string path)
        {
            string rootPath = GetFullPath(path);
            var result = this.files.Where(p => p.StartsWith(rootPath, StringComparison.CurrentCultureIgnoreCase))
                                   .Where(p => p.Replace(rootPath, string.Empty).LastIndexOf(folderSeparator) <= 0)
                                   .ToArray();
            return result;
        }
        #endregion
    }
}
