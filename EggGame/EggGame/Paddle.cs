using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace EggGame
{
    class Paddle : Sprite
    {
        private float speed;
        private float maxSpeed;
        private Vector2 direction;
        private float gravity;
        private float friction;
        private float acceleration;
        private float inertia;
        private Animation idleAnimation;

        public Paddle(ContentManager content, Vector2? startPosition = null)
            : base()
        {
            speed = 0f;
            maxSpeed = 8f;
            spriteSheet = content.Load<Texture2D>("paddle");
            idleAnimation = new Animation(spriteSheet, new Rectangle(0, 0, spriteSheet.Width, spriteSheet.Height), 1, true, 0);
            locationVector = startPosition ?? new Vector2(EggGameMain.ScreenRectangle.Width/2, 200);
            LoadAnimation(idleAnimation);
            gravity = 0.9f;
            friction = 0.1f;
            acceleration = 0.5f;
            inertia = 0;
        }

        public override void LoadAnimation(Animation animation)
        {
            base.LoadAnimation(animation);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                direction.X = -1;
                speed = Math.Min(speed + acceleration, maxSpeed);
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                direction.X = 1;
                speed = Math.Min(speed + acceleration, maxSpeed);
            }
            else
                direction.X = 0;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {

            }

            if (speed > 0)
                speed = Math.Max(0f, speed - friction - inertia);


            locationVector.X += direction.X * (speed + inertia);
            collisionToWall();
            base.Update(gameTime);
        }

        private void collisionToWall()
        {
            if (CurrentRectangle.Left <= 0)
                locationVector.X = 0;
            if (CurrentRectangle.Right > EggGameMain.ScreenRectangle.Width)
                locationVector.X = EggGameMain.ScreenRectangle.Width - CurrentRectangle.Width;
            if (CurrentRectangle.Top < 50)
                locationVector.Y = 50;
            if (CurrentRectangle.Bottom > EggGameMain.ScreenRectangle.Height)
                locationVector.Y = EggGameMain.ScreenRectangle.Height - CurrentRectangle.Height;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
