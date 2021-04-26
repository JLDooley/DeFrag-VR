using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    [CreateAssetMenu(fileName = "CallMethod", menuName = "Profiles/Damage/CallMethod", order = 3)]
    public class CallMethod : BaseDamageReaction
    {
        public override void Raise(Target target, WeaponProfile weaponProfile)
        {
            target.DamageReactionEvent();
        }
    }
}


