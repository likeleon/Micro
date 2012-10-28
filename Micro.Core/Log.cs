using System;

namespace Micro.Core
{
    public enum LogLevel
    {
        // Error condition. System is unusable.
        Error,

        // Minor user-level exceptions that are not expected during normal processing: an incoming HTTP parameter in the wrong format, an uploaded file that unexpectedly couldn't be opened, etc. 
        // Use WARNING for errors that generally only affect the current transaction for the current user, and never for anything that would bring the application down.
        Warn,

        // For each user-level transaction. It will summarize that transaction, providing the username that initiated the transaction and some key business parameters (if they are safe to log; some numbers may need masking).
        Info,

        // Intra-method debugging information (incoming parameters, local variables, etc).
        Debug
    }

    public static class Log
    {
        public sealed class LoggedEventArgs : EventArgs
        {
            public LogLevel Level { get; set; }
            public string Message { get; set; }
            public Exception Exception { get; set; }
        }

        public static event EventHandler<LoggedEventArgs> Logged = delegate {};

        #region Error
        public static void Error(string message)
        {
            Error(message, null);
        }

        public static void Error(string message, Exception exception)
        {
            LogImpl(LogLevel.Error, message, exception);
        }

        public static void ErrorFormat(string format, params object[] args)
        {
            Error(string.Format(format, args), null);
        }
        #endregion

        #region Warn
        public static void Warn(string message)
        {
            Warn(message, null);
        }

        public static void Warn(string message, Exception exception)
        {
            LogImpl(LogLevel.Warn, message, exception);
        }

        public static void WarnFormat(string format, params object[] args)
        {
            Warn(string.Format(format, args), null);
        }
        #endregion

        #region Info
        public static void Info(string message)
        {
            Info(message, null);
        }

        public static void Info(string message, Exception exception)
        {
            LogImpl(LogLevel.Info, message, exception);
        }

        public static void InfoFormat(string format, params object[] args)
        {
            Info(string.Format(format, args), null);
        }
        #endregion

        #region Debug
        public static void Debug(string message)
        {
            Debug(message, null);
        }

        public static void Debug(string message, Exception exception)
        {
            LogImpl(LogLevel.Debug, message, exception);
        }

        public static void DebugFormat(string format, params object[] args)
        {
            Debug(string.Format(format, args), null);
        }
        #endregion

        private static void LogImpl(LogLevel level, string message, Exception exception)
        {
            Logged(null, new LoggedEventArgs() { Level = LogLevel.Error, Message = message, Exception = exception });
        }
    }
}
