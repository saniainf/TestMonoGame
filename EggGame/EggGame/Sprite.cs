using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace EggGame
{
    public class Sprite
    {
        public Rectangle CurrentRectangle
        {
            get
            {
                return new Rectangle(
                    (int)locationVector.X,
                    (int)locationVector.Y,
                    sourceRectangle.Width,
                    sourceRectangle.Height);
            }
            set
            {
                locationVector.X = value.X;
                locationVector.Y = value.Y;
                sourceRectangle.Width = value.Width;
                sourceRectangle.Height = value.Height;
            }
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2(
                    locationVector.X + sourceRectangle.Width / 2,
                    locationVector.Y + sourceRectangle.Height / 2);
            }
        }

        public Animation spriteAnimation;
        public Texture2D spriteSheet;
        public List<Rectangle> frames = new List<Rectangle>();
        public int currentFrame;
        public float frameTime;
        public float currentFrameTime;
        public bool animationLooping;
        public bool animationPlay;
        public bool isEnable;

        public Rectangle sourceRectangle;
        public Vector2 locationVector;
        public float spriteRotation;
        public Vector2 spriteOrigin;
        public Vector2 spriteScale;
        public SpriteEffects spriteEffect;
        public float orderLayer;
        public Color spriteColor;

        public Sprite(int startFrame = 0, bool animationLooping = true, bool animationPlay = true, bool isEnable = true, Vector2? locationVector = null, float rotation = 0f, SpriteEffects? spriteEffect = null, Vector2? origin = null, Vector2? scale = null, float orderLayer = 0, Color? color = null)
        {
            this.currentFrame = startFrame;
            this.currentFrameTime = 0f;
            this.animationLooping = animationLooping;
            this.animationPlay = animationPlay;
            this.isEnable = isEnable;
            this.locationVector = locationVector ?? Vector2.Zero;
            this.spriteRotation = rotation;
            this.spriteEffect = spriteEffect ?? SpriteEffects.None;
            this.spriteOrigin = origin ?? Vector2.Zero;
            this.spriteScale = scale ?? new Vector2(1f);
            this.orderLayer = orderLayer;
            this.spriteColor = color ?? Color.White;
        }

        public virtual void LoadAnimation(Animation animation)
        {
            this.spriteAnimation = animation;
            this.spriteSheet = animation.SpriteSheet;
            this.frames = animation.Frames;
            this.frameTime = animation.FrameTime;
            this.sourceRectangle = frames[currentFrame];
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
            if (!isEnable)
            {
                sourceRectangle = new Rectangle(0, 0, 0, 0);
                locationVector = Vector2.Zero;
            }
        }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isEnable)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(spriteSheet, locationVector, sourceRectangle, spriteColor, spriteRotation, spriteOrigin, spriteScale, spriteEffect, orderLayer);
                spriteBatch.End();
            }

        }
    }
}
