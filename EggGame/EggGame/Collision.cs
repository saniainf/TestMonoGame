using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EggGame
{
    static class Collision
    {
        public static bool RectangleToWall(ref Vector2 direction, Rectangle rect)
        {
            if (rect.Y >= EggGameMain.WindowHeight - rect.Height)
            {
                direction = Vector2.Reflect(direction, new Vector2(0, 1f));
                return true;
            }
            else if (rect.Y <= 0)
            {
                direction = Vector2.Reflect(direction, new Vector2(0, -1f));
                return true;
            }
            else if (rect.Location.X <= 0)
            {
                direction = Vector2.Reflect(direction, new Vector2(1, 0f));
                return true;
            }
            else if (rect.Location.X > EggGameMain.WindowWidth - rect.Width)
            {
                direction = Vector2.Reflect(direction, new Vector2(-1, 0f));
                return true;
            }
            else
                return false;
        }
        public static bool RectangleToPaddle(ref Vector2 direction, Rectangle rect, Rectangle paddle)
        {
            if (rect.Intersects(paddle))
            {
                //direction = Vector2.Reflect(direction, new Vector2(0, 1f));
                direction = new Vector2(direction.X, -Math.Abs(direction.Y));
                return true;
            }
            else
                return false;
        }
    }
}
