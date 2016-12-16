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
        public Vector2 Offset { get { return offset; } set { offset = value; } }
        public Texture2D Image { get { return image; } set { image = value; } }

        private Vector2 offset;
        private Texture2D image;

        public Sprite(Vector2 offset, Texture2D image)
        {
            this.offset = offset;
            this.image = image;
        }
    }
}
