using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Game8
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameRoot : Game
    {
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public static Rectangle Screen;
        public static GameTime gameTime { get; private set; }
        public static ContentManager GameContent { get; private set; }
        public static Random Rnd = new Random();
        public static int CountBall;

        SpriteFont arial;
        int fps;
        string fpsS;
        float second;

        private List<Ball> balls;

        public GameRoot()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GameRoot.GameContent = Content;
            balls = new List<Ball>();
            this.Window.Title = "List";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            fpsS = "";
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            arial = Content.Load<SpriteFont>("arialbd");
            Screen = this.Window.ClientBounds;
            for (int i = 0; i < 10000; i++)
            {
                Ball ball = new Ball();
                balls.Add(ball);
                GameRoot.CountBall += 1;
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {

            }

            GameRoot.gameTime = gameTime;
            foreach (Ball b in balls)
                b.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            second += (float)gameTime.ElapsedGameTime.TotalSeconds;
            fps += 1;
            if (second >= 1.0f)
            {
                second = 0;
                fpsS = fps.ToString();
                fps = 0;
            }

            spriteBatch.Begin();
            foreach (Ball b in balls)
                b.Draw();
            spriteBatch.DrawString(arial, fpsS, new Vector2(20, 20), Color.Black);
            spriteBatch.DrawString(arial, GameRoot.CountBall.ToString(), new Vector2(20, 50), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
