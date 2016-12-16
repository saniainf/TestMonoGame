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

        int fps;
        string fpsS;
        float second;

        public DrawModule()
        {

        }

        public void Initizlize()
        {
            spriteBatch = new SpriteBatch(GameRoot.Instance.GraphicsDevice);
            fpsS = "";
        }

        public void Update()
        {

        }

        public void Draw()
        {
            second += (float)GameRoot.ThisGameTime.ElapsedGameTime.TotalSeconds;
            fps += 1;
            if (second >= 1.0f)
            {
                second = 0;
                fpsS = fps.ToString();
                fps = 0;
            }

            spriteBatch.Begin();

            foreach (IDraw e in (EntityManager.Instance.DrawEntities))
                foreach (Sprite s in (e.DrawComponent.GetSprite()))
                    spriteBatch.Draw(s.Image, (s.Offset + new Vector2(5f, 3f)), Color.Black);

            foreach (IDraw e in (EntityManager.Instance.DrawEntities))
                foreach (Sprite s in (e.DrawComponent.GetSprite()))
                    spriteBatch.Draw(s.Image, s.Offset, Color.White);

            spriteBatch.DrawString(Art.GetFont("arial"), fpsS, new Vector2(20, 20), Color.Black);
            spriteBatch.DrawString(Art.GetFont("arial"), EntityManager.Instance.Entities.Count.ToString(), new Vector2(20, 40), Color.Black);
            spriteBatch.End();
        }
    }
}
