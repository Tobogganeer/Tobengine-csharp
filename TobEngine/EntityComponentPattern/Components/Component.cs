using System;
using System.Collections.Generic;
using System.Text;
using TobEngine.EntityComponentPattern.Entities;
using TobEngine.EntityComponentPattern.Components;

namespace TobEngine.EntityComponentPattern
{
    class Component
    {
        public GameObject gameObject { get; private set; }
        public Transform transform { get => gameObject.transform; }

        public bool TryGetComponent(Type type, out Component component)
        {
            return gameObject.TryGetComponent(type, out component);
        }

        public bool TryGetComponent<T>(out T component) where T : Component
        {
            return gameObject.TryGetComponent(out component);
        }

        public virtual void Start() { }

        public virtual void Update() { }

        public virtual void Render() { }

        public void SetGameObject(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}
