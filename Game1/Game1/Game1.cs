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
        Rectangle rEgg;
        Rectangle rBoard;
        float speedEgg;
        float angleEgg;
        Vector2 vDirectionEgg;
        Vector2 vPositionEgg;
        Vector2 vPositionBoard;
        float ratio;

        int tmpX;

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
            wWidth = this.Window.ClientBounds.Width;
            wHeight = this.Window.ClientBounds.Height;
            speedEgg = 5f;
            angleEgg = 90f;
            rEgg = new Rectangle(0, 0, 16, 16);
            rBoard = new Rectangle(0, 0, 96, 16);
            vDirectionEgg = new Vector2(0, 0);
            vPositionEgg = new Vector2(wWidth / 2 - rEgg.Width / 2, 0);
            vPositionBoard = new Vector2(wWidth / 2 - rBoard.Width / 2, wHeight - board.Height * 2);
            ratio = (70f / (rBoard.Width / 2f));
            changeAngle();
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
            newX = (float)Math.Cos(Math.PI / 180 * angleEgg);
            newY = (float)Math.Sin(Math.PI / 180 * angleEgg);
            if (Math.Sign(vDirectionEgg.Y) < 0)
                newY = -newY;
            vDirectionEgg.X = newX;
            vDirectionEgg.Y = newY;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                vPositionBoard.X -= 6f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                vPositionBoard.X += 6f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                speedEgg += 0.05f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                speedEgg -= 0.05f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                angleEgg = 90f;
                changeAngle();
            }
            if (speedEgg < 0)
                speedEgg = 0;

            if (rEgg.Y >= wHeight - rEgg.Height)
                vDirectionEgg.Y = -(Math.Abs(vDirectionEgg.Y));
            if (rEgg.Y <= 0)
                vDirectionEgg.Y = Math.Abs(vDirectionEgg.Y);
            if (rEgg.X <= 0)
                vDirectionEgg.X = Math.Abs(vDirectionEgg.X);
            if (rEgg.X > wWidth - rEgg.Width)
                vDirectionEgg.X = -(Math.Abs(vDirectionEgg.X));

            if (vPositionBoard.X > wWidth - rBoard.Width)
                vPositionBoard.X = wWidth - rBoard.Width;
            if (vPositionBoard.X < 0)
                vPositionBoard.X = 0;

            vPositionEgg += speedEgg * vDirectionEgg;

            rBoard.X = (int)vPositionBoard.X;
            rBoard.Y = (int)vPositionBoard.Y;
            rEgg.X = (int)vPositionEgg.X;
            rEgg.Y = (int)vPositionEgg.Y;

            if (rEgg.Intersects(rBoard))
            {
                vDirectionEgg.Y = -(Math.Abs(vDirectionEgg.Y));
                tmpX = rBoard.Center.X - rEgg.Center.X;
                if (tmpX != 0)
                {
                    if (tmpX > (rBoard.Width / 2f) || tmpX < -(rBoard.Width / 2f))
                        tmpX = (int)(rBoard.Width / 2f) * Math.Sign(tmpX);
                    angleEgg = tmpX * ratio;
                    angleEgg = 90f + angleEgg;
                    changeAngle();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.DrawString(arial, "angle: " + angleEgg.ToString(), new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(arial, "ratio: " + ratio.ToString() + ", " + tmpX, new Vector2(10, 30), Color.White);
            spriteBatch.DrawString(arial, "speed: " + speedEgg.ToString(), new Vector2(10, 50), Color.White);
            spriteBatch.DrawString(arial, "direction X: " + vDirectionEgg.X.ToString(), new Vector2(10, 70), Color.White);
            spriteBatch.DrawString(arial, "direction Y: " + vDirectionEgg.Y.ToString(), new Vector2(10, 90), Color.White);
            spriteBatch.Draw(egg, new Vector2(rEgg.X + 2, rEgg.Y + 2), Color.Black);
            spriteBatch.Draw(board, new Vector2(rBoard.X + 3, rBoard.Y + 3), Color.Black);
            spriteBatch.Draw(egg, rEgg, Color.White);
            spriteBatch.Draw(board, rBoard, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
