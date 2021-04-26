using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    /// <summary>
    /// Deal damage to the target.
    /// </summary>
    [CreateAssetMenu(fileName = "DamagePlayer", menuName = "Profiles/Damage/Damage Player", order = 4)]
    public class DamagePlayer : BaseDamageReaction
    {
        public override void Raise(Target target, WeaponProfile weaponProfile)
        {
            Debug.Log("Damage Player triggered.");

            int amount = weaponProfile.damageAmount;

            float newHealthValue = target.CurrentHealth;

            newHealthValue -= amount;

            target.UpdateHealth(newHealthValue);
        }
    }
}

