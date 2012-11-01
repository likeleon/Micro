using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Graphic.Tests
{
    [TestClass]
    public class SceneGraphTest
    {
        [TestMethod()]
        public void SceneGraph_AddRemoveRenderable()
        {
            var sceneGraph = new SceneGraph();
            var renderable = TestHelpers.CreateRenderableMock(TestHelpers.Device);

            Assert.IsTrue(sceneGraph.AddRenderable(renderable));
            Assert.IsFalse(sceneGraph.AddRenderable(renderable));

            Assert.IsTrue(sceneGraph.RemoveRenderable(renderable));
            Assert.IsFalse(sceneGraph.RemoveRenderable(renderable));
        }
    }
}
