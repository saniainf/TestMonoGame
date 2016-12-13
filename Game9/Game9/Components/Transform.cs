using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class Transform : IComponent
    {
        public bool IsRemove { get { return false; } set { } }
        public Vector2 Position { get { return position; } set { position = value; } }
        public Vector2 Size { get { return size; } set { size = value; } }

        private Entity root;
        private Vector2 position;
        private Vector2 size;

        public Transform(Entity rootEntity)
        {
            root = rootEntity;
            position = Vector2.Zero;
            size = Vector2.Zero;
            root.onUpdate += Update;
        }

        public void Initialize()
        {

        }

        public void Update()
        {

        }
    }
}
