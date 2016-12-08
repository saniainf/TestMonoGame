using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class SimpleBallBehavior: IBehavior
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

        private Vector2 location;

        public SimpleBallBehavior(Vector2 location)
        {
            location.X = 100;
        }

        public void Initialize()
        {
            
        }

        public void Update()
        {
            location.X += 0.2f;
        }
    }
}
