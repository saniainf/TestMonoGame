using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class SimpleBallBehavior : IBehavior
    {
        public bool IsRemove
        {
            get
            {
                return false;
            }
            set
            {

            }
        }

        private Entity root;

        public SimpleBallBehavior(Entity rootEntity)
        {
            root = rootEntity;
            root.onUpdate += Update;
        }

        public void Initialize()
        {

        }

        public void Update()
        {
            Transform t = root.GetComponent<Transform>() as Transform;
            t.Position = new Vector2(t.Position.X + 1f, t.Position.Y);
        }
    }
}
