using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Graphic.Tests
{
    [TestClass]
    public class SceneGraphTest
    {
        private Device Device { get; set; }

        [TestInitialize()]
        public void SetUp()
        {
            Device = TestHelpers.GetDevice();
        }
        
        [TestMethod()]
        public void SceneGraph_AddRemoveRenderable()
        {
            var sceneGraph = new SceneGraph();
            var renderable = TestHelpers.CreateRenderableMock(Device);

            Assert.IsTrue(sceneGraph.AddRenderable(renderable));
            Assert.IsFalse(sceneGraph.AddRenderable(renderable));

            Assert.IsTrue(sceneGraph.RemoveRenderable(renderable));
            Assert.IsFalse(sceneGraph.RemoveRenderable(renderable));
        }
    }
}
