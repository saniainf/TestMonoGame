using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class Sprite
    {
        public string Id { get { return id; } set { id = value; } }
        public Rectangle BoundingBox { get { return boundingBox; } set { boundingBox = value; } }
        public Point Offset
        {
            get
            {
                Point offset = boundingBox.Location;
                offset.X = offset.X + texture.Width / 2;
                offset.Y = offset.Y + texture.Height / 2;
                return offset;
            }
            set
            {
                Point offset = value;
                offset.X = offset.X - texture.Width / 2;
                offset.Y = offset.Y - texture.Height / 2;
                boundingBox.Location = offset;
            }
        }
        public Texture2D Texture { get { return texture; } set { texture = value; } }
        public Rectangle SourceRectangle { get { return sourceRectangle; } set { sourceRectangle = value; } }
        public SpriteEffects FlipSprite;

        private string id;
        private Rectangle boundingBox;
        private Texture2D texture;
        private Rectangle sourceRectangle;

        public Sprite(string id, Point offset, Texture2D image, Rectangle sourceRectangle, SpriteEffects spriteEffect)
        {
            this.id = id;
            this.FlipSprite = spriteEffect;
            this.boundingBox = new Rectangle(offset.X - sourceRectangle.Width / 2, offset.Y - sourceRectangle.Height / 2, sourceRectangle.Width, sourceRectangle.Height);
            this.texture = image;
            this.sourceRectangle = sourceRectangle;
        }
    }
}
