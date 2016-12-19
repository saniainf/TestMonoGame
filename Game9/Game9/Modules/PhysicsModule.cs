using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class PhysicsModule
    {
        public static PhysicsModule Instance
        {
            get { return instance ?? (instance = new PhysicsModule()); }
        }

        private static PhysicsModule instance;

        public PhysicsModule()
        {

        }

        public void Update()
        {
            foreach (IPhysics physicsItem in EntityManager.Instance.PhysicsEntities)
            {
                if (physicsItem.PhysicsComponent.BodyType == PhysicBodyType.Dynamic)
                {
                    if (physicsItem.TransformComponent.Position.X + physicsItem.PhysicsComponent.Collider2D.Size.X / 2 >= GameRoot.Screen.Width)
                        physicsItem.PhysicsComponent.Direction = new Vector2(-Math.Abs(physicsItem.PhysicsComponent.Direction.X), physicsItem.PhysicsComponent.Direction.Y);
                    if (physicsItem.TransformComponent.Position.X - physicsItem.PhysicsComponent.Collider2D.Size.X / 2 <= 0)
                        physicsItem.PhysicsComponent.Direction = new Vector2(Math.Abs(physicsItem.PhysicsComponent.Direction.X), physicsItem.PhysicsComponent.Direction.Y);
                    if (physicsItem.TransformComponent.Position.Y + physicsItem.PhysicsComponent.Collider2D.Size.Y / 2 >= GameRoot.Screen.Height)
                        physicsItem.PhysicsComponent.Direction = new Vector2(physicsItem.PhysicsComponent.Direction.X, -Math.Abs(physicsItem.PhysicsComponent.Direction.Y));
                    if (physicsItem.TransformComponent.Position.Y - physicsItem.PhysicsComponent.Collider2D.Size.Y / 2 <= 0)
                        physicsItem.PhysicsComponent.Direction = new Vector2(physicsItem.PhysicsComponent.Direction.X, Math.Abs(physicsItem.PhysicsComponent.Direction.Y));

                    physicsItem.PhysicsComponent.Direction = Vector2.Normalize(physicsItem.PhysicsComponent.Direction);
                    physicsItem.TransformComponent.Position += physicsItem.PhysicsComponent.Velocity * physicsItem.PhysicsComponent.Direction * (float)GameRoot.ThisGameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }
    }
}
