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
        public int FramesCount { get { return framesCount; } }
        public string SpriteId { get { return id; } }

        private Rectangle[] recSeq;
        private string id;
        private int framesCount;
        private int startFrame;

        public AnimatedSprite(string spriteId, Rectangle[] sequence, int startFrame)
        {
            id = spriteId;
            recSeq = sequence;
            framesCount = sequence.Count();
            this.startFrame = startFrame;
        }

        public Rectangle GetFrame(int i)
        {
            if (i < startFrame)
                return recSeq.First();
            else if (i >= startFrame && i < framesCount + startFrame)
                return recSeq[i - startFrame];
            else
                return recSeq.Last();
        }
    }
}
