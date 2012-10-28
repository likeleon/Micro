using Micro.Core.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Core.Tests
{
    [TestClass()]
    public class SlimDXMappingsTest
    {
        [TestMethod()]
        public void SlimDxMappings_ToD3DMatrix()
        {
            Assert.AreEqual(SlimDX.Matrix.Identity, Matrix4.Identity.ToD3DMatrix());
            Assert.AreEqual(SlimDX.Matrix.Identity, Matrix3.Identity.ToD3DMatrix());
        }

        [TestMethod()]
        public void SlimDxMappings_ToMatrix4()
        {
            Assert.AreEqual(Matrix4.Identity, SlimDX.Matrix.Identity.ToMatrix4());
        }

        [TestMethod()]
        public void SlimDxMappings_ToD3DVector2()
        {
            var actual = new Vector2(0.1f, 0.22f);
            var expected = new SlimDX.Vector2(actual.x, actual.y);
            Assert.AreEqual(expected, actual.ToD3DVector2());
        }

        [TestMethod()]
        public void SlimDxMappings_D3DMapping_Vector3()
        {
            var actual = new Vector3(0.1f, 0.22f, 0.33f);
            var expected = new SlimDX.Vector3(actual.x, actual.y, actual.z);
            Assert.AreEqual(expected, actual.ToD3DVector3());
            Assert.AreEqual(actual, expected.ToVector3());
        }

        [TestMethod()]
        public void SlimDxMappings_ToD3DVector4()
        {
            var actual = new Vector4(0.1f, 0.22f, 0.33f, 0.444f);
            var expected = new SlimDX.Vector4(actual.x, actual.y, actual.z, actual.w);
            Assert.AreEqual(expected, actual.ToD3DVector4());
        }

        [TestMethod()]
        public void SlimDxMappings_ToD3DColor4()
        {
            var actual = new Color(0.1f, 0.22f, 0.333f, 0.4444f);
            var expected = new SlimDX.Color4(actual.a, actual.r, actual.g, actual.b);
            Assert.AreEqual(expected, actual.ToD3DColor4());
        }

        [TestMethod()]
        public void SlimDxMappings_FromColor_ToD3DVector4()
        {
            var actual = new Color(0.1f, 0.22f, 0.333f, 0.4444f);
            var expected = new SlimDX.Vector4(actual.r, actual.g, actual.b, actual.a);
            Assert.AreEqual(expected, actual.ToD3DVector4());
        }

        [TestMethod()]
        public void SlimDxMappings_FromD3DColor4_ToIreliaColor()
        {
            var actual = new SlimDX.Color4(0.1f, 0.22f, 0.333f, 0.4444f);
            var expected = new Color(actual.Alpha, actual.Red, actual.Green, actual.Blue);
            Assert.AreEqual(expected, actual.ToIreliaColor());
        }
    }
}
