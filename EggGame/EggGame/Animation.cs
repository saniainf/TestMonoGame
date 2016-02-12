using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EggGame
{
    public class Animation
    {
        public Texture2D SpriteSheet
        {
            get { return spriteSheet; }
            set { spriteSheet = value; }
        }
        public List<Rectangle> Frames
        {
            get { return frames; }
            set
            {
                if (value.Count > 0)
                    frames = value;
            }
        }
        public float FrameTime
        {
            get { return frameTime; }
            set { frameTime = value; }
        }

        private Texture2D spriteSheet;
        private List<Rectangle> frames = new List<Rectangle>();
        private float frameTime;

        public Animation(Texture2D spriteSheet, Rectangle firstFrame, int frameCount, bool simpleSheet, float frameTime)
        {
            this.spriteSheet = spriteSheet;
            if (simpleSheet)
                for (int i = 0; i < frameCount; i++)
                {
                    Rectangle rect = new Rectangle(firstFrame.X + firstFrame.Width * i, firstFrame.Y, firstFrame.Width, firstFrame.Height);
                    frames.Add(rect);
                }
            else
                frames.Add(firstFrame);
            this.frameTime = frameTime;
        }

        public void addFrame(Rectangle frame)
        {
            frames.Add(frame);
        }
    }
}
