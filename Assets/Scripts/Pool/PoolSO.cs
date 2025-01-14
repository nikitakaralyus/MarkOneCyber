﻿using UnityEngine;
using System.Collections.Generic;
using MarkOne.Factory;

namespace MarkOne.Pool
{
    public abstract class PoolSO<T> : ScriptableObject, IPool<T>
    {
        protected readonly Stack<T> Available = new Stack<T>();
        
        public abstract IFactory<T> Factory { get; set; }
        protected bool HasBeenPrewarmed { get; set; }

        protected virtual T Create()
        {
            return Factory.Create();
        }

        public virtual void Prewarm(int count)
        {
            if (HasBeenPrewarmed)
            {
                Debug.LogWarning($"Pool {name} has already been prewarmed.");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                Available.Push(Create());
            }

            HasBeenPrewarmed = true;
        }

        public virtual T Request()
        {
            return Available.Count > 0 ? Available.Pop() : Create();
        }

        public virtual IEnumerable<T> Request(int count = 1)
        {
            List<T> members = new List<T>(count);

            for (int i = 0; i < count; i++)
            {
                members.Add(Request());
            }

            return members;
        }

        public virtual void Return(T member)
        {
            Available.Push(member);
        }

        public virtual void Return(IEnumerable<T> members)
        {
            foreach (T member in members)
            {
                Return(member);
            }
        }

        public virtual void OnDisable()
        {
            Available.Clear();
            HasBeenPrewarmed = false;
        }
    }
}