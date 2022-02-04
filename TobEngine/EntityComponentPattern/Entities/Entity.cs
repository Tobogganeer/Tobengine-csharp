using System;
using System.Collections.Generic;
using System.Text;

namespace TobEngine.EntityComponentPattern
{
    class Entity
    {
        public int id { get; private set; }

        protected List<Component> components { get; private set; } = new List<Component>();

        public bool TryGetComponent(Type type, out Component component)
        {
            if (type.IsSubclassOf(typeof(Component)))
            {
                foreach (Component comp in components)
                {
                    if (comp.GetType() == type)
                    {
                        component = comp;
                        return true;
                    }
                }
            }

            component = null;
            return false;
        }

        public bool TryGetComponent<T>(out T component) where T : Component
        {
            foreach (Component comp in components)
            {
                if (comp.GetType() == typeof(T))
                {
                    component = (T)comp;
                    return true;
                }
            }

            component = null;
            return false;
        }

        public void Start()
        {
            foreach (Component component in components)
            {
                component.Start();
            }
        }

        public void Update()
        {
            foreach (Component component in components)
            {
                component.Update();
            }
        }

        public void Render()
        {
            foreach (Component component in components)
            {
                component.Render();
            }
        }
    }
}
