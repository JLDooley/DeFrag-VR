using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    /// <summary>
    /// Rays hitting the AimAssist collider are redirected towards a new point
    /// </summary>
    public class AimAssist : MonoBehaviour
    {
        [SerializeField]
        private Transform _newTarget;

        public Transform newTarget
        {
            get { return _newTarget; }
        }

        private void OnEnable()
        {
            if (_newTarget == null)
            {
                _newTarget = transform;
            }
        }
    }
}

