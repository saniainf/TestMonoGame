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
        public Animation CurrentAnimation
        {
            get { return spriteAnimation; }
            set { spriteAnimation = value; }
        }

        public Texture2D SpriteSheet
        {
            get { return spriteSheet; }
            set { spriteSheet = value; }
        }

        public Vector2 Location
        {
            get { return locationVector; }
            set { locationVector = value; }
        }

        public float X
        {
            get { return locationVector.X; }
            set { locationVector.X = value; }
        }

        public float Y
        {
            get { return locationVector.Y; }
            set { locationVector.Y = value; }
        }

        public bool AnimationPlay
        {
            get { return animationPlay; }
            set { animationPlay = value; }
        }

        public bool AnimationLooping
        {
            get { return animationLooping; }
            set { animationLooping = value; }
        }

        public bool IsEnable
        {
            get { return isEnable; }
            set { isEnable = value; }
        }

        private Animation spriteAnimation;
        private Texture2D spriteSheet;
        private List<Rectangle> frames = new List<Rectangle>();
        private int currentFrame;
        private float frameTime;
        private float currentFrameTime;
        private bool animationLooping;
        private bool animationPlay;
        private bool isEnable;

        private Rectangle sourceRectangle;
        private Vector2 locationVector;
        private float spriteRotation;
        private Vector2 spriteOrigin;
        private Vector2 spriteScale;
        private SpriteEffects spriteEffect;
        private float orderLayer;
        private Color spriteColor;

        public Sprite(Animation animation, int startFrame = 0, bool animationLooping = true, bool animationPlay = true, bool isEnable = true, Vector2? locationVector = null, float rotation = 0f, SpriteEffects? spriteEffect = null, Vector2? origin = null, Vector2? scale = null, float orderLayer = 0, Color? color = null)
        {
            this.spriteAnimation = animation;
            this.spriteSheet = animation.SpriteSheet;
            this.frames = animation.Frames;
            this.currentFrame = startFrame;
            this.frameTime = animation.FrameTime;
            this.currentFrameTime = 0f;
            this.animationLooping = animationLooping;
            this.animationPlay = animationPlay;
            this.isEnable = isEnable;
            this.sourceRectangle = frames[currentFrame];
            this.locationVector = locationVector ?? Vector2.Zero;
            this.spriteRotation = rotation;
            this.spriteEffect = spriteEffect ?? SpriteEffects.None;
            this.spriteOrigin = origin ?? Vector2.Zero;
            this.spriteScale = scale ?? new Vector2(1f);
            this.orderLayer = orderLayer;
            this.spriteColor = color ?? Color.White;
        }
        public virtual void Update(GameTime gameTime)
        {
            if (isEnable && animationPlay && frames.Count > 1)
            {
                currentFrameTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                while (currentFrameTime > frameTime)
                {
                    currentFrameTime -= frameTime;
                    if (animationLooping)
                        currentFrame = (currentFrame + 1) % frames.Count;
                    else
                        currentFrame = Math.Min(currentFrame + 1, frames.Count - 1);
                }
                sourceRectangle = frames[currentFrame];
            }
        }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isEnable)
                spriteBatch.Draw(spriteSheet, locationVector, sourceRectangle, spriteColor, spriteRotation, spriteOrigin, spriteScale, spriteEffect, orderLayer);
        }
    }
}
