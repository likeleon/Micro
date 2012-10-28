using Micro.Core.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Micro.Core.Tests
{
    internal class TestHelpers
    {
        internal static void QuaternionAreNearEqual(Quaternion lhs, Quaternion rhs)
        {
            Assert.IsTrue(MathUtils.IsNearZero((lhs - rhs).Length));
        }

        internal static void Matrix3AreNearEqual(Matrix3 lhs, Matrix3 rhs)
        {
            var diff = lhs - rhs;
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    Assert.IsTrue(MathUtils.IsNearZero(diff[i, j]));
                }
            }
        }

        internal static void AreEqual(Vector3 lhs, Vector3 rhs)
        {
            var diff = lhs - rhs;
            Assert.IsTrue(MathUtils.IsNearZero(diff.x));
            Assert.IsTrue(MathUtils.IsNearZero(diff.y));
            Assert.IsTrue(MathUtils.IsNearZero(diff.z));
        }
    }
}
