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
        public static DrawManager Instance
        {
            get { return instance ?? (instance = new DrawManager()); }
        }
        private static DrawManager instance;

        private SpriteBatch spriteBatch;

        int fps;
        string fpsS;
        float second;

        public DrawManager()
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

            //foreach (IDrawing e in (EntityManager.Instance.DrawEntities()))
            //    foreach (Sprite s in (e.DrawComponent.GetSprites()))
            //    {
            //        Vector2 position = Vector2.Add(e.TransformComponent.Position, s.BoundingBox.Location.ToVector2());
            //        position.X += 2;
            //        position.Y += 2;
            //        spriteBatch.Draw(s.Image, position, null, s.SourceRectangle, null, 0f, null, Color.Black, s.FlipSprite, 0f);
            //    }

            foreach (IDrawing e in (EntityManager.Instance.DrawEntities()))
                foreach (Sprite s in (e.DrawComponent.GetSprites()))
                {
                    Vector2 position = Vector2.Add(e.TransformComponent.Position, s.BoundingBox.Location.ToVector2());
                    position = position.ToPoint().ToVector2();
                    spriteBatch.Draw(s.Texture, position, null, s.SourceRectangle, null, 0f, null, Color.White, s.FlipSprite, 0f);
                }

            //foreach (IDrawing e in (EntityManager.Instance.DrawEntities()))
            //{
            //    spriteBatch.DrawString(Art.GetFont("arial"), e.DrawComponent.BoundingBox.Size.ToString(), e.TransformComponent.Position, Color.Black);
            //    spriteBatch.DrawString(Art.GetFont("arial"), e.DrawComponent.BoundingBox.Location.ToString(), new Vector2(e.TransformComponent.Position.X, e.TransformComponent.Position.Y + 15), Color.Black);
            //    spriteBatch.DrawString(Art.GetFont("arial"), e.DrawComponent.BoundingBox.Center.ToString(), new Vector2(e.TransformComponent.Position.X, e.TransformComponent.Position.Y + 30), Color.Black);
            //}

            spriteBatch.DrawString(Art.GetFont("arial"), "fps: " + fpsS, new Vector2(20, 20), Color.Black);
            spriteBatch.DrawString(Art.GetFont("arial"), EntityManager.Instance.EntityCount.ToString(), new Vector2(20, 40), Color.Red);
            spriteBatch.End();
        }
    }
}
