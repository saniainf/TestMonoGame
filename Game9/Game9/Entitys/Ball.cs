using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class Ball : Entity, IDraw, IPhysics
    {
        public Ball()
            : base()
        {

        }

        public override void Initialize()
        {
            base.Initialize();
            Drawing drawComp = new Drawing(this);
            setComponent(drawComp);
            PhysicsBody pb = new PhysicsBody(this);
            pb.BodyType = PhysicBodyType.Dynamic;
            pb.Direction = new Vector2((float)(GameRoot.Rnd.Next(-100, 100)), (float)(GameRoot.Rnd.Next(-100, 100)));
            pb.Velocity = GameRoot.Rnd.Next(100);
            setComponent(pb);
            Transform tr = GetComponent<Transform>() as Transform;
            tr.Position = new Vector2(GameRoot.Rnd.Next(GameRoot.Screen.Width), GameRoot.Rnd.Next(GameRoot.Screen.Height));

            drawComp.SetSprite("center", new Vector2(0f, 0f), Art.GetSprite("dot"));
            drawComp.SetSprite("left", new Vector2(-32f, -16f), Art.GetSprite("ship"));
            drawComp.SetSprite("right", new Vector2(0f, -16f), Art.GetSprite("ship"));
        }

        override public void Update()
        {
            base.Update();
        }

        public Drawing DrawComponent
        {
            get { return GetComponent<Drawing>() as Drawing; }
        }

        public PhysicsBody PhysicsComponent
        {
            get { return GetComponent<PhysicsBody>() as PhysicsBody; }
        }
    }
}
