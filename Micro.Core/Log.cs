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
            Error(null, message);
        }

        public static void Error(Exception exception, string message)
        {
            LogImpl(LogLevel.Error, message, exception);
        }

        public static void ErrorFormat(string format, params object[] args)
        {
            Error(null, string.Format(format, args));
        }

        public static void ErrorFormat(Exception exception, string format, params object[] args)
        {
            Error(exception, string.Format(format, args));
        }
        #endregion

        #region Warn
        public static void Warn(string message)
        {
            Warn(null, message);
        }

        public static void Warn(Exception exception, string message)
        {
            LogImpl(LogLevel.Warn, message, exception);
        }

        public static void WarnFormat(string format, params object[] args)
        {
            WarnFormat(null, format, args);
        }

        public static void WarnFormat(Exception exception, string format, params object[] args)
        {
            Warn(exception, string.Format(format, args));
        }
        #endregion

        #region Info
        public static void Info(string message)
        {
            Info(null, message);
        }

        public static void Info(Exception exception, string message)
        {
            LogImpl(LogLevel.Info, message, exception);
        }

        public static void InfoFormat(string format, params object[] args)
        {
            InfoFormat(null, format, args);
        }

        public static void InfoFormat(Exception exception, string format, params object[] args)
        {
            Info(exception, string.Format(format, args));
        }
        #endregion

        #region Debug
        public static void Debug(string message)
        {
            Debug(null, message);
        }

        public static void Debug(Exception exception, string message)
        {
            LogImpl(LogLevel.Debug, message, exception);
        }

        public static void DebugFormat(string format, params object[] args)
        {
            DebugFormat(null, format, args);
        }

        public static void DebugFormat(Exception exception, string format, params object[] args)
        {
            Debug(exception, string.Format(format, args));
        }
        #endregion

        private static void LogImpl(LogLevel level, string message, Exception exception)
        {
            Logged(null, new LoggedEventArgs() { Level = LogLevel.Error, Message = message, Exception = exception });
        }
    }
}
