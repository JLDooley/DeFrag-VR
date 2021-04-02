using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    /// <summary>
    /// Causes the berserk debuff, damaging nearby enemies but increasing ROF.
    /// </summary>
    [CreateAssetMenu(fileName = "Berserk", menuName = "Profiles/Damage/Berserk", order = 5)]
    public class DebuffBerserk : BaseDamageReaction
    {
        public override void Raise(Target target, WeaponProfile weaponProfile)
        {
            Debug.Log("Berserk triggered.");

            base.Raise(target, weaponProfile);
        }
    }
}

