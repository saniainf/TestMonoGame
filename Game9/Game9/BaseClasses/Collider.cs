using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    public enum Collider2DType
    {
        Box,
        Circle
    }

    class Collider
    {
        public Collider2DType ColliderType { get { return collider2D; } set { collider2D = value; } }
        public Point Size { get { return size; } set { size = value; } }
        public float Radius { get { return radius; } set { radius = value; } }
        public Point Offset { get { return offset; } set { offset = value; } }
        
        private Collider2DType collider2D;
        private Point size;
        private float radius;
        private Point offset;
    }
}
