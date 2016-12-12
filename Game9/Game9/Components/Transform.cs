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
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public Transform()
        {
            Position = Vector2.Zero;
            Size = Vector2.Zero;
        }

        public void Initialize()
        {
            
        }

        public void Update()
        {

        }

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
    }
}
