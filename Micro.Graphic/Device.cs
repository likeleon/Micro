using System;
using Micro.Core;
using SlimDX;
using SlimDX.Direct3D9;
using D3D = SlimDX.Direct3D9;

namespace Micro.Graphic
{
    public sealed class Device : DisposableObject
    {
        #region Member fields
        private Direct3DEx direct3dEx;
        private Direct3D direct3d;
        private DeviceEx deviceEx;
        private D3D.Device device;
        private IRenderTarget renderTarget;

        // Device settings
        private readonly Format adapterFormat = Format.X8R8G8B8;
        private readonly Format backbufferFormat = Format.A8R8G8B8;
        private readonly Format depthStencilFormat = Format.D16;
        private readonly CreateFlags createFlags = CreateFlags.Multithreaded | CreateFlags.FpuPreserve;
        #endregion

        #region Private Properties
        private bool UseDeviceEx { get; set; }

        private Direct3D Direct3D
        {
            get
            {
                if (UseDeviceEx)
                    return this.direct3dEx;
                else
                    return this.direct3d;
            }
        }
        #endregion

        #region Public Properties
        public D3D.Device D3DDevice
        {
            get
            {
                if (UseDeviceEx)
                    return this.deviceEx;
                else
                    return this.device;
            }
        }

        public PresentParameters PresentParams
        {
            get;
            private set;
        }

        public IRenderTarget RenderTarget
        {
            get { return this.renderTarget; }
            set
            {
                if (this.renderTarget == value)
                    return;

                var result = D3DDevice.SetRenderTarget(0, value.TargetSurface);
                if (result.IsFailure)
                {
                    Log.WarnFormat("SetRenderTarget failed: {0}", result.Description);
                    return;
                }

                D3DDevice.DepthStencilSurface = value.DepthStencilSurface;
                this.renderTarget = value;
            }
        }
        #endregion

        #region Constructors
        public Device(IntPtr handle, int width, int height)
        {
            if (handle == IntPtr.Zero)
                throw new ArgumentException("Value must be a valid window handle", "handle");

            if (GetSystemMetrics(SM_REMOTESESSION) != 0)
                throw new Exception("We can't run at all under terminal services");

            // Create D3D
            try
            {
                this.direct3dEx = new Direct3DEx();
                UseDeviceEx = true;
            }
            catch
            {
                this.direct3d = new Direct3D();
                UseDeviceEx = false;
            }

            // Create device
            Result result;
            if (!Direct3D.CheckDeviceType(0, DeviceType.Hardware, this.adapterFormat, this.backbufferFormat, true, out result))
                throw new Exception("CheckDeviceType failed: " + result.ToString());

            if (!Direct3D.CheckDepthStencilMatch(0, DeviceType.Hardware, this.adapterFormat, this.backbufferFormat, this.depthStencilFormat, out result))
                throw new Exception("CheckDepthStencilMatch failed: " + result.ToString());

            Capabilities deviceCaps = Direct3D.GetDeviceCaps(0, DeviceType.Hardware);
            if ((deviceCaps.DeviceCaps & DeviceCaps.HWTransformAndLight) != 0)
                this.createFlags |= CreateFlags.HardwareVertexProcessing;
            else
                this.createFlags |= CreateFlags.SoftwareVertexProcessing;

            PresentParams = new PresentParameters()
            {
                BackBufferFormat = this.backbufferFormat,
                BackBufferCount = 1,
                BackBufferWidth = width,
                BackBufferHeight = height,
                Multisample = MultisampleType.None,
                SwapEffect = SwapEffect.Discard,
                EnableAutoDepthStencil = true,
                AutoDepthStencilFormat = this.depthStencilFormat,
                PresentFlags = PresentFlags.DiscardDepthStencil,
                PresentationInterval = PresentInterval.Immediate,
                Windowed = true,
                DeviceWindowHandle = handle,
            };

            if (UseDeviceEx)
            {
                this.deviceEx = new DeviceEx(this.direct3dEx,
                        0,
                        DeviceType.Hardware,
                        handle,
                        this.createFlags,
                        PresentParams);
            }
            else
            {
                this.device = new SlimDX.Direct3D9.Device(this.direct3d,
                    0,
                    DeviceType.Hardware,
                    handle,
                    this.createFlags,
                    PresentParams);
            }

        }
        #endregion

        #region DLL imports
        // can't figure out how to access remote session status through .NET
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int GetSystemMetrics(int smIndex);
        private const int SM_REMOTESESSION = 0x1000;
        #endregion

        #region Public Methods
        public bool BeginScene()
        {
            return D3DDevice.BeginScene().IsSuccess;
        }

        public bool EndScene()
        {
            return D3DDevice.EndScene().IsSuccess;
        }
        #endregion

        #region Overrides DisposableObject
        protected override void Dispose(bool disposeManagedResources)
        {
            if (!IsDisposed)
            {
                if (this.device != null)
                    this.device.Dispose();
                if (this.deviceEx != null)
                    this.deviceEx.Dispose();
                if (this.direct3d != null)
                    this.direct3d.Dispose();
                if (this.direct3dEx != null)
                    this.direct3dEx.Dispose();
            }

            base.Dispose(disposeManagedResources);
        }
        #endregion
    }
}
