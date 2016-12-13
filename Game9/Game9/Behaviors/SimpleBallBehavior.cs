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
        Vector2 ballLocation;
        Vector2 ballDirection;
        float speed;


        public SimpleBallBehavior(Entity rootEntity)
        {
            root = rootEntity;
            root.onUpdate += Update;
            speed = 100;
            ballDirection = new Vector2((float)(GameRoot.Rnd.NextDouble()), (float)(GameRoot.Rnd.NextDouble()));
        }

        public void Initialize()
        {

        }

        public void Update()
        {
            Transform t = root.GetComponent<Transform>() as Transform;
            ballLocation = t.Position;

            if (ballLocation.X >= GameRoot.Screen.Width)
                ballDirection.X = -Math.Abs(ballDirection.X);
            if (ballLocation.X <= 0)
                ballDirection.X = Math.Abs(ballDirection.X);
            if (ballLocation.Y >= GameRoot.Screen.Height)
                ballDirection.Y = -Math.Abs(ballDirection.Y);
            if (ballLocation.Y <= 0)
                ballDirection.Y = Math.Abs(ballDirection.Y);

            ballDirection.Normalize();
            ballLocation += speed * ballDirection * (float)GameRoot.ThisGameTime.ElapsedGameTime.TotalSeconds;

            t.Position = ballLocation;
        }
    }
}
