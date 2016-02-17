using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace EggGame
{
    class GOManager
    {
        private SpriteFont arial;
        private List<Ball> balls = new List<Ball>();
        private Paddle paddle;
        private ContentManager content;

        public GOManager(ContentManager content)
        {
            this.content = content;
            arial = content.Load<SpriteFont>("arial");
        }

        public void LoadContent()
        {
            balls.Add(new Ball(content, startPosition: new Vector2(EggGameMain.ScreenRectangle.Center.X, EggGameMain.ScreenRectangle.Bottom - 30)));
            paddle = new Paddle(content);
        }

        public void Update(GameTime gameTime)
        {
            paddle.Update(gameTime);
            for (int a = 0; a < balls.Count; a++)
            {
                while (balls[a].moveBallonUnit())
                {
                    balls[a].checkCollisionToWall();
                    balls[a].checkCollisionToPaddle(paddle.CurrentRectangle);
                    //if (balls.Count > 1)
                    //{
                    //    for (int b = a + 1; b < balls.Count; b++)
                    //        balls[a].checkCollisionToOtherBall(balls[b]);
                    //}
                }
                balls[a].Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int signY = 0;
            foreach (Ball ball in balls)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(arial, "direction: " + ball.direction.X + ", " + ball.direction.Y + "   " + "speed: " + ball.speed, new Vector2(10, signY), Color.White);
                spriteBatch.End();
                ball.Draw(gameTime, spriteBatch);
                signY += 15;
            }
            paddle.Draw(gameTime, spriteBatch);
        }

        public void AddBall()
        {
            balls.Add(new Ball(content, startPosition: new Vector2(EggGameMain.ScreenRectangle.Center.X, EggGameMain.ScreenRectangle.Bottom - 30)));
        }
    }
}
