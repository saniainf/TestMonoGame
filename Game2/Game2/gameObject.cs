using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game2
{
    class gameObject
    {
        Vector2 location;
        Vector2 position;
        Vector2 direction;
        float speed;
        Texture2D texture;
        Rectangle screenSize;
        public Color color;
        public Rectangle rect;

        public gameObject(Vector2 lct, Vector2 drc, float spd, Texture2D txr, Rectangle screen)
        {
            location = lct;
            direction = drc;
            direction.Normalize();
            speed = spd;
            texture = txr;
            screenSize = screen;
            color = Color.White;
            rect = new Rectangle((int)Math.Round(location.X), (int)Math.Round(location.Y), texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            location += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X = (float)Math.Round(location.X);
            position.Y = (float)Math.Round(location.Y);
            rect.X = (int)position.X;
            rect.Y = (int)position.Y;

            if (location.X <= 0)
                direction.X = Math.Abs(direction.X);
            if (location.Y <= 0)
                direction.Y = Math.Abs(direction.Y);
            if (location.X + texture.Width >= screenSize.Right)
                direction.X = -Math.Abs(direction.X);
            if (location.Y + texture.Height >= screenSize.Bottom)
                direction.Y = -Math.Abs(direction.Y);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);
        }
    }
}
