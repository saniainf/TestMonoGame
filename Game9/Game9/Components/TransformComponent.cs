using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class TransformComponent : IComponent
    {
        public bool IsRemove { get { return false; } set { } }
        public Vector2 Position { get { return position; } set { position = value; } }
        public Vector2 Size { get { return size; } set { size = value; } }

        private Entity root;
        private Vector2 position; // center point
        private Vector2 size;

        public TransformComponent(Entity rootEntity)
        {
            root = rootEntity;
            position = Vector2.Zero;
            size = Vector2.Zero;
        }

        public void Update()
        {

        }
    }
}
