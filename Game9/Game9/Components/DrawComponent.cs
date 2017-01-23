using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class DrawComponent : IComponent
    {
        public bool IsRemove { get { return false; } set { } }
        public Rectangle BoundingBox
        {
            get
            {
                Rectangle s;
                Rectangle rect = sprites.ElementAt(0).Value.BoundingBox;
                if (sprites.Values.Count > 1)
                {
                    for (int i = 1; i < sprites.Values.Count; i++)
                    {
                        s = sprites.ElementAt(i).Value.BoundingBox;
                        rect = Rectangle.Union(rect, s);
                    }
                }
                return rect;
            }
        }

        private Entity root;
        private Dictionary<string, Sprite> sprites;
        private Dictionary<string, IAnimation> animations;

        public DrawComponent(Entity rootEntity)
        {
            root = rootEntity;
            sprites = new Dictionary<string, Sprite>();
            animations = new Dictionary<string, IAnimation>();
        }

        public void Update()
        {
            if (animations.Count > 0)
                for (int i = 0; i < animations.Count; i++)
                    animations.ElementAt(i).Value.Update();
        }

        public IEnumerable<Sprite> GetSprites()
        {
            foreach (Sprite s in sprites.Values)
            {
                yield return s;
            }
        }

        public Sprite GetSprite(string id)
        {
            return sprites[id];
        }

        public void AddSprite(Sprite sprite)
        {
            sprites.Add(sprite.Id, sprite);
        }

        public void AddAnimation(string id, IAnimation animation)
        {
            animation.RootDC = this;
            animations.Add(id, animation);
        }

        public void PlayAnimation(string id)
        {
            animations[id].Play();
        }

        public void StopAnimation(string id)
        {
            animations[id].Stop();
        }

        public void PauseAnimation(string id)
        {
            animations[id].Pause();
        }
    }
}
