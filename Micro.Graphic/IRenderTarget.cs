using Micro.Core;
using SlimDX.Direct3D9;
using D3D = SlimDX.Direct3D9;

namespace Micro.Graphic
{
    public interface IRenderTarget
    {
        Size Size { get; }

        D3D.Surface TargetSurface { get; }
        D3D.Surface DepthStencilSurface { get; }

        bool ClearBackGround { get; }
        ClearFlags ClearOptions { get; }
        Color ClearColor { get; }

        float LastFps { get; }

        void OnRender();
    }
}
