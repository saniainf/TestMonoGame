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
        private Vector2 direction;
        private Animation idleAnimation;

        public Paddle(ContentManager content, Vector2? startPosition = null)
            : base()
        {
            speed = 5f;
            direction = new Vector2(0, -1);
            direction.Normalize();
            spriteSheet = content.Load<Texture2D>("paddle");
            idleAnimation = new Animation(spriteSheet, new Rectangle(0, 0, spriteSheet.Width, spriteSheet.Height), 1, true, 0);
            locationVector = startPosition ?? new Vector2(EggGameMain.ScreenRectangle.Center.X, 200);
            LoadAnimation(idleAnimation);
        }

        public override void LoadAnimation(Animation animation)
        {
            base.LoadAnimation(animation);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                locationVector.X -= 5f;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                locationVector.X += 5f;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                locationVector.Y -= 5f;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                locationVector.Y += 5f;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
