using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Prism.Logging;

namespace Micro.Editor
{
    public sealed class EnterpriseLibraryLoggerAdapter : ILoggerFacade
    {
        void ILoggerFacade.Log(string message, Category category, Priority priority)
        {
            Logger.Write(message, category.ToString(), (int)priority);
        }
    }
}
