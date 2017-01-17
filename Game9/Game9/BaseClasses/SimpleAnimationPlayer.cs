using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class SimpleAnimationPlayer : IAnimation
    {
        public bool IsPlay { get { return isPlay; } set { isPlay = value; } }
        public DrawComponent RootDC { set { dc = value; } }

        private DrawComponent dc;
        private AnimatedSprite[] animations;
        private bool isPlay;
        private int maxFrameCount;
        private float frameTime;

        int i;

        public SimpleAnimationPlayer(AnimatedSprite[] animations, int maxFrameCount, float frameTime)
        {
            this.animations = animations;
            this.maxFrameCount = maxFrameCount;
            this.frameTime = frameTime;
            isPlay = false;

            // test section
            i = 0;
        }

        public void Play()
        {
            isPlay = true;
        }

        public void Update()
        {
            if (i >= maxFrameCount)
                i = 0;
            for (int j = 0; j < animations.Count(); j++)
            {
                animations[j].FrameIndex = i;
                dc.GetSprite(animations[j].SpriteId).SourceRectangle = animations[j].Frame;
            }
            i++;
        }

        public void Stop()
        {

        }

        public void Pause()
        {
            isPlay = false;
        }
    }
}
