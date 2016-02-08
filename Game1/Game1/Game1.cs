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
        Texture2D egg;
        Texture2D board;
        Random rand = new Random();
        Rectangle rEgg;
        float speedEgg;
        float angleEgg;
        Vector2 vDirectionEgg;
        Vector2 vPositionEgg;

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
            rEgg = new Rectangle(0, 0, 16, 16);
            wWidth = this.Window.ClientBounds.Width;
            wHeight = this.Window.ClientBounds.Height;
            speedEgg = 5f;
            angleEgg = 0f;
            vDirectionEgg = new Vector2(0, 1);
            vPositionEgg = new Vector2(wWidth / 2, 0);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            egg = Content.Load<Texture2D>("square");
            board = Content.Load<Texture2D>("board");
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
            //newX = (float)(vDirection.X * Math.Cos(angle) - vDirection.Y * Math.Sin(angle));
            //newY = (float)(vDirection.X * Math.Sin(angle) + vDirection.Y * Math.Cos(angle));
            //vDirection.X = newX;
            //vDirection.Y = newY;
            //float h = (float)(1 / Math.Sin(angle));
            newX = (float)Math.Cos(Math.PI / 180 * angleEgg);
            newY = (float)Math.Sin(Math.PI / 180 * angleEgg);
            if (Math.Sign(vDirectionEgg.X) < 0)
                newX = -newX;
            if (Math.Sign(vDirectionEgg.Y) < 0)
                newY = -newY;
            vDirectionEgg.X = newX;
            vDirectionEgg.Y = newY;

            //vDirection.Normalize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                angleEgg -= 1f;
                if (angleEgg < 0)
                    angleEgg = 0;
                changeAngle();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                angleEgg += 1f;
                if (angleEgg > 180)
                    angleEgg = 180;
                changeAngle();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                speedEgg += 0.05f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                speedEgg -= 0.05f;
            }
            if (speedEgg < 0)
                speedEgg = 0;




            if (rEgg.Y >= wHeight - rEgg.Height)
                vDirectionEgg.Y = -vDirectionEgg.Y;
            if (rEgg.Y <= 0)
                vDirectionEgg.Y = Math.Abs(vDirectionEgg.Y);

            if (rEgg.X <= 0)
                vDirectionEgg.X = Math.Abs(vDirectionEgg.X);
            if (rEgg.X > wWidth - rEgg.Width)
                vDirectionEgg.X = -vDirectionEgg.X;

            vPositionEgg += speedEgg * vDirectionEgg;

            rEgg.X = (int)vPositionEgg.X;
            rEgg.Y = (int)vPositionEgg.Y;
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
            spriteBatch.DrawString(arial, "angle: " + angleEgg.ToString(), new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(arial, "speed: " + speedEgg.ToString(), new Vector2(10, 30), Color.White);
            spriteBatch.DrawString(arial, "direction X: " + vDirectionEgg.X.ToString(), new Vector2(10, 50), Color.White);
            spriteBatch.DrawString(arial, "direction Y: " + vDirectionEgg.Y.ToString(), new Vector2(10, 70), Color.White);
            spriteBatch.Draw(egg, rEgg, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
