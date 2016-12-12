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
            Drawing drawComp = new Drawing();
            addComponent(drawComp);

            drawComp.Sprite = Art.GetSprite("ball");
            base.addBehavior(new SimpleBallBehavior());
        }

        override public void Update()
        {

            base.Update();
        }
    }
}
