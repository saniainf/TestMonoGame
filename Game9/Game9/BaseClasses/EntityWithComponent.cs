using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class EntityWithComponent : SimpleEntity
    {
        private List<SimpleEntity> Components;

        public EntityWithComponent()
        {
            Components = new List<SimpleEntity>();
        }

        virtual public void Update()
        {

        }

        public void AddComponent(SimpleEntity component)
        {
            Components.Add(component);
        }
    }
}
