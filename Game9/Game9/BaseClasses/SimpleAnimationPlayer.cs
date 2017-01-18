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
        private int framesCount;
        private float frameTime;
        private bool isLooping;
        private float time;
        private int frameIndex;

        public SimpleAnimationPlayer(AnimatedSprite[] animations, bool isLooping, int framesCount, float frameTime)
        {
            this.isLooping = isLooping;
            this.animations = animations;
            this.framesCount = framesCount;
            this.frameTime = frameTime;
            time = 0f;
            isPlay = false;

            // test section
            frameIndex = 0;
        }

        public void Play()
        {
            isPlay = true;

        }

        public void Update()
        {
            time += (float)GameRoot.ThisGameTime.ElapsedGameTime.TotalSeconds;
            while (time > frameTime)
            {
                time -= frameTime;
                if (isLooping)
                {
                    frameIndex = (frameIndex + 1) % framesCount;
                }
                else
                {
                    frameIndex = Math.Min(frameIndex + 1, framesCount - 1);
                }

            }
            for (int j = 0; j < animations.Count(); j++)
            {
                dc.GetSprite(animations[j].SpriteId).SourceRectangle = animations[j].GetFrame(frameIndex);
            }
        }

        public void Stop()
        {
            isPlay = false;
        }

        public void Pause()
        {
            isPlay = false;
        }
    }
}
