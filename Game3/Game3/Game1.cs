using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game3
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D tCell;
        SpriteFont arial;
        List<GameObject> gameObjs = new List<GameObject>();
        int cellSize = 50;
        int wCell = 16;
        int hCell = 12;
        Vector2 pointA;
        Vector2 pointB;
        int indx;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            arial = Content.Load<SpriteFont>("arialbd");
            tCell = Content.Load<Texture2D>("cell");
            for (int i = 0; i < hCell; i++)
                for (int e = 0; e < wCell; e++)
                    gameObjs.Add(new GameObject(new Vector2(e * cellSize, i * cellSize), tCell));
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                pointA = Mouse.GetState().Position.ToVector2();
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
                pointB = Mouse.GetState().Position.ToVector2();

            foreach (GameObject go in gameObjs)
                go.color = Color.White;

            Vector2 start = pointA;
            Vector2 dir = pointB - pointA;
            float distance = dir.Length();
            dir.Normalize();

            Intersection(start, dir, distance);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            foreach (GameObject go in gameObjs)
                go.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(arial, pointA.X + ", " + pointA.Y, new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(arial, pointB.X + ", " + pointB.Y, new Vector2(10, 30), Color.White);
            spriteBatch.DrawString(arial, indx.ToString(), new Vector2(10, 50), Color.White);
            spriteBatch.DrawString(arial, "A", pointA, Color.White);
            spriteBatch.DrawString(arial, "B", pointB, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        void Intersection(Vector2 start, Vector2 dir, float distance)
        {
            int x = (int)Math.Floor(start.X / cellSize);
            int y = (int)Math.Floor(start.Y / cellSize);

            float nearXPlane, nearYPlane;
            int stepX, stepY;
            if (dir.X >= 0)
            {
                nearXPlane = (x + 1) * cellSize;
                stepX = 1;
            }
            else
            {
                nearXPlane = x * cellSize;
                stepX = -1;
            }
            if (dir.Y >= 0)
            {
                nearYPlane = (y + 1) * cellSize;
                stepY = 1;
            }
            else
            {
                nearYPlane = y * cellSize;
                stepY = -1;
            }

            float maxX = (nearXPlane - start.X) / dir.X;
            float maxY = (nearYPlane - start.Y) / dir.Y;

            float deltaX = cellSize / Math.Abs(dir.X);
            float deltaY = cellSize / Math.Abs(dir.Y);

            float dis = 0;
            indx = 0;
            while (dis < distance)
            {
                //checkCell(x, y);
                int i = y * wCell + x;
                if (i >= 0 && i < gameObjs.Count)
                    gameObjs[i].color = Color.Red;

                dis = Math.Min(maxX, maxY);
                if (maxX < maxY)
                {
                    maxX += deltaX;
                    x += stepX;
                }
                else
                {
                    maxY += deltaY;
                    y += stepY;
                }
                indx++;
            }
        }
    }
}
