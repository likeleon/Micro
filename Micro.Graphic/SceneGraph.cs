using System.Collections.Generic;

namespace Micro.Graphic
{
    public sealed class SceneGraph
    {
        public List<IRenderable> Renderables { get; private set; }
        public List<ISprite> Sprites { get; private set; }

        public SceneGraph()
        {
            Renderables = new List<IRenderable>();
            Sprites = new List<ISprite>();
        }

        public bool AddRenderable(IRenderable renderable)
        {
            if (Renderables.Contains(renderable))
                return false;

            Renderables.Add(renderable);
            return true;
        }

        public bool RemoveRenderable(IRenderable renderable)
        {
            return Renderables.Remove(renderable);
        }

        public bool AddSprite(ISprite sprite)
        {
            if (Sprites.Contains(sprite))
                return false;

            Sprites.Add(sprite);
            return true;
        }

        public bool RemoveSprite(ISprite sprite)
        {
            return Sprites.Remove(sprite);
        }
    }
}
