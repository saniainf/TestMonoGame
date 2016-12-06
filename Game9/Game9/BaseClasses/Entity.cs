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
        public Vector2 Location;
        public Vector2 Size;
        public bool IsRemove;

        private List<IComponent> components;
        private List<IBehavior> behaviors;

        private bool isUpdating;
        private List<IComponent> addedComponents;
        private List<IBehavior> addedBehaviors;

        public Entity()
        {
            components = new List<IComponent>();
            addedComponents = new List<IComponent>();
            behaviors = new List<IBehavior>();
            addedBehaviors = new List<IBehavior>();

            Location = Vector2.Zero;
            Size = Vector2.Zero;
            IsRemove = false;
        }

        virtual public void Update()
        {
            isUpdating = true;
            foreach (IComponent c in components)
                c.Update();
            foreach (IBehavior b in behaviors)
                b.Update();
            isUpdating = false;

            foreach (IComponent c in addedComponents)
                addComponent(c);
            foreach (IBehavior b in addedBehaviors)
                addBehavior(b);
            
            addedComponents.Clear();
            addedBehaviors.Clear();

            behaviors = behaviors.Where(b => !b.IsRemove).ToList();
        }

        protected void addComponent(IComponent component)
        {
            component.Initialize();
            if (!isUpdating)
                components.Add(component);
            else
                addedComponents.Add(component);
        }

        protected void addBehavior(IBehavior behavior)
        {
            behavior.Initialize();
            if (!isUpdating)
                behaviors.Add(behavior);
            else
                addedBehaviors.Add(behavior);
        }
    }
}
