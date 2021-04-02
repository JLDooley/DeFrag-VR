using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    public abstract class RuntimeSet<T> : ScriptableObject
    {
    #if UNITY_EDITOR
        [Multiline]
        public string Description = "";
    #endif

        public List<T> Items = new List<T>();

        public void Add(T item)
        {
            if (!Items.Contains(item))
            {
                Items.Add(item);
            }
        }

        public void Remove(T item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);
            }
        }
    }
}

