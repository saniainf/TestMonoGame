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
        private static EntityManager instance;
        public static EntityManager Instance
        {
            get { return instance ?? (instance = new EntityManager()); }
        }

        public List<Entity> entities { get; private set; }

        private bool isUpdating;
        private List<Entity> addedEntities;

        public EntityManager()
        {
            entities = new List<Entity>();
            addedEntities = new List<Entity>();
            isUpdating = false;
        }

        public void Initialize()
        {

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
        }

        public void AddEntity(Entity e)
        {
            if (!isUpdating)
                entities.Add(e);
            else
                addedEntities.Add(e);
        }
    }
}
