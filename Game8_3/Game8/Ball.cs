using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game8
{
    class Ball
    {
        SpriteFont arial;
        Texture2D ballTex;
        Rectangle ballRect;
        Vector2 ballLocation;
        Vector2 ballDirection;
        float speed;
        SpriteBatch spriteBatch;

        public Ball()
        {
            ballTex = GameRoot.GameContent.Load<Texture2D>("egg");
            arial = GameRoot.GameContent.Load<SpriteFont>("arialbd");
            ballRect = new Rectangle(0, GameRoot.Screen.Height / 2 - ballTex.Height / 2, ballTex.Width, ballTex.Height);
            ballLocation = ballRect.Center.ToVector2();
            ballDirection = new Vector2((float)(GameRoot.Rnd.NextDouble()), (float)(GameRoot.Rnd.NextDouble()));
            speed = 500;
            spriteBatch = new SpriteBatch(GameRoot.gd);
        }

        public void Update()
        {
            if (ballRect.Right >= GameRoot.Screen.Width)
                ballDirection.X = -Math.Abs(ballDirection.X);
            if (ballRect.Left <= 0)
                ballDirection.X = Math.Abs(ballDirection.X);
            if (ballRect.Bottom >= GameRoot.Screen.Height)
                ballDirection.Y = -Math.Abs(ballDirection.Y);
            if (ballRect.Top <= 0)
                ballDirection.Y = Math.Abs(ballDirection.Y);

            ballDirection.Normalize();
            ballLocation += speed * ballDirection * (float)GameRoot.gameTime.ElapsedGameTime.TotalSeconds;
            ballRect.Location = ballLocation.ToPoint();
            ballRect.X += ballRect.Width / 2;
            ballRect.Y += ballRect.Height / 2;
            //for (int i = 1; i < 100; i++)
            //{
            //    Math.Sqrt(i);
            //}
            //GameRoot.gd.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(ballTex, ballRect, Color.White);
            spriteBatch.End();
        }

        public void Draw()
        {
            //GameRoot.spriteBatch.DrawString(arial, ballDirection.X + " | " + ballDirection.Y, new Vector2(20, 50), Color.Black);
            //GameRoot.gd.Clear(Color.CornflowerBlue);
            //spriteBatch.Begin();
            //spriteBatch.Draw(ballTex, ballRect, Color.White);
            //spriteBatch.End();
        }
    }
}
