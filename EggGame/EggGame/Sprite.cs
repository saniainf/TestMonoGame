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
        public Vector2 Location
        {
            get { return locationVector; }
            set { locationVector = value; }
        }

        private Animation spriteAnimation;
        private Texture2D spriteSheet;
        private List<Rectangle> frames = new List<Rectangle>();
        private int currentFrame;
        private float frameTime;
        private float currentFrameTime;
        private bool loopAnimation;
        private bool playAnimation;

        private Rectangle sourceRectangle;
        private Vector2 locationVector;
        private float spriteRotation;
        private Vector2 spriteOrigin;
        private Vector2 spriteScale;
        private float orderLayer;
        private Color spriteColor;

        public Sprite(Animation animation, int startFrame = 0, bool loopAnimation = true, bool playAnimation = true, Vector2? locationVector = null, float rotation = 0f, Vector2? origin = null, Vector2? scale = null, float orderLayer = 0, Color? color = null)
        {
            this.spriteAnimation = animation;
            this.spriteSheet = animation.SpriteSheet;
            this.frames = animation.Frames;
            this.currentFrame = startFrame;
            this.frameTime = animation.FrameTime;
            this.currentFrameTime = 0f;
            this.loopAnimation = loopAnimation;
            this.playAnimation = playAnimation;
            this.sourceRectangle = frames[currentFrame];
            this.locationVector = locationVector ?? Vector2.Zero;
            this.spriteRotation = rotation;
            this.spriteOrigin = origin ?? Vector2.Zero;
            this.spriteScale = scale ?? new Vector2(1f);
            this.orderLayer = orderLayer;
            this.spriteColor = color ?? Color.White;
        }
        public virtual void Update(GameTime gameTime)
        {
            currentFrameTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (currentFrameTime > frameTime)
            {
                currentFrameTime = 0;
                currentFrame += 1;
            }
            currentFrame = (currentFrame + 1) % frames.Count - 1;
            sourceRectangle = frames[currentFrame];
        }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteSheet, locationVector, sourceRectangle, spriteColor, spriteRotation, spriteOrigin, spriteScale, SpriteEffects.None, orderLayer);
        }
    }
}
