using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace EggGame
{
    class Egg
    {
        public Vector2 Direction
        {
            get { return direction; }
            set { direction = Vector2.Normalize(value); }
        }
        public Vector2 Location
        {
            get { return sprite.Location; }
            set { sprite.Location = value; }
        }

        SpriteFont arial;
        private Texture2D texture;
        private Animation idleAnimation;
        private Sprite sprite;
        private float speed;
        private Vector2 direction;

        public Egg(ContentManager content, float speed)
        {
            arial = content.Load<SpriteFont>("arial");
            this.speed = speed;
            direction = new Vector2(0, -1);
            direction.Normalize();
            texture = content.Load<Texture2D>("egg");
            idleAnimation = new Animation(texture, new Rectangle(0, 0, texture.Width, texture.Height), 1, true, 0);
            sprite = new Sprite(idleAnimation, locationVector: new Vector2(EggGameMain.WindowWidth / 2 - texture.Width / 2, EggGameMain.WindowHeight - texture.Height * 3));
        }
        public void Update(GameTime gameTime, Paddle paddle)
        {
            float tmpSpeed = 0;
            if (speed > 1)
                while (tmpSpeed < speed) // чтоб непроскачить при большой скорости
                {
                    sprite.Location += 1f * direction;
                    if (Collision.RectangleToWall(ref direction, sprite.CurrentRectangle))
                        break;
                    if (Collision.RectangleToPaddle(ref direction, sprite.CurrentRectangle, paddle.CurrentRectangle))
                    {
                        sprite.Y = paddle.Location.Y - sprite.Height;
                        break;
                    }
                    tmpSpeed += 1f;
                }
            else
                sprite.Location += speed * direction;

            sprite.Update(gameTime);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(arial, "direction: " + direction.X + ", " + direction.Y, new Vector2(10, 10), Color.White);
            sprite.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
