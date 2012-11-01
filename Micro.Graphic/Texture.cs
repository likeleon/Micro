using System.IO;
using Micro.Core;
using D3D = SlimDX.Direct3D9;

namespace Micro.Graphic
{
    public sealed class Texture : DisposableObject
    {
        #region Properties
        public D3D.Texture RawTexture { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        #endregion

        private readonly D3D.ImageFileFormat imageFileFormat;

        #region Constructors
        private Texture(D3D.Texture rawTexture, int width, int height, D3D.ImageFileFormat imageFileFormat)
        {
            RawTexture = rawTexture;
            Width = width;
            Height = height;
            this.imageFileFormat = imageFileFormat;
        }
        #endregion

        public static Texture Create(Device device, Size size)
        {
            var rawTexture = new D3D.Texture(device.RawDevice, (int)size.Width, (int)size.Height, 1, D3D.Usage.RenderTarget, D3D.Format.A8R8G8B8, D3D.Pool.Default);
            return new Texture(rawTexture, (int)size.Width, (int)size.Height, D3D.ImageFileFormat.Bmp);
        }

        public static Texture LoadFromFile(Device device, string filePath)
        {
            var imgInfo = D3D.ImageInformation.FromFile(filePath);
            var rawTexture = D3D.Texture.FromFile(device.RawDevice, filePath,
                                              imgInfo.Width, imgInfo.Height,
                                              1,
                                              D3D.Usage.None,
                                              D3D.Format.Unknown,
                                              D3D.Pool.Default,
                                              D3D.Filter.Default,
                                              D3D.Filter.Default,
                                              new Color(1.0f, 0.0f, 1.0f, 0.0f).ToArgb());
            return new Texture(rawTexture, imgInfo.Width, imgInfo.Height, imgInfo.ImageFileFormat);
        }

        public static Texture LoadFromStream(Device device, Stream stream)
        {
            var imgInfo = D3D.ImageInformation.FromStream(stream);
            var rawTexture = D3D.Texture.FromStream(device.RawDevice, stream, (int)(stream.Length - stream.Position),
                                                    imgInfo.Width, imgInfo.Height,
                                                    1,
                                                    D3D.Usage.None,
                                                    D3D.Format.Unknown,
                                                    D3D.Pool.Default,
                                                    D3D.Filter.Default,
                                                    D3D.Filter.Default,
                                                    new Color(1.0f, 0.0f, 1.0f, 0.0f).ToArgb());
            return new Texture(rawTexture, imgInfo.Width, imgInfo.Height, imgInfo.ImageFileFormat);
        }

        public bool SaveToFile(string filePath)
        {
            return D3D.Texture.ToFile(RawTexture, filePath, D3D.ImageFileFormat.Jpg).IsSuccess;
        }

        public Stream ToStream()
        {
            return D3D.Texture.ToStream(RawTexture, this.imageFileFormat);
        }

        public Stream ToJpgStream()
        {
            return D3D.Texture.ToStream(RawTexture, D3D.ImageFileFormat.Jpg);
        }

        #region Overrides DisposableObject
        protected override void Dispose(bool disposeManagedResources)
        {
            if (!IsDisposed)
            {
                if (RawTexture != null)
                    RawTexture.Dispose();
            }

            base.Dispose(disposeManagedResources);
        }
        #endregion
    }
}
