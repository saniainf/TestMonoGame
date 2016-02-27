using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle screenSize;
        Texture2D texture;
        Random rnd;
        SpriteFont arial;
        Vector2 pointA;
        Vector2 pointB;

        int cellSize = 50;

        Dictionary<int, List<gameObject>> cells = new Dictionary<int, List<gameObject>>();
        Dictionary<gameObject, int> objects = new Dictionary<gameObject, int>();

        List<gameObject> gameObjs = new List<gameObject>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screenSize = this.Window.ClientBounds;
            rnd = new Random();
            texture = Content.Load<Texture2D>("brick");
            arial = Content.Load<SpriteFont>("arialbd");
            for (int i = 0; i < 2000; i++)
            {
                gameObject go = new gameObject(new Vector2(rnd.Next(0, screenSize.Width), rnd.Next(0, screenSize.Height)), new Vector2(rnd.Next(-10, 10), rnd.Next(-10, 10)), rnd.Next(20, 100), texture, screenSize);
                gameObjs.Add(go);
            }

        }
        protected override void UnloadContent()
        {
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

            while (dis < distance)
            {
                checkCell(x, y);

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
            }
        }

        void checkCell(int x, int y)
        {
            int hashId = x + y * screenSize.Width / cellSize;
            if (hashId >= 0 && cells.ContainsKey(hashId))
            {
                for (int i = 0; i < cells[hashId].Count; i++)
                {
                    for (int e = i + 1; e < cells[hashId].Count; e++)
                    {
                        if (cells[hashId][i].rect.Intersects(cells[hashId][e].rect))
                        {
                            cells[hashId][i].color = Color.Red;
                            cells[hashId][e].color = Color.Red;
                        }
                    }
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                pointA = Mouse.GetState().Position.ToVector2();
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
                pointB = Mouse.GetState().Position.ToVector2();

            foreach (gameObject go in gameObjs)
                go.color = Color.White;

            // fill dict
            Clear();
            foreach (gameObject go in gameObjs)
                Insert(go);

            Vector2 start = pointA;
            Vector2 dir = pointB - pointA;
            float distance = dir.Length();
            dir.Normalize();

            Intersection(start, dir, distance);


            //for (int hashId = 0; hashId < cells.Count; hashId++)
            //{
            //    if (cells.ContainsKey(hashId))
            //    {
            //        for (int i = 0; i < cells[hashId].Count; i++)
            //        {
            //            for (int e = i + 1; e < cells[hashId].Count; e++)
            //            {
            //                if (cells[hashId][i].rect.Intersects(cells[hashId][e].rect))
            //                {
            //                    cells[hashId][i].color = Color.Red;
            //                    cells[hashId][e].color = Color.Red;
            //                }
            //            }
            //        }
            //    }
            //}

            foreach (gameObject go in gameObjs)
                go.Update(gameTime);

            base.Update(gameTime);
        }

        private void Clear()
        {
            int[] keys = cells.Keys.ToArray();
            for (int i = 0; i < keys.Count(); i++)
                cells[keys[i]].Clear();
            objects.Clear();
        }

        void Insert(gameObject insGo)
        {
            int hashId = ((int)Math.Floor((float)insGo.rect.X / cellSize)) + ((int)Math.Floor((float)insGo.rect.Y / cellSize)) * screenSize.Width / cellSize;
            if (cells.ContainsKey(hashId))
                cells[hashId].Add(insGo);
            else
                cells[hashId] = new List<gameObject> { insGo };
            objects[insGo] = hashId;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            foreach (gameObject go in gameObjs)
                go.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(arial, gameTime.ElapsedGameTime.TotalSeconds.ToString(), new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(arial, "pointA: " + pointA.X + ", " + pointA.Y, new Vector2(10, 30), Color.White);
            spriteBatch.DrawString(arial, "pointB: " + pointB.X + ", " + pointB.Y, new Vector2(10, 50), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
