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
        Rectangle brickRect;
        Texture2D ball;
        Rectangle ballRect;
        Vector2 locationBall;
        float radius;
        string intersect;
        string location;
        SpriteFont arial;

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
            this.IsMouseVisible = true;
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
            brickRect = new Rectangle(this.Window.ClientBounds.Width / 2 - brick.Width / 2, this.Window.ClientBounds.Height / 2 - brick.Height / 2, brick.Width, brick.Height);
            ballRect = new Rectangle(0, 0, ball.Width, ball.Height);
            radius = ball.Width / 2;
            intersect = "";
            location = "";
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
            location = "";
            locationBall = Mouse.GetState().Position.ToVector2();
            ballRect.Location = new Point((int)(locationBall.X - radius), (int)(locationBall.Y - radius));
            if (cross())
            {
                intersect = "TRUE";
                if (ballRect.Center.Y > brickRect.Bottom)
                    location = "BOT";
                else if (ballRect.Center.Y < brickRect.Top)
                    location = "TOP";
                else if (ballRect.Center.X < brickRect.Left)
                    location = "LEFT";
                else if (ballRect.Center.X > brickRect.Right)
                    location = "RIGHT";
            }

            base.Update(gameTime);
        }

        //bool cross()
        //{
        //    //if ((Math.Abs(ballRect.Center.X - brickRect.Center.X)) < brickRect.Width / 2 + radius)
        //    //{
        //    //    if ((Math.Abs(ballRect.Center.Y - brickRect.Center.Y)) < brickRect.Height / 2 + radius)
        //    //        return true;
        //    //}
        //    if (!brickRect.Intersects(ballRect))
        //        return false;
        //    return
        //        radius >= distance(ballRect.Center, new Point(brickRect.Left, brickRect.Top)) ||
        //        radius >= distance(ballRect.Center, new Point(brickRect.Left, brickRect.Bottom)) ||
        //        radius >= distance(ballRect.Center, new Point(brickRect.Right, brickRect.Top)) ||
        //        radius >= distance(ballRect.Center, new Point(brickRect.Right, brickRect.Bottom))||
        //        brickRect.Intersects(ballRect);
        //}

        bool cross()
        {
            Point circleDistance;
            circleDistance.X = Math.Abs(ballRect.Center.X - brickRect.Center.X);
            circleDistance.Y = Math.Abs(ballRect.Center.Y - brickRect.Center.Y);

            if (circleDistance.X > (brickRect.Width / 2 + radius))
                return false;
            if (circleDistance.Y > (brickRect.Height / 2 + radius))
                return false;

            if (circleDistance.X <= (brickRect.Width / 2))
                return true;
            if (circleDistance.Y <= (brickRect.Height / 2))
                return true;

            float cornerDistance_sq = (circleDistance.X - brickRect.Width / 2) * (circleDistance.X - brickRect.Width / 2) + (circleDistance.Y - brickRect.Height / 2) * (circleDistance.Y - brickRect.Height / 2);

            return (cornerDistance_sq <= (radius * radius));
        }

        int distance(Point p1, Point p2)
        {
            return Convert.ToInt16(Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y)));
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
            spriteBatch.Draw(ball, ballRect, Color.White);
            spriteBatch.DrawString(arial, intersect, new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(arial, location, new Vector2(10, 30), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
