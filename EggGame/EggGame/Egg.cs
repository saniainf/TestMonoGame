using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace EggGame
{
    class Egg
    {
        private Texture2D texture;
        private Animation animationEgg;
        private Sprite spriteEgg;
        public Egg(ContentManager content)
        {
            texture = content.Load<Texture2D>("run");
            animationEgg = new Animation(texture, new Rectangle(0, 0, texture.Width / 10, texture.Height), 10, true, 100);
            spriteEgg = new Sprite(animationEgg);
        }
        public void Update(GameTime gameTime)
        {
            spriteEgg.Update(gameTime);
            spriteEgg.Location = new Vector2(spriteEgg.Location.X + 1, spriteEgg.Location.Y);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteEgg.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
