using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;
using Valve.VR.InteractionSystem;

namespace Game.Utility
{
    public class Suspendable : MonoBehaviour
    {
        [SerializeField]
        private PauseSet PauseSet;

        [Tooltip("Automatically assign this gameobject to the PauseSet or assign through remote code.")]
        [SerializeField]
        private bool autoAssign = true;

        private void Awake()
        {
            if (autoAssign)
            {
                SubscribeToSet();
            }
        }

        private void OnDestroy()
        {
            UnsubscribeToSet();
        }

        public void SubscribeToSet()
        {
            PauseSet.Add(this);
        }

        public void UnsubscribeToSet()
        {
            PauseSet.Remove(this);
        }
    }
}


