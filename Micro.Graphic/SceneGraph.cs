using System.Collections.Generic;

namespace Micro.Graphic
{
    public sealed class SceneGraph
    {
        public List<IRenderable> Renderables { get; private set; }

        public SceneGraph()
        {
            Renderables = new List<IRenderable>();
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
    }
}
