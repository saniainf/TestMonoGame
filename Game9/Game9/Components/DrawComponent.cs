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
                Rectangle rect = images.ElementAt(0).Value.BoundingBox;
                if (images.Values.Count > 1)
                {
                    for (int i = 1; i < images.Values.Count; i++)
                    {
                        s = images.ElementAt(i).Value.BoundingBox;
                        rect = Rectangle.Union(rect, s);
                    }
                }
                return rect;
            }
        }

        private Entity root;
        private Dictionary<string, Sprite> images;
        private Dictionary<string, IAnimation> animations;

        public DrawComponent(Entity rootEntity)
        {
            root = rootEntity;
            images = new Dictionary<string, Sprite>();
            animations = new Dictionary<string, IAnimation>();
        }

        public void Update()
        {
            if (animations.Count > 0)
            {
                for (int i = 0; i < animations.Count; i++)
                {
                    animations.ElementAt(i).Value.Update();
                }
            }
        }

        public IEnumerable<Sprite> GetSprites()
        {
            foreach (Sprite s in images.Values)
            {
                yield return s;
            }
        }

        public Sprite GetSprite(string id)
        {
            return images[id];
        }

        public void AddSprite(string id, Point offset, Texture2D image, Rectangle sourceRectangle, SpriteEffects spriteEffect)
        {
            images.Add(id, new Sprite(offset, image, sourceRectangle, spriteEffect));
        }

        public void AddAnimation(string id, IAnimation animation)
        {
            animations.Add(id, animation);
        }

        public void PlayAnimation(string id)
        {
            animations[id].Play();
        }
    }
}
