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
        private Animation idleAnimation;

        public Paddle(ContentManager content, Vector2? startPosition = null)
            : base()
        {
            speed = 0f;
            maxSpeed = 5f;
            spriteSheet = content.Load<Texture2D>("paddle");
            idleAnimation = new Animation(spriteSheet, new Rectangle(0, 0, spriteSheet.Width, spriteSheet.Height), 1, true, 0);
            locationVector = startPosition ?? new Vector2(EggGameMain.ScreenRectangle.Center.X, 200);
            LoadAnimation(idleAnimation);
            friction = 0.1f;
            gravity = 0.9f;
            acceleration = 0.5f;
        }

        public override void LoadAnimation(Animation animation)
        {
            base.LoadAnimation(animation);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                direction = new Vector2(-1, 0);
                leftOrRight();
            }
            else if (speed > 0)
                speed -= friction;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                direction = new Vector2(1, 0);
                leftOrRight();
            }
            else if (speed > 0)
                speed -= friction;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                direction = new Vector2(0, -1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                direction = new Vector2(0, 1);
            }

            if (speed > 0 && !collisionToWall())
                locationVector += direction * speed;
            base.Update(gameTime);
        }

        private void leftOrRight()
        {
            if (speed < maxSpeed)
                speed += acceleration;
        }

        private bool collisionToWall()
        {
            if (CurrentRectangle.Left <= EggGameMain.ScreenRectangle.Left)
                return true;
            if (CurrentRectangle.Right >= EggGameMain.ScreenRectangle.Right)
                return true;
            if (CurrentRectangle.Top <= EggGameMain.ScreenRectangle.Top + 50)
                return true;
            if (CurrentRectangle.Bottom >= EggGameMain.ScreenRectangle.Bottom)
                return true;
            return false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
