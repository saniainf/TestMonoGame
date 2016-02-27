using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game3
{
    class GameObject
    {
        Vector2 location;
        Texture2D texture;
        public Color color;
        public Rectangle rect;

        public GameObject(Vector2 lct, Texture2D txr)
        {
            location = lct;
            texture = txr;
            color = Color.White;
            rect = new Rectangle((int)Math.Round(location.X), (int)Math.Round(location.Y), texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location, color);
        }
    }
}
