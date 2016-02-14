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
        private Texture2D texture;
        private Animation idleAnimation;
        private Sprite sprite;
        private int windowWidth;
        private int windowHeight;
        private float speed;
        private float angle;
        private Vector2 direction;
        private Vector2 location;

        public Egg(ContentManager content, int wWidth, int wHeight)
        {
            angle = 90f;
            speed = 5f;
            direction = new Vector2(1,1);
            location = Vector2.Zero;
            windowHeight = wHeight;
            windowWidth = wWidth;
            texture = content.Load<Texture2D>("egg");
            idleAnimation = new Animation(texture, new Rectangle(0, 0, texture.Width, texture.Height), 1, true, 0);
            sprite = new Sprite(idleAnimation);
        }
        public void Update(GameTime gameTime)
        {
            sprite.Location += speed * direction;
            sprite.Update(gameTime);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            sprite.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
