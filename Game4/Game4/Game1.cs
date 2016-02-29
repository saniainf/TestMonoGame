using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game4
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D dot;
        Texture2D brick;
        SpriteFont arial;
        Vector2 pointA;
        Vector2 pointB;
        Rectangle rectBrick;
        Vector2 lMidPoint;
        Vector2 lDirection;
        float lHalfLenght;
        float lLenght;
        Color brickColor = new Color();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            arial = Content.Load<SpriteFont>("arialbd");
            dot = Content.Load<Texture2D>("dot");
            brick = Content.Load<Texture2D>("brick");
            rectBrick = new Rectangle(this.Window.ClientBounds.Center.X - brick.Width / 2, this.Window.ClientBounds.Center.Y - brick.Height / 2, brick.Width, brick.Height);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                pointA = Mouse.GetState().Position.ToVector2();
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
                pointB = Mouse.GetState().Position.ToVector2();

            lDirection = pointB - pointA;
            lMidPoint = (lDirection / 2) + pointA;
            lLenght = lDirection.Length();
            lHalfLenght = lDirection.Length() / 2;
            lDirection.Normalize();
            brickColor = Color.White;

            if (overlapsLineSegment())
                brickColor = Color.Red;

            base.Update(gameTime);
        }

        bool overlapsLineSegment()
        {
            Vector2 T = rectBrick.Center.ToVector2() - lMidPoint;
            Vector2 P = rectBrick.Center.ToVector2();
            Vector2 E = new Vector2(rectBrick.Width / 2, rectBrick.Height / 2);

            if ((Math.Abs(T.X) > E.X + lHalfLenght * Math.Abs(lDirection.X)) ||
                (Math.Abs(T.Y) > E.Y + lHalfLenght * Math.Abs(lDirection.Y)))
                return false;
            return true;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(brick, rectBrick, brickColor);
            ///
            Vector2 locat = pointA;
            Vector2 dir = pointB - pointA;
            float distance = dir.Length();
            dir.Normalize();
            float dis = 0;
            while (dis < distance)
            {
                spriteBatch.Draw(dot, locat, Color.White);
                locat += dir;
                dis += dir.Length();
            }
            ///
            spriteBatch.DrawString(arial, pointA.X + ", " + pointA.Y, new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(arial, pointB.X + ", " + pointB.Y, new Vector2(10, 30), Color.White);
            spriteBatch.DrawString(arial, lLenght.ToString() + "  " + lHalfLenght.ToString(), new Vector2(10, 50), Color.White);
            spriteBatch.DrawString(arial, "M", lMidPoint, Color.White);
            spriteBatch.DrawString(arial, "A", pointA, Color.White);
            spriteBatch.DrawString(arial, "B", pointB, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
