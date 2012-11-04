using Micro.Core;
using Micro.Core.Math;

namespace Micro.Graphic
{
    public sealed class Sprite : ISprite
    {
        #region Properties
        public Texture Texture { get; private set; }
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public Radian Rotation { get; set; }
        public Rectangle SourceRect { get; set; }
        public Color ModulateColor { get; set; }
        #endregion

        public Sprite(Texture texture)
        {
            Texture = texture;
            ModulateColor = Color.White;
            Position = Vector2.Zero;
            Scale = new Vector2(1.0f, 1.0f);
            Rotation = new Radian(0.0f);
            SourceRect = new Rectangle(0, 0, Texture.Width, Texture.Height);
        }

        public bool Draw(Renderer renderer)
        {
            var center = new Vector2((SourceRect.Width * Scale.x) / 2, (SourceRect.Height * Scale.y) / 2);
            var transformMatrix = Matrix4.Transformation2D(Vector2.Zero, new Radian(0.0f), Scale, center, Rotation, Position);
            return renderer.Draw(Texture, SourceRect, ModulateColor, transformMatrix);
        }
    }
}
