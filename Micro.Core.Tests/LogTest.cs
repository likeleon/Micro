using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Core.Tests
{
    [TestClass]
    public class LogTest
    {
        [TestMethod]
        public void Log_LoggedEventTest()
        {
            string actualMessage = null;
            Exception actualException = null;
            Log.Logged += (o, e) =>
            {
                actualMessage = e.Message;
                actualException = e.Exception;
            };

            foreach (var levelString in Enum.GetNames(typeof(LogLevel)))
            {
                string message = levelString + " Message";
                var exception = new Exception(levelString);
                var parameters = new object[] { message, exception };
                var method = typeof(Log).GetMethod(levelString, parameters.Select(p => p.GetType()).ToArray());
                method.Invoke(null, parameters);

                Assert.AreEqual(actualMessage, message);
                Assert.AreEqual(actualException, exception);
            }
        }
    }
}
