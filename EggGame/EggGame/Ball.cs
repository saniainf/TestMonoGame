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

        public Ball(ContentManager content, Vector2? startPosition = null)
            : base()
        {
            speed = 4f;

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
            else if (CurrentRectangle.Top < 0)
            {
                direction = Vector2.Reflect(direction, new Vector2(0, -1f));
            }
            else if (CurrentRectangle.Left < 0)
            {
                direction = Vector2.Reflect(direction, new Vector2(1, 0f));
            }
            else if (CurrentRectangle.Right > EggGameMain.ScreenRectangle.Right)
            {
                direction = Vector2.Reflect(direction, new Vector2(-1, 0f));
            }
            //Collision.RectangleToWall(ref ball.direction, ball.CurrentRectangle);
            //if (Collision.RectangleToPaddle(ref ball.direction, ball.CurrentRectangle, paddle.CurrentRectangle))
            //    ball.locationVector.Y = paddle.locationVector.Y - ball.CurrentRectangle.Height;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(arial, "direction: " + direction.X + ", " + direction.Y, new Vector2(10, 10), Color.White);
            spriteBatch.End();

            base.Draw(gameTime, spriteBatch);
        }
    }
}
