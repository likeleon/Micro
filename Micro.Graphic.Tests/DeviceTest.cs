using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Graphic.Tests
{
    [TestClass()]
    public class DeviceTest
    {
        private Window Window { get; set; }

        [TestInitialize()]
        public void CreateWindow()
        {
            Window = new Window("DeviceTest", 640, 480);
        }

        [TestMethod()]
        public void Device_Constructor()
        {
            Device device = new Device(Window.Handle, Window.Width, Window.Height);
            Assert.IsNotNull(device.RawDevice);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Device_ConstructorFail()
        {
            Device device = new Device(IntPtr.Zero, 0, 0);
        }

        //[TestMethod()]
        //public void Device_RenderTarget()
        //{
        //    var device = new Device(Window.Handle, Window.Width, Window.Height);
        //    var renderTarget = new TextureRenderTarget(device, new Size(100, 100));

        //    device.RenderTarget = renderTarget;
        //    Assert.AreEqual(renderTarget, device.RenderTarget);
        //}
    }
}
