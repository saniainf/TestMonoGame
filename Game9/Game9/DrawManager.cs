using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class DrawManager
    {
        private static DrawManager instance;
        public static DrawManager Instance
        {
            get { return instance ?? (instance = new DrawManager()); }
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
