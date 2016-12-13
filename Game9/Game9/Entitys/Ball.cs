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
        public Ball()
            : base()
        {
            Drawing drawComp = new Drawing(this);
            setComponent(drawComp);

            drawComp.Sprite = Art.GetSprite("ball");
            base.setBehavior(new SimpleBallBehavior(this));
        }

        override public void Update()
        {

            base.Update();
        }
    }
}
