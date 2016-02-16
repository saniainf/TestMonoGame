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
    class Paddle
    {
        public Rectangle CurrentRectangle
        {
            get { return sprite.CurrentRectangle; }
        }

        public Vector2 Location
        {
            get { return sprite.Location; }
            set { sprite.Location = value; }
        }

        private Texture2D texture;
        private Animation idleAnimation;
        private Sprite sprite;

        public Paddle(ContentManager content)
        {
            texture = content.Load<Texture2D>("paddle");
            idleAnimation = new Animation(texture, new Rectangle(0, 0, texture.Width, texture.Height), 1, true, 0);
            sprite = new Sprite(idleAnimation, locationVector: new Vector2(EggGameMain.WindowWidth / 2 - texture.Width / 2, EggGameMain.WindowHeight - texture.Height * 2));
        }
        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                sprite.X -= 3f;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                sprite.X += 3f;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                sprite.Y -= 3f;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                sprite.Y += 3f;
            sprite.Update(gameTime);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            sprite.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
