using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EggGame
{
    public class Sprite
    {
        private Texture2D spriteSheet;
        private List<Rectangle> frames = new List<Rectangle>();
        private int currentFrame;
        private float frameTime;
        private float currentFrameTime;

        private Rectangle sourceRectangle;
        private Vector2 locationVector;
        private Vector2 spriteLocation;
        private float spriteRotation;
        private Vector2 spriteOrigin;
        private Vector2 spriteScale;
        private float drawDepth;
        private Color spriteColor;


        public Sprite(Texture2D texture, Rectangle firstFrame, int frameCount, float frameTime)
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteSheet, locationVector, sourceRectangle, spriteColor, spriteRotation, spriteOrigin, spriteScale, SpriteEffects.None, drawDepth);
        }
    }
}
