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
        private bool isRemove;

        public delegate void UpdateDelegate();
        public event UpdateDelegate onUpdate;

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
        }

        public void Initialize()
        {

        }

        virtual public void Update()
        {
            isUpdating = true;
            onUpdate();
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
            component.Initialize();
            if (!isUpdating)
                components.Add(component.GetType().Name, component);
            else
                addedComponents.Add(component);
        }

        protected void setBehavior(IBehavior behavior)
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
