using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace TowerDefense.Infrastructure
{
    public class ComponentsPool<T> where T : Component
    {
        private T template;
        private int capacity = 5;
        private Transform container;
        private HashSet<T> allObjects;

        public ComponentsPool(T template, int capacity, Transform container)
        {
            this.template = template;
            this.capacity = capacity;
            this.container = container;
            InitialPool();
        }

        private void InitialPool()
        {
            allObjects = new HashSet<T>();
            for (int i = 0; i < capacity; i++)
            {
                var obj = CreateObj();
                obj.gameObject.SetActive(false);
            }
        }

        private T CreateObj()
        {
            var obj = Object.Instantiate(template, container);
            allObjects.Add(obj);
            return obj;
        }

        private bool TryGetObj(out T obj)
        {
            obj = allObjects.FirstOrDefault((p) => p.gameObject.activeSelf == false);
            return obj != null;
        }
        public T Get()
        {
            T obj;
            if (TryGetObj(out obj))
                return obj;
            return CreateObj();
        }
        public T GetAt(Vector3 point)
        {
            var obj = Get();
            obj.transform.position = point;
            obj.transform.parent = null;
            obj.transform.rotation = Quaternion.identity;
            obj.gameObject.SetActive(true);
            return obj;
        }
    }
}