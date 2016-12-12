using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class Entity
    {
        public bool IsRemove;

        private Dictionary<string, IComponent> components;
        private Dictionary<string, IBehavior> behaviors;

        private bool isUpdating;
        private List<IComponent> addedComponents;
        private List<IBehavior> addedBehaviors;

        public Entity()
        {
            components = new Dictionary<string, IComponent>();
            addedComponents = new List<IComponent>();
            behaviors = new Dictionary<string, IBehavior>();
            addedBehaviors = new List<IBehavior>();

            IsRemove = false;
        }

        virtual public void Update()
        {
            isUpdating = true;
            // event update

            isUpdating = false;

            foreach (IComponent c in addedComponents)
                addComponent(c);
            foreach (IBehavior b in addedBehaviors)
                addBehavior(b);

            addedComponents.Clear();
            addedBehaviors.Clear();

            //behaviors = behaviors.Where(b => !b.Value.IsRemove).ToDictionary
        }

        protected void addComponent(IComponent component)
        {
            component.Initialize();
            if (!isUpdating)
                components.Add(component.GetType().Name, component);
            else
                addedComponents.Add(component);
        }

        protected void addBehavior(IBehavior behavior)
        {
            behavior.Initialize();
            if (!isUpdating)
                behaviors.Add(behavior.GetType().Name, behavior);
            else
                addedBehaviors.Add(behavior);
        }

        public IComponent GetComponent<T>()
        {
            return components[typeof(T).Name];
        }

        public IBehavior GetBehavior<T>()
        {
            return behaviors[typeof(T).Name];
        }
    }
}
