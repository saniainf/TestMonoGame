using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class AnimatedSprite
    {
        public Rectangle Frame
        {
            get
            {
                if (frameIndex < framesCount)
                    return recSeq[frameIndex];
                else
                    return recSeq.Last();
            }
        }
        public int FrameIndex { get { return frameIndex; } set { frameIndex = value; } }
        public int FramesCount { get { return framesCount; } }
        public string SpriteId { get { return id; } }

        private Rectangle[] recSeq;
        private string id;
        private int framesCount;
        private bool isLooping;
        private int frameIndex;

        public AnimatedSprite(string spriteId, Rectangle[] sequence, bool isLooping, int startFrame)
        {
            id = spriteId;
            recSeq = sequence;
            framesCount = sequence.Count();
            this.isLooping = isLooping;
            frameIndex = startFrame;
        }
    }
}
