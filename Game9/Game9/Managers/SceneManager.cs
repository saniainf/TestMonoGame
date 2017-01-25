using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class SceneManager
    {
        public static SceneManager Instance
        {
            get { return instance ?? (instance = new SceneManager()); }
        }
        private static SceneManager instance;

        public SceneManager()
        {
            LoadContent();
        }

        public void Initialize()
        {

        }

        private void LoadContent()
        {
            Art.AddImage("paddle_mid", "paddle_mid");
            Art.AddImage("paddle_left", "paddle_left");
            Art.AddImage("spriteSheet", "SpriteSheet");
            Art.AddImage("turret", "turret");
            Art.AddImage("turret-mask", "turret-mask");
            Art.SetFont("arial", "arialbd");
        }
    }
}
