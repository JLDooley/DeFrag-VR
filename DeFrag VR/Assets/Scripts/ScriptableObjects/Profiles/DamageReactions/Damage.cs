using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    /// <summary>
    /// Deal damage to the target.
    /// </summary>
    [CreateAssetMenu(fileName = "Damage", menuName = "Profiles/Damage/Damage", order = 5)]
    public class Damage : BaseDamageReaction
    {
        [SerializeField]
        private float damageMultiplier = 1f;
        public override void Raise(Target target, WeaponProfile weaponProfile)
        {
            Debug.Log("Damage triggered.");

            float amount = weaponProfile.damageAmount * damageMultiplier;

            float newHealthValue = target.CurrentHealth;

            newHealthValue -= amount;

            target.UpdateHealth(newHealthValue);

        }
    }
}

