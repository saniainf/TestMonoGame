using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class Ball : Entity, IDrawing, IPhysic
    {
        public DrawComponent DrawComponent { get { return drawComponent; } }
        public PhysicsComponent PhysicsComponent { get { return physicsComponent; } }

        private DrawComponent drawComponent;
        private PhysicsComponent physicsComponent;

        public Ball()
            : base()
        {
            drawComponent = new DrawComponent(this);
            drawComponent.SetSprite("paddle_middle", new Point(0, 0), Art.GetSprite("paddle_mid"), new Rectangle(0, 0, Art.GetSprite("paddle_mid").Width, Art.GetSprite("paddle_mid").Height), SpriteEffects.None);
            drawComponent.SetSprite("paddle_left", new Point(-25, 0), Art.GetSprite("paddle_left"), new Rectangle(0, 0, Art.GetSprite("paddle_left").Width, Art.GetSprite("paddle_left").Height), SpriteEffects.None);
            drawComponent.SetSprite("paddle_right", new Point(25, 0), Art.GetSprite("paddle_left"), new Rectangle(0, 0, Art.GetSprite("paddle_left").Width, Art.GetSprite("paddle_left").Height), SpriteEffects.FlipHorizontally);
            setComponent(drawComponent);

            physicsComponent = new PhysicsComponent(this);
            physicsComponent.BodyType = PhysicBodyType.Dynamic;
            physicsComponent.Direction = new Vector2((float)(GameRoot.Rnd.Next(-100, 100)), (float)(GameRoot.Rnd.Next(-100, 100)));
            physicsComponent.Velocity = GameRoot.Rnd.Next(100);
            physicsComponent.Collider2D.ColliderType = Collider2DType.Box;
            this.TransformComponent.Position = new Vector2(GameRoot.Rnd.Next(GameRoot.Screen.Width), GameRoot.Rnd.Next(GameRoot.Screen.Height));
            //this.TransformComponent.Position = new Vector2(32, 32);
            physicsComponent.Collider2D.Size = drawComponent.BoundingBox.Size;
            physicsComponent.Collider2D.Offset = drawComponent.BoundingBox.Center;
            setComponent(physicsComponent);
        }

        override public void Update()
        {
            base.Update();
        }
    }
}
