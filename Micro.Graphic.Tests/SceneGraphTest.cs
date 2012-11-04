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
            var renderable = TestHelpers.CreateRenderable();

            Assert.IsTrue(sceneGraph.AddRenderable(renderable));
            Assert.IsFalse(sceneGraph.AddRenderable(renderable));

            Assert.IsTrue(sceneGraph.RemoveRenderable(renderable));
            Assert.IsFalse(sceneGraph.RemoveRenderable(renderable));
        }

        [TestMethod()]
        public void SceneGraph_AddRemoveSprite()
        {
            var sceneGraph = new SceneGraph();
            var sprite = TestHelpers.CreateSprite();

            Assert.IsTrue(sceneGraph.AddSprite(sprite));
            Assert.IsFalse(sceneGraph.AddSprite(sprite));

            Assert.IsTrue(sceneGraph.RemoveSprite(sprite));
            Assert.IsFalse(sceneGraph.RemoveSprite(sprite));
        }
    }
}
