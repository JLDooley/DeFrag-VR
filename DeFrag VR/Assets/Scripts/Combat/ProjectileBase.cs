using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        public WeaponAim spawningWeapon;

        public WeaponProfile weaponProfile;

        public GameObject deathEffect;

        [SerializeField]
        protected float speed = 2f;

        [SerializeField]
        protected Rigidbody rb;

        [SerializeField]
        protected bool ignoreAutoAim;

        void OnEnable()
        {
            Debug.Log("Projectile initialised.");

        }

        public virtual void SetProperties()
        {
            if (spawningWeapon != null)
            {
                Debug.Log("Weapon Profile: " + weaponProfile);
                weaponProfile = spawningWeapon.WeaponProfile;
                Debug.Log("Weapon Profile: " + weaponProfile);
            }
        }

        public virtual void DestroyProjectile()
        {
            Debug.Log("Destroy Projectile triggered.");

            if (deathEffect != null)
            {
                Instantiate(deathEffect);
            }

            Destroy(gameObject);
        }
    }
}

