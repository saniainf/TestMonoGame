using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class DrawModule
    {
        public static DrawModule Instance
        {
            get { return instance ?? (instance = new DrawModule()); }
        }

        private static DrawModule instance;
        private SpriteBatch spriteBatch;

        public DrawModule()
        {

        }

        public void Initizlize()
        {
            spriteBatch = new SpriteBatch(GameRoot.Instance.GraphicsDevice);
        }

        public void Update()
        {

        }

        public void Draw()
        {
            spriteBatch.Begin();
            foreach (Entity e in (EntityManager.Instance.DrawEntities))
                spriteBatch.Draw((e.GetComponent<Drawing>() as Drawing).Sprite, ((e.GetComponent<Transform>()) as Transform).Position, Color.White);
            spriteBatch.End();
        }
    }
}
