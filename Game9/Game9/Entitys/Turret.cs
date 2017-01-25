using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class Turret : Entity, IDrawing, IPhysic
    {
        public DrawComponent DrawComponent { get { return drawComponent; } }
        public PhysicsComponent PhysicsComponent { get { return physicsComponent; } }

        private DrawComponent drawComponent;
        private PhysicsComponent physicsComponent;

        public Turret()
            : base()
        {
            Texture2D textureTurret = Art.GetImage("turret");
            Texture2D maskTurret = Art.GetImage("turret-mask");

            Sprite spriteTurret = new Sprite("turretBase", new Point(0, 0), textureTurret, new Rectangle(0, 0, textureTurret.Width / 8, textureTurret.Height / 8), SpriteEffects.None);
            Sprite turretMask = new Sprite("turretMask", new Point(0, -5), maskTurret, new Rectangle(0, 0, maskTurret.Width / 8, maskTurret.Height / 8), SpriteEffects.None);
            drawComponent = new DrawComponent(this);
            drawComponent.AddSprite(spriteTurret);
            drawComponent.AddSprite(turretMask);

            Rectangle[] tSeq = Inf.RectangleSequence(8, 8, textureTurret.Width, textureTurret.Height);
            Rectangle[] mSeq = Inf.RectangleSequence(8, 8, maskTurret.Width, maskTurret.Height);
            AnimatedSprite[] ap = new AnimatedSprite[2];
            ap[0] = new AnimatedSprite(spriteTurret.Id, tSeq, 0);
            ap[1] = new AnimatedSprite(turretMask.Id, mSeq, 0);

            SimpleAnimationPlayer turretRotation = new SimpleAnimationPlayer(ap, true, 64, 0.03f);
            drawComponent.AddAnimation("turretRotation", turretRotation);
            drawComponent.PlayAnimation("turretRotation");
            //

            setComponent(drawComponent);
            physicsComponent = new PhysicsComponent(this);
            physicsComponent.BodyType = PhysicBodyType.Static;
            physicsComponent.Direction = new Vector2((float)(GameRoot.Rnd.Next(-100, 100)), (float)(GameRoot.Rnd.Next(-100, 100)));
            physicsComponent.Velocity = GameRoot.Rnd.Next(100);
            physicsComponent.Collider2D.ColliderType = Collider2DType.Box;
            this.TransformComponent.Position = new Vector2(GameRoot.Rnd.Next(GameRoot.Screen.Width), GameRoot.Rnd.Next(GameRoot.Screen.Height));
            physicsComponent.Collider2D.Size = drawComponent.BoundingBox.Size;
            physicsComponent.Collider2D.Offset = drawComponent.BoundingBox.Center;
            setComponent(physicsComponent);
            setBehavior(new BallInput(this));
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
