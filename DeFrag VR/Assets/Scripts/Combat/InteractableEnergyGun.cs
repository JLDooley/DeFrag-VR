using Game.Utility.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public class InteractableEnergyGun : InteractableGun
    {
        [SerializeField]
        private GameObject shieldRef;

        [SerializeField]
        private float shieldCooldown = 5f;

        private float timer = 0f;

        protected override void OnTriggerDown()
        {
            DisableShield();
        }

        private void DisableShield()
        {
            if (shieldRef != null)
            {
                timer = 0f;
                shieldRef.SetActive(false);
            }

            Shoot();
        }
        
        protected override void Update()
        {
            base.Update();

            timer += Time.deltaTime;
            
            if (shieldRef != null)
            {
                if (timer >= shieldCooldown && !shieldRef.activeSelf)
                {
                    shieldRef.SetActive(true);
                }
            }
            
        }
        
    }
}


