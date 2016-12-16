using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    public enum PhysicBodyType
    {
        Dynamic,
        Static
    }

    class PhysicsBody : IComponent
    {
        public bool IsRemove { get { return false; } set { } }
        public PhysicBodyType BodyType { get { return bodyType; } set { bodyType = value; } }
        public Vector2 Direction { get { return direction; } set { direction = value; } }
        public float Velocity { get { return velocity; } set { velocity = value; } }
        public bool IsTrigger { get { return isTrigger; } set { isTrigger = value; } }

        private Entity root;
        private PhysicBodyType bodyType;
        private Vector2 direction;
        private float velocity;
        private bool isTrigger;

        public PhysicsBody(Entity rootEntity)
        {
            root = rootEntity;
        }

        public void Initialize()
        {
            bodyType = PhysicBodyType.Static;
            direction = Vector2.Zero;
            velocity = 0f;
            isTrigger = false;
        }

        public void Update()
        {

        }
    }
}
