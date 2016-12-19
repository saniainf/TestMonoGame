using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class Drawing : IComponent
    {
        public bool IsRemove { get { return false; } set { } }
        public Vector2 SizeForSprite
        {
            get
            {
                float minW = 0;
                float maxW = 0;
                float minH = 0;
                float maxH = 0;
                foreach (Sprite s in images.Values)
                {
                    minW = minW > s.Offset.X ? s.Offset.X : minW;
                    maxW = maxW < s.Offset.X + s.Image.Width ? s.Offset.X + s.Image.Width : maxW;
                    minH = minH > s.Offset.Y ? s.Offset.Y : minH;
                    maxH = maxH < s.Offset.Y + s.Image.Height ? s.Offset.Y + s.Image.Height : maxH;
                }
                return new Vector2(Math.Abs(minW) + Math.Abs(maxW), Math.Abs(minH) + Math.Abs(maxH));
            }
        }

        private Entity root;
        private Dictionary<string, Sprite> images;

        public Drawing(Entity rootEntity)
        {
            root = rootEntity;
            images = new Dictionary<string, Sprite>();
        }

        public void Update()
        {

        }

        public IEnumerable<Sprite> GetSprite()
        {
            Vector2 rootPosition = (root.TransformComponent.Position);
            foreach (Sprite s in images.Values)
            {
                yield return (new Sprite(s.Offset + rootPosition, s.Image));
            }
        }

        public void SetSprite(string id, Vector2 offset, Texture2D image)
        {
            images.Add(id, new Sprite(offset, image));
        }
    }
}
