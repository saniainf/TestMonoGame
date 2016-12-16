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

        private Entity root;
        private Dictionary<string, Sprite> images;

        public Drawing(Entity rootEntity)
        {
            root = rootEntity;
        }

        public void Initialize()
        {
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

        public void SetSprite(string id, Vector2 anchor, Texture2D image)
        {
            images.Add(id, new Sprite(anchor, image));
        }
    }
}
