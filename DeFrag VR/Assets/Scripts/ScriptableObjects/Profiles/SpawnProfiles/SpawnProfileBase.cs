using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    /// <summary>
    /// The base class of all spawnable objects.
    /// </summary>
    public abstract class SpawnProfileBase : ScriptableObject
    {
    #if UNITY_EDITOR
        [Multiline]
        public string Description = "";
#endif

        public GameObject prefab;

    }
}