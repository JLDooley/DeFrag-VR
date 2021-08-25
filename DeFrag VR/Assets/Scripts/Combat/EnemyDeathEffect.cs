using Game.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    //This is intended as a way to add in death effects retroactively, without modifying the existing Target class code.
    //Better integrate into the health management class(es) in future.
    //Trigger it through the OnDeath event in the Target class.

    public class EnemyDeathEffect : MonoBehaviour
    {
        //Spawn a prefab with a specific orientation relative to the player
        //Prefab should hold a particle effect and a sound effect

        [SerializeField] private GameObject deathEffect;
        [SerializeField] private TransformVariable playerPosition;
        [SerializeField] private Vector3 offsetRotation;

        public void TriggerDeathEffect()
        {
            Vector3 direction = transform.position - playerPosition.posValue;
            Quaternion orientation = Quaternion.LookRotation(direction) * Quaternion.Euler(offsetRotation);

            Instantiate(deathEffect, transform.position, orientation);
        }

    }
}

