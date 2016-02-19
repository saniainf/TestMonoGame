using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace EggGame
{
    class Ball : Sprite
    {
        public float speed;
        private float tmpSpeed;
        private float oneUnit;
        public Vector2 direction;
        public Animation idleAnimation;
        private float ratio;
        public bool imCollisionCheck;
        public int radius;

        public Ball(ContentManager content, Vector2? startPosition = null)
            : base()
        {
            speed = 200f;
            oneUnit = 100f;

            direction = new Vector2(0, -1);
            direction.Normalize();
            spriteSheet = content.Load<Texture2D>("egg");
            idleAnimation = new Animation(spriteSheet, new Rectangle(0, 0, spriteSheet.Width, spriteSheet.Height), 1, true, 0);
            locationVector = startPosition ?? new Vector2(200, 200);
            LoadAnimation(idleAnimation);
            imCollisionCheck = false;
            radius = spriteSheet.Width / 2;
        }

        public override void LoadAnimation(Animation animation)
        {
            base.LoadAnimation(animation);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public bool moveBallonUnit(GameTime gameTime)
        {
            if (tmpSpeed < speed)
            {
                tmpSpeed += oneUnit;
                locationVector += direction * (float)gameTime.ElapsedGameTime.TotalSeconds * oneUnit;
                return true;
            }
            else
            {
                tmpSpeed = 0;
                return false;
            }
        }

        public void checkCollisionToWall()
        {
            if (CurrentRectangle.Bottom >= EggGameMain.ScreenRectangle.Bottom)
            {
                direction = Vector2.Reflect(direction, new Vector2(0, 1f));
            }
            else if (CurrentRectangle.Top <= EggGameMain.ScreenRectangle.Top)
            {
                direction = Vector2.Reflect(direction, new Vector2(0, -1f));
            }
            else if (CurrentRectangle.Left <= EggGameMain.ScreenRectangle.Left)
            {
                direction = Vector2.Reflect(direction, new Vector2(1, 0f));
            }
            else if (CurrentRectangle.Right >= EggGameMain.ScreenRectangle.Right)
            {
                direction = Vector2.Reflect(direction, new Vector2(-1, 0f));
            }
        }

        public void checkCollisionToPaddle(Rectangle rectangle)
        {
            if (CurrentRectangle.Intersects(rectangle))
            {
                Vector2 normal = new Vector2(0, -1);
                Vector2 secondVector = Vector2.Zero;
                if (CurrentRectangle.Center.X < rectangle.Center.X)
                    secondVector = new Vector2(-1, 0);
                if (CurrentRectangle.Center.X > rectangle.Center.X)
                    secondVector = new Vector2(1, 0);
                ratio = (float)Math.Abs((CurrentRectangle.Center.X - rectangle.Center.X) * 0.011);
                locationVector.Y = rectangle.Top - CurrentRectangle.Height;
                direction = Vector2.Lerp(normal, secondVector, ratio);
                direction.Normalize();
            }
        }

        public bool checkCollisionToBrick(Rectangle rectBrick)
        {
            float bRadius = Vector2.Distance(rectBrick.Center.ToVector2(), rectBrick.Location.ToVector2()) + radius;

            if (Vector2.Distance(rectBrick.Center.ToVector2(), Center) <= bRadius)
            {
                Vector2 pA, pB, pC, pD;
                Vector2 pATop, pALeft;
                Vector2 pBTop, pBRight;
                Vector2 pCLeft, pCBottom;
                Vector2 pDRight, pDBottom;
                pA = rectBrick.Location.ToVector2();
                pB = new Vector2(rectBrick.Right, rectBrick.Top);
                pC = new Vector2(rectBrick.Left, rectBrick.Bottom);
                pD = new Vector2(rectBrick.Right, rectBrick.Bottom);
                pATop = new Vector2(rectBrick.Left, rectBrick.Top - radius);
                pALeft = new Vector2(rectBrick.Left - radius, rectBrick.Top);
                pBTop = new Vector2(rectBrick.Right, rectBrick.Top - radius);
                pBRight = new Vector2(rectBrick.Right + radius, rectBrick.Top);
                pCLeft = new Vector2(rectBrick.Left - radius, rectBrick.Bottom);
                pCBottom = new Vector2(rectBrick.Left, rectBrick.Bottom + radius);
                pDRight = new Vector2(rectBrick.Right + radius, rectBrick.Bottom);
                pDBottom = new Vector2(rectBrick.Right, rectBrick.Bottom + radius);

                if (CurrentRectangle.Center.X >= rectBrick.Left &
                    CurrentRectangle.Center.X <= rectBrick.Right &
                    CurrentRectangle.Center.Y >= rectBrick.Top - radius &
                    CurrentRectangle.Center.Y <= rectBrick.Top)
                {
                    //flectStr = "Top";
                    direction = new Vector2(direction.X, -Math.Abs(direction.Y));
                    return true;
                }
                else if (CurrentRectangle.Center.X >= rectBrick.Left &
                    CurrentRectangle.Center.X <= rectBrick.Right &
                    CurrentRectangle.Center.Y >= rectBrick.Bottom &
                    CurrentRectangle.Center.Y <= rectBrick.Bottom + radius)
                {
                    //reflectStr = "Bottom";
                    direction = new Vector2(direction.X, Math.Abs(direction.Y));
                    return true;
                }
                else if (CurrentRectangle.Center.X >= rectBrick.Left - radius &
                    CurrentRectangle.Center.X <= rectBrick.Left &
                    CurrentRectangle.Center.Y >= rectBrick.Top &
                    CurrentRectangle.Center.Y <= rectBrick.Bottom)
                {
                    //reflectStr = "Left";
                    direction = new Vector2(-Math.Abs(direction.X), direction.Y);
                    return true;
                }
                else if (CurrentRectangle.Center.X >= rectBrick.Right &
                    CurrentRectangle.Center.X <= rectBrick.Right + radius &
                    CurrentRectangle.Center.Y >= rectBrick.Top &
                    CurrentRectangle.Center.Y <= rectBrick.Bottom)
                {
                    //reflectStr = "Right";
                    direction = new Vector2(Math.Abs(direction.X), direction.Y);
                    return true;
                }

                else if (Vector2.Distance(pA, CurrentRectangle.Center.ToVector2()) <= radius)
                {
                    if (Vector2.Distance(CurrentRectangle.Center.ToVector2(), pATop) > Vector2.Distance(CurrentRectangle.Center.ToVector2(), pALeft))
                    {
                        //reflectStr = "Left";
                        direction = new Vector2(-Math.Abs(direction.X), direction.Y);
                        return true;
                    }
                    else
                    {
                        //reflectStr = "Top";
                        direction = new Vector2(direction.X, -Math.Abs(direction.Y));
                        return true;
                    }
                }
                else if (Vector2.Distance(pB, CurrentRectangle.Center.ToVector2()) <= radius)
                {
                    if (Vector2.Distance(CurrentRectangle.Center.ToVector2(), pBTop) > Vector2.Distance(CurrentRectangle.Center.ToVector2(), pBRight))
                    {
                        //reflectStr = "Right";
                        direction = new Vector2(Math.Abs(direction.X), direction.Y);
                        return true;
                    }
                    else
                    {
                        //reflectStr = "Top";
                        direction = new Vector2(direction.X, -Math.Abs(direction.Y));
                        return true;
                    }
                }
                else if (Vector2.Distance(pC, CurrentRectangle.Center.ToVector2()) <= radius)
                {
                    if (Vector2.Distance(CurrentRectangle.Center.ToVector2(), pCBottom) > Vector2.Distance(CurrentRectangle.Center.ToVector2(), pCLeft))
                    {
                        //reflectStr = "Left";
                        direction = new Vector2(-Math.Abs(direction.X), direction.Y);
                        return true;
                    }
                    else
                    {
                        //reflectStr = "Bottom";
                        direction = new Vector2(direction.X, Math.Abs(direction.Y));
                        return true;
                    }
                }
                else if (Vector2.Distance(pD, CurrentRectangle.Center.ToVector2()) <= radius)
                {
                    if (Vector2.Distance(CurrentRectangle.Center.ToVector2(), pDBottom) > Vector2.Distance(CurrentRectangle.Center.ToVector2(), pDRight))
                    {
                        //reflectStr = "Right";
                        direction = new Vector2(Math.Abs(direction.X), direction.Y);
                        return true;
                    }
                    else
                    {
                        //reflectStr = "Bottom";
                        direction = new Vector2(direction.X, Math.Abs(direction.Y));
                        return true;
                    }
                }
            }
            return false;
        }
        public void checkCollisionToOtherBall(Ball otherBall)
        {
            if (Vector2.Distance(Center, otherBall.Center) < (CurrentRectangle.Width / 2 + otherBall.CurrentRectangle.Width / 2))
            {
                Vector2 cOfMass = (direction + otherBall.direction) / 2;

                Vector2 normal2 = Center - otherBall.Center;
                normal2.Normalize();
                Vector2 normal1 = otherBall.Center - Center;
                normal1.Normalize();

                direction -= cOfMass;
                direction = Vector2.Reflect(direction, normal1);
                direction += cOfMass;
                direction.Normalize();

                otherBall.direction -= cOfMass;
                otherBall.direction = Vector2.Reflect(otherBall.direction, normal2);
                otherBall.direction += cOfMass;
                otherBall.direction.Normalize();
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
