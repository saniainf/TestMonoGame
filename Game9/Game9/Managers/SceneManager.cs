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
            Art.SetSprite("ball", "egg");
            Art.SetSprite("dot", "dot");
            Art.SetSprite("ship", "simple-box");
            Art.SetFont("arial", "arialbd");
        }
    }
}
