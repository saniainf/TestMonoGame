using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class EntityManager
    {
        // entity lists
        public List<Entity> Entities { get; private set; }
        public List<Entity> DrawEntities { get; private set; }
        public List<Entity> PhysicsEntities { get; private set; }

        private static EntityManager instance;
        public static EntityManager Instance
        {
            get { return instance ?? (instance = new EntityManager()); }
        }

        private bool isUpdating;
        private List<Entity> addedEntities;

        public EntityManager()
        {
            Entities = new List<Entity>();
            DrawEntities = new List<Entity>();
            PhysicsEntities = new List<Entity>();
            addedEntities = new List<Entity>();
            isUpdating = false;
        }

        public void Initialize()
        {
            
        }

        public void Update()
        {
            isUpdating = true;

            foreach (Entity e in Entities)
                e.Update();

            isUpdating = false;

            foreach (Entity e in addedEntities)
                AddEntity(e);
            addedEntities.Clear();

            Entities = Entities.Where(e => !e.IsRemove).ToList();
            DrawEntities = DrawEntities.Where(d => !d.IsRemove).ToList();
            PhysicsEntities = PhysicsEntities.Where(p => !p.IsRemove).ToList();
        }

        public void AddEntity(Entity e)
        {
            if (!isUpdating)
            {
                Entities.Add(e);
                if (e is IDraw)
                    DrawEntities.Add(e);
                if (e is IPhysics)
                    PhysicsEntities.Add(e);
            }
            else
                addedEntities.Add(e);
        }
    }
}
