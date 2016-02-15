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
        private int windowWidth;
        private int windowHeight;
        private float speed;
        private Vector2 direction;

        public Egg(ContentManager content, int wWidth, int wHeight)
        {
            arial = content.Load<SpriteFont>("arial");
            speed = 5f;
            direction = new Vector2(0, -1);
            direction.Normalize();
            windowHeight = wHeight;
            windowWidth = wWidth;
            texture = content.Load<Texture2D>("egg");
            idleAnimation = new Animation(texture, new Rectangle(0, 0, texture.Width, texture.Height), 1, true, 0);
            sprite = new Sprite(idleAnimation, locationVector: new Vector2(windowWidth / 2 - texture.Width / 2, windowHeight - texture.Height * 2));
        }
        public void Update(GameTime gameTime)
        {
            if (sprite.Y >= windowHeight - sprite.Height)
            {
                sprite.Y = windowHeight - sprite.Height;
                direction = Vector2.Reflect(direction, new Vector2(0, 1f));
            }
            if (sprite.Y <= 0)
            {
                sprite.Y = 0;
                direction = Vector2.Reflect(direction, new Vector2(0, -1f));
            }
            if (sprite.Location.X <= 0)
            {
                sprite.X = 0;
                direction = Vector2.Reflect(direction, new Vector2(1, 0f));
            }
            if (sprite.Location.X > windowWidth - sprite.Width)
            {
                sprite.X = windowWidth - sprite.Width;
                direction = Vector2.Reflect(direction, new Vector2(-1, 0f));
            }

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
