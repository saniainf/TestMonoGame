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
        string str;
        SpriteBatch spriteBatch;
        Texture2D brick, ball;
        Vector2 locationBrick, locationBall;
        Rectangle rectBrick, rectBall;
        int radius;
        Vector2 direction, reflectDirection;

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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            arial = Content.Load<SpriteFont>("arial");
            brick = Content.Load<Texture2D>("brick");
            ball = Content.Load<Texture2D>("ball");
            locationBrick = new Vector2(this.Window.ClientBounds.Center.X - brick.Width / 2, this.Window.ClientBounds.Center.Y - brick.Height / 2);
            locationBall = Vector2.Zero;
            rectBrick = new Rectangle((int)locationBrick.X, (int)locationBrick.Y, brick.Width, brick.Height);
            rectBall = new Rectangle((int)locationBall.X, (int)locationBall.Y, ball.Width, ball.Height);
            radius = ball.Width / 2;
            str = "";
            direction = Vector2.Zero;
            reflectDirection = Vector2.Zero;
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

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                direction.X = -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                direction.X = 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                direction.Y = -1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                direction.Y = 1;
            }
            base.Update(gameTime);
            locationBall = Mouse.GetState().Position.ToVector2();
            rectBall.Location = locationBall.ToPoint();
            str = "";
            CheckCollision();
        }

        void CheckCollision()
        {
            Vector2 pA, pB, pC, pD;
            pA = rectBrick.Location.ToVector2();
            pB = new Vector2(rectBrick.Right, rectBrick.Top);
            pC = new Vector2(rectBrick.Left, rectBrick.Bottom);
            pD = new Vector2(rectBrick.Right, rectBrick.Bottom);

            if (rectBall.Center.X > rectBrick.Left &
                rectBall.Center.X < rectBrick.Right &
                rectBall.Center.Y > rectBrick.Top - radius &
                rectBall.Center.Y < rectBrick.Top)
                str = "Intersect";
            if (rectBall.Center.X > rectBrick.Left &
                rectBall.Center.X < rectBrick.Right &
                rectBall.Center.Y > rectBrick.Bottom &
                rectBall.Center.Y < rectBrick.Bottom + radius)
                str = "Intersect";
            if (rectBall.Center.X > rectBrick.Left - radius &
                rectBall.Center.X < rectBrick.Left &
                rectBall.Center.Y > rectBrick.Top &
                rectBall.Center.Y < rectBrick.Bottom)
                str = "Intersect";
            if (rectBall.Center.X > rectBrick.Right &
                rectBall.Center.X < rectBrick.Right + radius &
                rectBall.Center.Y > rectBrick.Top &
                rectBall.Center.Y < rectBrick.Bottom)
                str = "Intersect";

            if (Vector2.Distance(pA, rectBall.Center.ToVector2()) < radius)
                str = "Intersect";
            if (Vector2.Distance(pB, rectBall.Center.ToVector2()) < radius)
                str = "Intersect";
            if (Vector2.Distance(pC, rectBall.Center.ToVector2()) < radius)
                str = "Intersect";
            if (Vector2.Distance(pD, rectBall.Center.ToVector2()) < radius)
                str = "Intersect";

            if (rectBall.Center.X > rectBrick.Left &
                rectBall.Center.X < rectBrick.Right &
                rectBall.Center.Y > rectBrick.Top &
                rectBall.Center.Y < rectBrick.Bottom)
                str = "Intersect";

        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(brick, locationBrick, Color.White);
            spriteBatch.Draw(ball, locationBall, Color.White);
            spriteBatch.DrawString(arial, str, new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(arial, "direction X: " + direction.X + ", Y: " + direction.Y, new Vector2(10, 30), Color.White);
            spriteBatch.DrawString(arial, "direction X: " + reflectDirection.X + ", Y: " + reflectDirection.Y, new Vector2(10, 50), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
