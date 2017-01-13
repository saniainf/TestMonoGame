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

        private Vector2 basePosition;
        private float bounce;

        public Ball()
            : base()
        {
            drawComponent = new DrawComponent(this);
            //drawComponent.AddSprite("paddle_middle", new Point(0, 0), Art.GetSprite("paddle_mid"), new Rectangle(0, 0, 20, 20), SpriteEffects.None);
            //drawComponent.AddSprite("paddle_left", new Point(-23, 0), Art.GetSprite("paddle_left"), new Rectangle(0, 0, Art.GetSprite("paddle_left").Width, Art.GetSprite("paddle_left").Height), SpriteEffects.None);
            //drawComponent.AddSprite("paddle_right", new Point(23, 0), Art.GetSprite("paddle_left"), new Rectangle(0, 0, Art.GetSprite("paddle_left").Width, Art.GetSprite("paddle_left").Height), SpriteEffects.FlipHorizontally);            
            drawComponent.AddSprite("paddle_left", new Point(-22, 0), Art.GetSprite("spriteSheet"), new Rectangle(0, 256, 44, 22), SpriteEffects.None);
            drawComponent.AddSprite("paddle_right", new Point(0, 0), Art.GetSprite("spriteSheet"), new Rectangle(0, 256, 44, 22), SpriteEffects.FlipHorizontally);
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

            basePosition = TransformComponent.Position;
        }

        override public void Update()
        {
            Rectangle rect = drawComponent.GetSprite("paddle_left").SourceRectangle;
            rect.Y = rect.Y + 22;
            if (rect.Y == 344)
                rect.Y = 256;
            drawComponent.GetSprite("paddle_left").SourceRectangle = rect;

            rect = drawComponent.GetSprite("paddle_right").SourceRectangle;
            rect.Y = rect.Y + 22;
            if (rect.Y == 344)
                rect.Y = 256;
            drawComponent.GetSprite("paddle_right").SourceRectangle = rect;
            base.Update();

            // Bounce control constants
            const float BounceHeight = 0.18f;
            const float BounceRate = 3.0f;

            // Bounce along a sine curve over time.
            // Include the X coordinate so that neighboring gems bounce in a nice wave pattern.            
            double t = GameRoot.ThisGameTime.TotalGameTime.TotalSeconds * BounceRate;
            bounce = (float)Math.Sin(t) * BounceHeight * 22;
            Vector2 v = drawComponent.GetSprite("paddle_left").Offset.ToVector2();
            drawComponent.GetSprite("paddle_left").Offset = + new Vector2(0.0f, bounce)
        }
    }
}
