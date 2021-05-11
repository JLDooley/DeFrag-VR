using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public class MakeEnemiesHostile : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            TargetAI target = other.gameObject.GetComponentInParent<TargetAI>();

            target.IsHostile = true;
            target.TrackPlayer = true;
        }

    }
}

