using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utility
{
    public class DestroyAfterTime : MonoBehaviour
    {
        [SerializeField] private float time = 0f;

        void Start()
        {
            Destroy(this.gameObject, time);
        }

    }
}

