using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Utility;

namespace Game.Combat
{
    public class Bullet : ProjectileBase
    {
        


        /// <summary>
        /// Emitter for global events (e.g. Player and UI updates)
        /// </summary>
        [Tooltip("Emitter for global events (e.g. Player and UI updates)")]
        public GameEvent OnHitEvent;

        public override void SetProperties()
        {
            base.SetProperties();
            FireBullet();
        }


        private void FireBullet()
        {
            rb.AddForce(speed * transform.forward, ForceMode.VelocityChange);
        }


        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Projectile impact detected: " + other);

            Target target = other.gameObject.GetComponentInParent<Target>();
            AimAssist aimAssist = other.GetComponent<AimAssist>();

            if (aimAssist == null || !ignoreAutoAim)
            {
                if (target != null)
                {
                    Debug.Log("Target not null: " + target);
                    target.targetType.OnDamageRecieved(target, weaponProfile, this);
                }
                else if (other.gameObject.isStatic)
                {
                    Debug.Log("Static geometry impacted, destroying projectile.");
                    DestroyProjectile();
                }
            }

        }
    }
}

