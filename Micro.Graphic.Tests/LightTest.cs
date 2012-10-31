﻿using Micro.Core;
using Micro.Core.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Graphic.Tests
{
    [TestClass()]
    public class LightTest
    {
        [TestMethod()]
        public void Light_DefaultValues_Test()
        {
            var light = new Light();
            Assert.AreNotEqual(Vector3.Zero, light.Position);
            Assert.AreNotEqual(Color.Black, light.Color);
            Assert.AreNotEqual(Color.Black, light.AmbientColor);
        }
    }
}
