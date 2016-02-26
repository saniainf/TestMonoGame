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

        int cellSize = 100;

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
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screenSize = this.Window.ClientBounds;
            rnd = new Random();
            texture = Content.Load<Texture2D>("brick");
            arial = Content.Load<SpriteFont>("Arial");
            for (int i = 0; i < 2000; i++)
            {
                gameObject go = new gameObject(new Vector2(rnd.Next(0, screenSize.Width), rnd.Next(0, screenSize.Height)), new Vector2(rnd.Next(-10, 10), rnd.Next(-10, 10)), rnd.Next(20, 100), texture, screenSize);
                gameObjs.Add(go);
            }

        }
        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (gameObject go in gameObjs)
                go.color = Color.White;

            // fill dict
            Clear();
            foreach (gameObject go in gameObjs)
            {
                Insert(go);
            }

            for (int hashId = 0; hashId < cells.Count; hashId++)
            {
                if (cells.ContainsKey(hashId))
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
            spriteBatch.DrawString(arial, cells[14].Count.ToString(), new Vector2(10, 30), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
