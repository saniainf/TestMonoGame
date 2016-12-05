using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class Ball : EntityWithComponent, IDraw
    {

        public Texture2D Sprite
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Ball()
            : base()
        {

        }

        override public void Update()
        {

            base.Update();
        }
    }
}
