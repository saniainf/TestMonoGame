using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    static class Inf
    {
        public static Rectangle[] RectangleSequence(int count, int x, int y, int width, int height, int dx, int dy)
        {
            Rectangle[] sequence = new Rectangle[count];
            x -= dx;
            y -= dy;
            for (int i = 0; i < count; i++)
            {
                x += dx;
                y += dy;
                sequence[i] = new Rectangle(x, y, width, height);
            }
            return sequence;
        }
    }
}
