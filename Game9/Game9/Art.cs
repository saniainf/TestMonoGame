using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    static class Art
    {
        private static Dictionary<string, Texture2D> sprites = new Dictionary<string, Texture2D>();
        private static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        public static Texture2D GetSprite(string id)
        {
            /*
            Texture2D s;
            if (sprites.TryGetValue(id, out s))
                return sprites[id];
            else
                return null;
             */
            return sprites[id];
        }

        public static void SetSprite(string id, string path)
        {
            Texture2D s = GameRoot.GameContent.Load<Texture2D>(path);
            sprites.Add(id, s);
        }

        public static SpriteFont GetFont(string id)
        {
            return fonts[id];
        }

        public static void SetFont(string id, string path)
        {
            SpriteFont f = GameRoot.GameContent.Load<SpriteFont>(path);
            fonts.Add(id, f);
        }
    }
}
