using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game5
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D brick;
        Texture2D ball;
        Rectangle brickRect;
        Vector2 locationBall;
        float radius;
        Vector2 center;
        SpriteFont arial;
        string intersect;
        Vector2 reflect;
        float distanceSquare;

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
            //this.IsMouseVisible = true;
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
            brick = Content.Load<Texture2D>("brick");
            ball = Content.Load<Texture2D>("ball");
            arial = Content.Load<SpriteFont>("arialbd");
            brickRect = new Rectangle(this.Window.ClientBounds.Center.X - brick.Width / 2, this.Window.ClientBounds.Center.Y - brick.Height / 2, brick.Width, brick.Height);
            intersect = "";
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
            intersect = "";
            locationBall = Mouse.GetState().Position.ToVector2();
            center = new Vector2(locationBall.X + ball.Width / 2, locationBall.Y + ball.Height / 2);
            radius = ball.Width / 2;

            Vector2 point = new Vector2(MathHelper.Clamp(center.X, brickRect.Left, brickRect.Right), MathHelper.Clamp(center.Y, brickRect.Top, brickRect.Bottom));
            Vector2 direction = center - point;
            Vector2 normal = Vector2.Normalize(direction);
            distanceSquare = direction.LengthSquared();
            if (distanceSquare > 0 && distanceSquare < radius * radius)
            {
                intersect = "TRUE";
                reflect = Vector2.Reflect(direction, normal);
            }
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
            spriteBatch.Draw(brick, brickRect, Color.White);
            spriteBatch.Draw(ball, locationBall, Color.White);
            spriteBatch.DrawString(arial, intersect, new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(arial, distanceSquare.ToString(), new Vector2(10, 30), Color.White);
            spriteBatch.DrawString(arial, "R", reflect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
