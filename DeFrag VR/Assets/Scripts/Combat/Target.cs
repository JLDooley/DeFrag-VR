using UnityEngine;
using Game.Utility;
using Game.Data;

namespace Game.Combat
{
    public abstract class Target : MonoBehaviour
    {
        //public GameManager gameManager;

        /// <summary>
        /// How should the target react to different types of damage.
        /// </summary>
        public TargetType targetType;

        #region Health

        //What exactly these get/set will be defined in derived classes
        public virtual float MaxHealth { get; set; }

        public virtual float CurrentHealth { get; set; }

        public virtual bool targetInvincible { get; set; }
        #endregion

        [Tooltip("The root object of the hierarchy to be destroyed on death. Defaults to this object if blank, or the parentEntity of an EnemyAI if present.")]
        public GameObject parentEntity;


        private void OnEnable()
        {
            Setup();
        }

        public virtual void Setup()
        {
            if (parentEntity == null)
            {
                Debug.Log(gameObject.name + ": No parent assigned, assigning this gameObject");
                parentEntity = gameObject;
            }
        }

        /// <summary>
        /// Sets the Target's current health to the given value.
        /// </summary>
        /// <param name="health"></param>
        public virtual void SetHealth(float health)
        {
            CurrentHealth = health;
        }

        /// <summary>
        /// How the target reacts to changes of health.
        /// </summary>
        public virtual void UpdateHealth(float newHealthValue)
        {

        }
        
        /// <summary>
        /// What happens when being hit by a Raycast damage source.
        /// </summary>
        public virtual void OnHit(WeaponProfile damageSourceProfile)
        {
            targetType.OnDamageRecieved(this, damageSourceProfile);
        }

        /// <summary>
        /// What happens when being hit by a Projectile damage source.
        /// </summary>
        /// <param name="damageSourceProfile"></param>
        /// <param name="projectile">Triggering projectile, for determining if it will be destroyed or carry on.</param>
        public virtual void OnHit(WeaponProfile damageSourceProfile, ProjectileBase projectile)
        {
            targetType.OnDamageRecieved(this, damageSourceProfile, projectile);
        }

        public virtual void Die()
        {
            
        }
    }

}
