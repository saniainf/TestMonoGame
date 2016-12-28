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
        public Rectangle BoundingBox { get { return boundingBox; } set { boundingBox = value; } }
        public Texture2D Image { get { return image; } set { image = value; } }
        public SpriteEffects FlipSprite;

        private Rectangle boundingBox;
        private Texture2D image;

        public Sprite(Point offset, Texture2D image, SpriteEffects spriteEffect)
        {
            this.FlipSprite = spriteEffect;
            this.boundingBox = new Rectangle(offset.X - image.Width / 2, offset.Y - image.Height / 2, image.Width, image.Height);
            this.image = image;
        }
    }
}
