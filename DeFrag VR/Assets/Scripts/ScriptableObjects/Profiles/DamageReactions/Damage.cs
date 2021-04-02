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
        public override void Raise(Target target, WeaponProfile weaponProfile)
        {
            Debug.Log("Damage triggered.");

            int amount = weaponProfile.damageAmount;

            target.CurrentHealth -= amount; //This last, to avoid issues with destroying before effects are triggered.

            //TO DO     award points based on certain parameters (distance, hit streak?, kill streak?)

            //TO DO     enemy pain response here (e.g. flinch and colour flash)

            //target.health -= amount;

            //target.health.ConstantValue -= amount;

        }
    }
}

