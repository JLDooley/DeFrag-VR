using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Combat
{
    [CreateAssetMenu(fileName = "New Target Type", menuName = "Profiles/Damage/Target Type", order = 2)]
    public class TargetType : ScriptableObject
    {
        [SerializeField]
        private BaseDamageReaction defaultDamageReaction;
        

        public List<DamageScenario> scenarios;
    



        /// <summary>
        /// Check the Profile's list of damage scenario for the DamageType, and execute the associated response.
        /// </summary>
        /// <param name="target">The Target script this attack should affect.</param>
        /// <param name="weaponProfile">The WeaponProfile of the damage source.</param>
        public void OnDamageRecieved(Target target, WeaponProfile weaponProfile)
        {
            DamageType damageType = weaponProfile.damageType;

            //Check the scenarios list for an entry covering the DamageType being received
            //If found Raise its reaction for the calling target
            //Else carry out some default reaction
            if (scenarios.Exists(x => x.action == damageType))
            {
                int i = scenarios.FindIndex(x => x.action == damageType);

                if (scenarios[i].reaction != null)
                {
                    scenarios[i].reaction.Raise(target, weaponProfile);
                }
                else
                {
                    Debug.LogWarning("TargetType " + this.name + ": No reaction found for DamageType " + damageType.name);
                }

            }
            else
            {
                //Revert to DamageType-independent response
                OnDamageRecieved(target);
            }
        }


        public void OnDamageRecieved(Target target, WeaponProfile weaponProfile, ProjectileBase projectile)
        {
            DamageType damageType = weaponProfile.damageType;

            if (scenarios.Exists(x => x.action == damageType))
            {
                int i = scenarios.FindIndex(x => x.action == damageType);

                if (scenarios[i].reaction != null)
                {
                    scenarios[i].reaction.Raise(target, weaponProfile);
                }
                else
                {
                    Debug.LogWarning("TargetType " + this.name + ": No reaction found for DamageType " + damageType);
                }
                //Allows projectile to continue on, useful for shields
                if (scenarios[i].destroyProjectile)
                {
                    projectile.DestroyProjectile();
                }
            }
            else
            {
                //Revert to DamageType-independent response, and destroy the projectile
                OnDamageRecieved(target);

                projectile.DestroyProjectile();
            }
        }


        /// <summary>
        /// Fallback response to taking damage.
        /// </summary>
        /// <param name="target">The Target script this should affect.</param>
        public void OnDamageRecieved(Target target)
        {
            if (defaultDamageReaction != null)
            {
                defaultDamageReaction.Raise(target);
            }
            
        }
    }

    [System.Serializable]
    public class DamageScenario
    {
        public DamageType action;
        public BaseDamageReaction reaction;
        public bool destroyProjectile = true;
    }
}