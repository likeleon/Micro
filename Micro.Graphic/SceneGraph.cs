using System.Collections.Generic;

namespace Micro.Graphic
{
    public sealed class SceneGraph
    {
        public IEnumerable<IRenderable> Renderables
        {
            get { return this.renderables; }
        }

        public IEnumerable<ISprite> Sprites
        {
            get { return this.sprites; }
        }

        private readonly List<IRenderable> renderables;
        private readonly List<ISprite> sprites;

        public SceneGraph()
        {
            this.renderables = new List<IRenderable>();
            this.sprites = new List<ISprite>();
        }

        public bool AddRenderable(IRenderable renderable)
        {
            if (this.renderables.Contains(renderable))
                return false;

            this.renderables.Add(renderable);
            return true;
        }

        public bool RemoveRenderable(IRenderable renderable)
        {
            return this.renderables.Remove(renderable);
        }

        public bool AddSprite(ISprite sprite)
        {
            if (this.sprites.Contains(sprite))
                return false;

            this.sprites.Add(sprite);
            return true;
        }

        public bool RemoveSprite(ISprite sprite)
        {
            return this.sprites.Remove(sprite);
        }
    }
}
