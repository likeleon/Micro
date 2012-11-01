using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Core.Tests
{
    [TestClass]
    public class PathUtilsTest
    {
        [TestMethod]
        public void PathUtils_IsAbsolutePath()
        {
            Assert.IsTrue(PathUtils.IsAbsolutePath(@"C:\Test"));
            Assert.IsFalse(PathUtils.IsAbsolutePath(@"Test\"));
            Assert.IsFalse(PathUtils.IsAbsolutePath(@"Test"));
        }
    }
}
