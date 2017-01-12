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
                int minW = 0;
                int maxW = 0;
                int minH = 0;
                int maxH = 0;
                foreach (Sprite s in images.Values)
                {
                    minW = minW > s.BoundingBox.Left ? s.BoundingBox.Left : minW;
                    maxW = maxW < s.BoundingBox.Right ? s.BoundingBox.Right : maxW;
                    minH = minH > s.BoundingBox.Top ? s.BoundingBox.Top : minH;
                    maxH = maxH < s.BoundingBox.Bottom ? s.BoundingBox.Bottom : maxH;
                }
                int w = Math.Abs(minW) + Math.Abs(maxW);
                int h = Math.Abs(minH) + Math.Abs(maxH);
                return new Rectangle(maxW - w, maxH - h, w, h);
            }
        }

        private Entity root;
        private Dictionary<string, Sprite> images;
        private Dictionary<string, Animation> animation;

        public DrawComponent(Entity rootEntity)
        {
            root = rootEntity;
            images = new Dictionary<string, Sprite>();
        }

        public void Update()
        {

        }

        public IEnumerable<Sprite> GetSprite()
        {
            foreach (Sprite s in images.Values)
            {
                yield return s;
            }
        }

        public void SetSprite(string id, Point offset, Texture2D image, Rectangle sourceRectangle, SpriteEffects spriteEffect)
        {
            images.Add(id, new Sprite(offset, image, sourceRectangle, spriteEffect));
        }

        public void PlayAnimation(string id)
        {
            animation[id].Play();
        }
    }
}
