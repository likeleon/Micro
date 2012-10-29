using Micro.Graphic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Graphic.Tests
{
    [TestClass()]
    public class WindowTest
    {
        [TestMethod()]
        public void Window_Constructor()
        {
            string title = "Window Test";
            int width = 800, height = 600;

            Window window = new Window(title, width, height);
            Assert.AreEqual(true, window.Created);
            Assert.AreEqual(title, window.Title);
            Assert.AreEqual(width, window.Width);
            Assert.AreEqual(height, window.Height);

            Assert.AreNotEqual(null, window.Handle);
        }

        [TestMethod()]
        public void Window_CreationFail()
        {
            Window window = new Window("Invalid width", -100, 300);
            Assert.AreEqual(false, window.Created);

            window = new Window("Invalid height", 100, -300);
            Assert.AreEqual(false, window.Created);
        }
    }
}
