using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EggGame
{
    public class Sprite
    {
        public Vector2 Location
        {
            get { return spritePosition; }
            set { spritePosition = value; }
        }

        public int Width
        {
            get { return spriteRectangle.Width; }
            set { spriteRectangle.Width = value; }
        }

        public int Height
        {
            get { return spriteRectangle.Height; }
            set { spriteRectangle.Height = value; }
        }

        private List<Rectangle> frames = new List<Rectangle>();
        private int currentFrame;
        private Rectangle spriteRectangle;
        private Vector2 spritePosition;
        private Texture2D spriteTexture;
        

        public Sprite(Point size, Point location, Texture2D texture)
        {
            this.spriteTexture = texture;
            this.spritePosition = location.ToVector2();
            spriteRectangle = new Rectangle(location, size);
        }

        public virtual void Update(GameTime gameTime)
        {
            spriteRectangle.Location = spritePosition.ToPoint();
        }

        public virtual void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spriteRectangle, Color.White);
        }
    }
}
