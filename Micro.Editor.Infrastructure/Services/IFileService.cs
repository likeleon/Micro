
namespace Micro.Editor.Infrastructure.Services
{
    public interface IFileService
    {
        string GetFullPath(string path);
        string GetFileName(string path);
        string[] GetDirectories(string path);
        string[] GetFiles(string path);
    }
}
