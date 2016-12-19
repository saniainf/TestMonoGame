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
        public Vector2 Size { get { return size; } set { size = value; } }
        public float Radius { get { return radius; } set { radius = value; } }
        public Vector2 Offset { get { return offset; } set { offset = value; } }
        
        private Collider2DType collider2D;
        private Vector2 size;
        private float radius;
        private Vector2 offset;
    }
}
