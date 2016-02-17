using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace EggGame
{
    class Ball : Sprite
    {
        private SpriteFont arial;
        public float speed;
        private float tmpSpeed;
        public Vector2 direction;
        public Animation idleAnimation;
        private float ratio;

        public Ball(ContentManager content, Vector2? startPosition = null)
            : base()
        {
            speed = 5f;

            direction = new Vector2(0, -1);
            direction.Normalize();
            spriteSheet = content.Load<Texture2D>("egg");
            arial = content.Load<SpriteFont>("arial");
            idleAnimation = new Animation(spriteSheet, new Rectangle(0, 0, spriteSheet.Width, spriteSheet.Height), 1, true, 0);
            locationVector = startPosition ?? new Vector2(200, 200);
            LoadAnimation(idleAnimation);
        }

        public override void LoadAnimation(Animation animation)
        {
            base.LoadAnimation(animation);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public bool moveBallonUnit()
        {
            if (tmpSpeed < speed)
            {
                tmpSpeed++;
                locationVector += 1f * direction;
                return true;
            }
            else
            {
                tmpSpeed = 0;
                return false;
            }
        }

        public void checkCollisionToWall()
        {
            if (CurrentRectangle.Bottom > EggGameMain.ScreenRectangle.Bottom)
            {
                direction = Vector2.Reflect(direction, new Vector2(0, 1f));
            }
            else if (CurrentRectangle.Top < EggGameMain.ScreenRectangle.Top)
            {
                direction = Vector2.Reflect(direction, new Vector2(0, -1f));
            }
            else if (CurrentRectangle.Left < EggGameMain.ScreenRectangle.Left)
            {
                direction = Vector2.Reflect(direction, new Vector2(1, 0f));
            }
            else if (CurrentRectangle.Right > EggGameMain.ScreenRectangle.Right)
            {
                direction = Vector2.Reflect(direction, new Vector2(-1, 0f));
            }
        }

        public void checkCollisionToPaddle(Rectangle rectangle)
        {
            if (CurrentRectangle.Intersects(rectangle))
            {
                Vector2 normal = new Vector2(0, -1);
                Vector2 secondVector = Vector2.Zero;
                if (CurrentRectangle.Center.X < rectangle.Center.X)
                    secondVector = new Vector2(-1, 0);
                if (CurrentRectangle.Center.X > rectangle.Center.X)
                    secondVector = new Vector2(1, 0);
                ratio = (float)Math.Abs((CurrentRectangle.Center.X - rectangle.Center.X) * 0.011);
                locationVector.Y = rectangle.Top - CurrentRectangle.Height;
                direction = Vector2.Lerp(normal, secondVector, ratio);
                direction.Normalize();
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(arial, "direction: " + direction.X + ", " + direction.Y, new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(arial, "ratio: " + ratio, new Vector2(10, 50), Color.White);
            spriteBatch.End();

            base.Draw(gameTime, spriteBatch);
        }
    }
}
