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
            Sprite pl = new Sprite("paddle_left", new Point(-22, 0), Art.GetImage("spriteSheet"), new Rectangle(0, 0, 44, 22), SpriteEffects.None);
            Sprite pr = new Sprite("paddle_right", new Point(22, 0), Art.GetImage("spriteSheet"), new Rectangle(0, 0, 44, 22), SpriteEffects.FlipHorizontally);
            drawComponent = new DrawComponent(this);
            drawComponent.AddSprite(pl);
            drawComponent.AddSprite(pr);

            //animation
            //Rectangle[] rSeq = Inf.RectangleSequence(4, 0, 0, 44, 22, 0, 22);
            //Rectangle[] lSeq = Inf.RectangleSequence(4, 0, 0, 44, 22, 0, 22);
            Rectangle[] rSeq = Inf.RectangleSequence(4, 0, 88, 44, 22, 0, 22);
            Rectangle[] lSeq = Inf.RectangleSequence(4, 0, 88, 44, 22, 0, 22);
            Rectangle[] rSeqReverse = Inf.RectangleSequence(4, 0, 154, 44, 22, 0, -22);
            Rectangle[] lSeqReverse = Inf.RectangleSequence(4, 0, 154, 44, 22, 0, -22);
            rSeq.Concat(rSeqReverse);
            lSeq.Concat(lSeqReverse);
            AnimatedSprite[] ap = new AnimatedSprite[2];
            ap[0] = new AnimatedSprite(pl.Id, lSeq, 0);
            ap[1] = new AnimatedSprite(pr.Id, rSeq, 0);

            SimpleAnimationPlayer flashCornerAnimation = new SimpleAnimationPlayer(ap, true, 8, 0.3f);
            drawComponent.AddAnimation("flashCorner", flashCornerAnimation);
            drawComponent.PlayAnimation("flashCorner");
            //

            setComponent(drawComponent);
            physicsComponent = new PhysicsComponent(this);
            physicsComponent.BodyType = PhysicBodyType.Dynamic;
            physicsComponent.Direction = new Vector2((float)(GameRoot.Rnd.Next(-100, 100)), (float)(GameRoot.Rnd.Next(-100, 100)));
            physicsComponent.Velocity = GameRoot.Rnd.Next(100);
            physicsComponent.Collider2D.ColliderType = Collider2DType.Box;
            this.TransformComponent.Position = new Vector2(GameRoot.Rnd.Next(GameRoot.Screen.Width), GameRoot.Rnd.Next(GameRoot.Screen.Height));
            physicsComponent.Collider2D.Size = drawComponent.BoundingBox.Size;
            physicsComponent.Collider2D.Offset = drawComponent.BoundingBox.Center;
            setComponent(physicsComponent);
            setBehavior(new BallInput(this));
        }

        override public void Update()
        {
            base.Update();
        }
    }
}
