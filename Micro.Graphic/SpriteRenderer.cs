using Micro.Core;
using Micro.Core.Math;
using D3D = SlimDX.Direct3D9;

namespace Micro.Graphic
{
    public sealed class SpriteRenderer
    {
        private readonly D3D.Sprite d3dSprite;

        public SpriteRenderer(Device device)
        {
            this.d3dSprite = new D3D.Sprite(device.D3DDevice);
        }

        public bool Begin()
        {
            return this.d3dSprite.Begin(D3D.SpriteFlags.AlphaBlend).IsSuccess;
        }

        public bool Draw(Texture texture, Rectangle sourceRect, Color modulateColor, Matrix4 transformMatrix)
        {
            this.d3dSprite.Transform = transformMatrix.ToD3DMatrix();
            var ret = this.d3dSprite.Draw(texture.RawTexture, sourceRect.ToD3DRectangle(), modulateColor.ToD3DColor4());
            this.d3dSprite.Transform = SlimDX.Matrix.Identity;
            return ret.IsSuccess;
        }

        public bool End()
        {
            return this.d3dSprite.End().IsSuccess;
        }
    }
}
