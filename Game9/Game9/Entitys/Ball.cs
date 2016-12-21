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
            drawComp.SetSprite("left", new Point(-16, -16), Art.GetSprite("ship"));
            drawComp.SetSprite("right", new Point(64, 64), Art.GetSprite("ship"));
            drawComp.SetSprite("center", new Point(0, 0), Art.GetSprite("dot"));
            setComponent(drawComp);

            PhysicsBody pb = new PhysicsBody(this);
            pb.BodyType = PhysicBodyType.Dynamic;
            pb.Direction = new Vector2((float)(GameRoot.Rnd.Next(-100, 100)), (float)(GameRoot.Rnd.Next(-100, 100)));
            pb.Velocity = GameRoot.Rnd.Next(100);
            pb.Collider2D.ColliderType = Collider2DType.Box;
            this.TransformComponent.Position = new Vector2(GameRoot.Rnd.Next(GameRoot.Screen.Width), GameRoot.Rnd.Next(GameRoot.Screen.Height));
            //this.TransformComponent.Position = new Vector2(32, 32);
            pb.Collider2D.Size = drawComp.BoundingBox.Size;
            pb.Collider2D.Offset = drawComp.BoundingBox.Center;
            setComponent(pb);
        }

        override public void Update()
        {
            base.Update();
        }
    }
}
