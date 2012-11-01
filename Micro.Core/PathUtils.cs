using System.IO;

namespace Micro.Core
{
    public static class PathUtils
    {
        public static bool IsAbsolutePath(string path)
        {
            return Path.IsPathRooted(path);
        }
    }
}
