using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Sprite
    {
        public Vector2 Position
        {
            get { return spritePosition; }
            set { spritePosition = value; }
        }

        private Rectangle spriteRectangle;
        private Vector2 spritePosition;
        private Texture2D spriteTexture;

        public Sprite(Point size, Point position, Texture2D texture)
        {
            this.spriteTexture = texture;
            this.spritePosition = position.ToVector2();
            spriteRectangle = new Rectangle(position, size);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spriteRectangle, Color.White);
        }
    }
}
