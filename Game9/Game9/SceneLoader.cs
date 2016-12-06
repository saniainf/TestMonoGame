using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class SceneLoader
    {
        private static SceneLoader instance;
        public static SceneLoader Instance
        {
            get { return instance ?? (instance = new SceneLoader()); }
        }

        public SceneLoader()
        {
            LoadContent();
        }

        public void Initialize()
        {
            EntityManager.Instance.AddEntity(new Ball());
        }

        private void LoadContent()
        {
            Art.SetSprite("ball", "egg");
        }
    }
}
