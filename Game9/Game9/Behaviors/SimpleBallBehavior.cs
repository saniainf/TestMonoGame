using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class SimpleBallBehavior : IBehavior
    {
        public bool IsRemove
        {
            get
            {
                return false;
            }
            set
            {

            }
        }

        private Entity root;
        Vector2 ballPosition;
        Vector2 ballDirection;
        float speed;


        public SimpleBallBehavior(Entity rootEntity)
        {
            root = rootEntity;
            speed = 100;
            ballDirection = new Vector2((float)(GameRoot.Rnd.Next(-100, 100)), (float)(GameRoot.Rnd.Next(-100, 100)));
        }

        public void Initialize()
        {

        }

        public void Update()
        {
            Transform t = root.GetComponent<Transform>() as Transform;
            ballPosition = t.Position;

            if (ballPosition.X >= GameRoot.Screen.Width)
                ballDirection.X = -Math.Abs(ballDirection.X);
            if (ballPosition.X <= 0)
                ballDirection.X = Math.Abs(ballDirection.X);
            if (ballPosition.Y >= GameRoot.Screen.Height)
                ballDirection.Y = -Math.Abs(ballDirection.Y);
            if (ballPosition.Y <= 0)
                ballDirection.Y = Math.Abs(ballDirection.Y);

            ballDirection.Normalize();
            ballPosition += speed * ballDirection * (float)GameRoot.ThisGameTime.ElapsedGameTime.TotalSeconds;

            t.Position = ballPosition;
        }
    }
}
