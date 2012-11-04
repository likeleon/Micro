using Micro.Core;
using Micro.Core.Math;
using Micro.Graphic;

namespace Micro.GUI
{
    public class TextBlock : ISprite
    {
        #region Properties
        public TrueTypeFont Font { get; set; }
        public string Text { get; set; }
        public Color Foreground { get; set; }
        public Vector2 Position { get; set; }
        #endregion

        public TextBlock()
        {
            Text = string.Empty;
            Foreground = Color.Black;
        }

        #region Implements ISprite
        public bool Draw(SpriteRenderer renderer)
        {
            if (Font == null || string.IsNullOrWhiteSpace(Text))
                return false;

            return renderer.Draw(Text, Font, (int)Position.x, (int)Position.y, Foreground);
        }
        #endregion
    }
}
