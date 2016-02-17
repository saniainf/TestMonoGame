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
        private Animation idleAnimation;

        public Paddle(ContentManager content, Vector2? startPosition = null)
            : base()
        {
            speed = 5f;
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
                if (CurrentRectangle.Left > EggGameMain.ScreenRectangle.Left)
                    locationVector -= new Vector2(1, 0) * speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                if (CurrentRectangle.Right < EggGameMain.ScreenRectangle.Right)
                    locationVector += new Vector2(1, 0) * speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                if (CurrentRectangle.Top > EggGameMain.ScreenRectangle.Top + 50)
                    locationVector -= new Vector2(0, 1) * speed; ;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                if (CurrentRectangle.Bottom < EggGameMain.ScreenRectangle.Bottom)
                    locationVector += new Vector2(0, 1) * speed; ;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
