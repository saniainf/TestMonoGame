using Microsoft.Xna.Framework;
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

        public DrawManager()
        {

        }

        public void Initizlize()
        {

        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (Entity e in (EntityManager.Instance.DrawEntities))
                spriteBatch.Draw(((Drawing)e.GetComponent<Drawing>()).Sprite, ((Transform)e.GetComponent<Transform>()).Position, Color.White);

            spriteBatch.End();
        }
    }
}
