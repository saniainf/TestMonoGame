using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace breakOut
{
    class Sprite
    {
        public Texture2D Texture;
        public float Rotaion;

        public Vector2 Size
        {
            get { return Texture == null ? Vector2.Zero : new Vector2(Texture.Width, Texture.Height); }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
