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
            Art.SetSprite("paddle_mid", "paddle_mid");
            Art.SetSprite("paddle_left", "paddle_left");
            Art.SetFont("arial", "arialbd");
        }
    }
}
