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

        public static Rectangle[] RectangleSequence(int countWidth, int countHeight, int sheetWidth, int sheetHeight)
        {
            Rectangle[] sequence = new Rectangle[countHeight * countHeight];
            int k = 0;
            int w = sheetWidth / countWidth;
            int h = sheetHeight / countHeight;
            for (int i = 0; i < countHeight; i++)
            {
                for (int j = 0; j < countWidth; j++)
                {
                    sequence[k] = new Rectangle(j * w, i * h, w, h);
                    k++;
                }
            }
            return sequence;
        }
    }
}
