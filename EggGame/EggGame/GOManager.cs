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
        private List<Ball> balls = new List<Ball>();
        private Paddle paddle;
        private ContentManager content;

        public GOManager(ContentManager content)
        {
            this.content = content;
        }

        public void LoadContent()
        {
            balls.Add(new Ball(content));
            paddle = new Paddle(content);
        }

        public void Update(GameTime gameTime)
        {
            paddle.Update(gameTime);
            foreach (Ball ball in balls)
            {
                while (ball.moveBallonUnit())
                {
                    ball.checkCollisionToWall();
                    ball.checkCollisionToPaddle(paddle.CurrentRectangle);
                }

                ball.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Ball ball in balls)
                ball.Draw(gameTime, spriteBatch);
            paddle.Draw(gameTime, spriteBatch);
        }

        public void AddBall()
        {
            balls.Add(new Ball(content));
        }
    }
}
