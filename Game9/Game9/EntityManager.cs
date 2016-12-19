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
        public IEnumerable<IEntity> Entities { get { foreach (IEntity e in entities) { yield return e; } } }
        //public IEnumerable<IDraw> DrawEntities { get { foreach (IDraw drawe in (entities.FindAll(d => d is IDraw))) { yield return drawe; } } }
        //public IEnumerable<IPhysics> PhysicsEntities { get { foreach (IPhysics pe in (entities.FindAll(p => p is IPhysics))) { yield return pe; } } }
        public IEnumerable<IDraw> DrawEntities() { foreach (IDraw d in drawEntities) { yield return d; } }
        public IEnumerable<IPhysics> PhysicsEntities { get { foreach (IPhysics p in physicsEntities) { yield return p; } } }
        public int EntityCount { get { return entities.Count; } }

        public List<Entity> _DrawEntities { get { return (entities.FindAll(d => d is IDraw)); } }

        private List<Entity> entities;
        private List<Entity> drawEntities;
        private List<Entity> physicsEntities;

        private static EntityManager instance;
        public static EntityManager Instance
        {
            get { return instance ?? (instance = new EntityManager()); }
        }

        private bool isUpdating;
        private List<Entity> addedEntities;

        public EntityManager()
        {
            entities = new List<Entity>();
            drawEntities = new List<Entity>();
            physicsEntities = new List<Entity>();
            addedEntities = new List<Entity>();
            isUpdating = false;
        }

        public void Update()
        {
            isUpdating = true;

            foreach (Entity e in entities)
                e.Update();

            isUpdating = false;

            foreach (Entity e in addedEntities)
                AddEntity(e);
            addedEntities.Clear();

            entities = entities.Where(e => !e.IsRemove).ToList();
            drawEntities = drawEntities.Where(d => !d.IsRemove).ToList();
            physicsEntities = physicsEntities.Where(p => !p.IsRemove).ToList();
        }

        public void AddEntity(Entity e)
        {
            if (!isUpdating)
            {
                entities.Add(e);
                if (e is IDraw)
                    drawEntities.Add(e);
                if (e is IPhysics)
                    physicsEntities.Add(e);
            }
            else
                addedEntities.Add(e);
        }
    }
}
