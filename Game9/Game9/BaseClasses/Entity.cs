using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class Entity : IEntity
    {
        public bool IsRemove { get { return isRemove; } set { isRemove = value; } }
        public Transform TransformComponent { get { return GetComponent<Transform>() as Transform; } }
        private bool isRemove;

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
            setComponent(new Transform(this));
            Initialize();
        }

        virtual public void Initialize()
        {

        }

        virtual public void Update()
        {
            isUpdating = true;
            foreach (IComponent c in components.Values)
                c.Update();
            foreach (IBehavior b in behaviors.Values)
                b.Update();
            isUpdating = false;

            foreach (IComponent c in addedComponents)
                setComponent(c);
            foreach (IBehavior b in addedBehaviors)
                setBehavior(b);

            addedComponents.Clear();
            addedBehaviors.Clear();

            behaviors = behaviors.Where(v => !v.Value.IsRemove).ToDictionary(k => k.Key, v => v.Value);
            components = components.Where(v => !v.Value.IsRemove).ToDictionary(k => k.Key, v => v.Value);
        }

        protected void setComponent(IComponent component)
        {
            if (!isUpdating)
            {
                component.Initialize();
                components.Add(component.GetType().Name, component);
            }
            else
                addedComponents.Add(component);
        }

        protected void setBehavior(IBehavior behavior)
        {
            if (!isUpdating)
            {
                behavior.Initialize();
                behaviors.Add(behavior.GetType().Name, behavior);
            }
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
