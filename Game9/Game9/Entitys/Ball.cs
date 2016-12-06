using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class Ball : Entity, IDraw
    {

        public Texture2D Sprite { get { return sprite; } set { } }

        private Texture2D sprite;

        public Ball()
            : base()
        {
            sprite = Art.GetSprite("ball");
            base.addBehavior(new SimpleBallBehavior());
        }

        override public void Update()
        {

            base.Update();
        }
    }
}
