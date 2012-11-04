using Micro.Core;
using Micro.Core.Math;
using D3D = SlimDX.Direct3D9;

namespace Micro.Graphic
{
    public sealed class TrueTypeFont : DisposableObject
    {
        public D3D.Font D3DFont { get; private set; }

        public TrueTypeFont(Device device, string faceName, int size)
        {
            D3DFont = new D3D.Font(device.D3DDevice, size, 0, D3D.FontWeight.Normal, 0, false,
                                     D3D.CharacterSet.Default, D3D.Precision.Default, D3D.FontQuality.Default,
                                     D3D.PitchAndFamily.Default, faceName);
        }

        #region Overrides DisposableObject
        protected override void Dispose(bool disposeManagedResources)
        {
            if (!IsDisposed)
            {
                if (D3DFont != null)
                    D3DFont.Dispose();
            }

            base.Dispose(disposeManagedResources);
        }
        #endregion
    }
}
