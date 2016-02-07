using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteFont arial;
        SpriteBatch spriteBatch;
        Texture2D texture1;
        Random rand = new Random();
        Rectangle rect1;
        float speed;
        float angle;
        Vector2 vDirection;
        Vector2 vPosition;

        int wWidth;
        int wHeight;

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

            base.Initialize();
            rect1 = new Rectangle(0, 0, 16, 16);
            wWidth = this.Window.ClientBounds.Width;
            wHeight = this.Window.ClientBounds.Height;
            speed = 1f;
            angle = 0f;
            vDirection = new Vector2(1, 0);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture1 = Content.Load<Texture2D>("square");
            arial = Content.Load<SpriteFont>("arial");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        void changeAngle()
        {
            float newX;
            float newY;
            newX = (float)(vDirection.X * Math.Cos(angle) - vDirection.Y * Math.Sin(angle));
            newY = (float)(vDirection.X * Math.Sin(angle) + vDirection.Y * Math.Cos(angle));
            vDirection.X = newX;
            vDirection.Y = newY;
            vDirection.Normalize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                angle -= 1f;
                changeAngle();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                angle = 1f;
                changeAngle();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                speed += 0.05f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                speed -= 0.05f;
            }
            if (speed < 0)
                speed = 0;




            if (rect1.Y >= wHeight - rect1.Height)
                vDirection.Y = -vDirection.Y;
            if (rect1.Y <= 0)
                vDirection.Y = Math.Abs(vDirection.Y);

            if (rect1.X <= 0)
                vDirection.X = Math.Abs(vDirection.X);
            if (rect1.X > wWidth - rect1.Width)
                vDirection.X = -vDirection.X;

            vPosition += speed * vDirection;

            rect1.X = (int)vPosition.X;
            rect1.Y = (int)vPosition.Y;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.DrawString(arial, "angle: " + angle.ToString(), new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(arial, "speed: " + speed.ToString(), new Vector2(10, 30), Color.White);
            spriteBatch.Draw(texture1, rect1, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
