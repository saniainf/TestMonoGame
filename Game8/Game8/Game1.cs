using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game8
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle screen;

        SpriteFont arial;
        int fps;
        string fpsS;
        float second;

        Texture2D ballTex;
        Rectangle ballRect;
        Vector2 ballLocation;
        Vector2 ballDirection;
        float speed;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            ballTex = Content.Load<Texture2D>("egg");
            screen = this.Window.ClientBounds;
            ballRect = new Rectangle(0, screen.Height / 2 - ballTex.Height / 2, ballTex.Width, ballTex.Height);
            ballLocation = ballRect.Center.ToVector2();
            ballDirection = new Vector2(1, 0);
            speed = 200;

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
            if (ballRect.Right >= screen.Width)
                ballDirection.X = -1;
            if (ballRect.Left <= 0)
                ballDirection.X = 1;

            ballLocation += speed * ballDirection * (float)gameTime.ElapsedGameTime.TotalSeconds;
            ballRect.Location = ballLocation.ToPoint();
            ballRect.X += ballRect.Width / 2;
            ballRect.Y += ballRect.Height / 2;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            second += (float)gameTime.ElapsedGameTime.TotalSeconds;
            fps += 1;
            if (second >= 1.0f)
            {
                second = 0;
                fpsS = fps.ToString();
                fps = 0;
            }

            spriteBatch.Begin();
            spriteBatch.DrawString(arial, fpsS, new Vector2(20, 20), Color.Black);
            spriteBatch.DrawString(arial, ballRect.X.ToString(), new Vector2(20, 50), Color.Black);
            spriteBatch.Draw(ballTex, ballRect, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
