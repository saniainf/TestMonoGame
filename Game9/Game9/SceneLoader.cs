using Microsoft.Xna.Framework;
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
            UIModule.Instance.Initialize();
            EntityManager.Instance.Initialize();
            DrawModule.Instance.Initizlize();

            Ball firstBall = new Ball();
            Transform ft = firstBall.GetComponent<Transform>() as Transform;
            ft.Position = new Vector2(50, 50);
            EntityManager.Instance.AddEntity(firstBall);

            Ball secondBall = new Ball();
            Transform st = secondBall.GetComponent<Transform>() as Transform;
            st.Position = new Vector2(100, 100);
            EntityManager.Instance.AddEntity(secondBall);
        }

        private void LoadContent()
        {
            Art.SetSprite("ball", "egg");
            Art.SetFont("arial", "arialbd");
        }
    }
}
