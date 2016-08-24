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
        private List<Brick> bricks = new List<Brick>();
        private Paddle paddle;
        private ContentManager content;

        public GOManager(ContentManager content)
        {
            this.content = content;
            arial = content.Load<SpriteFont>("arial");
        }

        public void LoadContent()
        {
            balls.Add(new Ball(content, startPosition: new Vector2(EggGameMain.ScreenRectangle.Width / 2, EggGameMain.ScreenRectangle.Height - 35)));
            for (int i = 0; i < 8; i++)
                for (int e = 0; e < 10; e++)
                    bricks.Add(new Brick(content, location: new Vector2(64 * e + 50, 32 * i + 50)));
            paddle = new Paddle(content, startPosition: new Vector2(EggGameMain.ScreenRectangle.Width / 2, EggGameMain.ScreenRectangle.Height - 30));
        }

        public void Update(GameTime gameTime)
        {
            bool brickOff;
            brickOff = false;
            paddle.Update(gameTime);
            for (int a = 0; a < balls.Count; a++)
            {
                while (balls[a].moveBallonUnit(gameTime))
                {
                    balls[a].checkCollisionToWall();
                    balls[a].checkCollisionToPaddle(paddle.CurrentRectangle);
                    if (!brickOff)
                        foreach (Brick brick in bricks)
                        {
                            if (brick.isEnable)
                                if (balls[a].checkCollisionToBrick(brick.CurrentRectangle))
                                {
                                    brick.isEnable = false;
                                    brick.Update(gameTime);
                                    //brickOff = true;
                                    break;
                                }
                        }
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
            paddle.Draw(gameTime, spriteBatch);
            foreach (Brick brick in bricks)
            {
                brick.Draw(gameTime, spriteBatch);
            }
            int signY = 0;
            foreach (Ball ball in balls)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(arial, "direction: " + ball.direction.X + ", " + ball.direction.Y + "   " + "speed: " + ball.speed, new Vector2(10, signY), Color.Black);
                spriteBatch.End();
                ball.Draw(gameTime, spriteBatch);
                signY += 15;
            }
        }

        public void AddBall()
        {
            balls.Add(new Ball(content, startPosition: new Vector2(EggGameMain.ScreenRectangle.Width / 2, EggGameMain.ScreenRectangle.Height - 30)));
        }

        public void ballTest()
        {
            foreach (Ball ball in balls)
            {
                ball.direction = new Vector2(ball.direction.X, 0);
            }
        }
    }
}
