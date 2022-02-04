using System;
using System.Collections.Generic;
using System.Text;
using TobEngine.EntityComponentPattern.Components;

namespace TobEngine.EntityComponentPattern.Entities
{
    class GameObject : Entity
    {
        public Transform transform { get; private set; } = Transform.Identity;

        public string tag;
        public string name;

        public void AddComponent(Component component)
        {
            components.Add(component);
            component.SetGameObject(this);
        }

        public GameObject()
        {
            transform = Transform.Identity;
            name = "New GameObject";
        }

        public GameObject(Transform transform, string name = "New GameObject")
        {
            this.transform = transform;
            this.name = name;
        }
    }
}
