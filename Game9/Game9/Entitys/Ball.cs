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
        public Drawing DrawComponent { get { return GetComponent<Drawing>() as Drawing; } }
        public PhysicsBody PhysicsComponent { get { return GetComponent<PhysicsBody>() as PhysicsBody; } }

        public Ball()
            : base()
        {
            Drawing drawComp = new Drawing(this);
            drawComp.SetSprite("center", new Vector2(0f, 0f), Art.GetSprite("dot"));
            drawComp.SetSprite("left", new Vector2(-32f, -32f), Art.GetSprite("ship"));
            drawComp.SetSprite("right", new Vector2(0f, 0f), Art.GetSprite("ship"));
            setComponent(drawComp);

            PhysicsBody pb = new PhysicsBody(this);
            pb.BodyType = PhysicBodyType.Dynamic;
            pb.Direction = new Vector2((float)(GameRoot.Rnd.Next(-100, 100)), (float)(GameRoot.Rnd.Next(-100, 100)));
            pb.Velocity = GameRoot.Rnd.Next(100);
            pb.Collider2D.ColliderType = Collider2DType.Box;
            this.TransformComponent.Position = new Vector2(GameRoot.Rnd.Next(GameRoot.Screen.Width), GameRoot.Rnd.Next(GameRoot.Screen.Height));
            //this.TransformComponent.Position = new Vector2(32, 32);
            pb.Collider2D.Size = drawComp.SizeForSprite;
            setComponent(pb);
        }

        override public void Update()
        {
            base.Update();
        }
    }
}
