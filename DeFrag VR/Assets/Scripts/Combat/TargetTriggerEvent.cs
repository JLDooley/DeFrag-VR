using Game.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public class TargetTriggerEvent : Target
    {
        public GameEvent onHitEvent;

        public override void OnHit(WeaponProfile damageSourceProfile)
        {
            onHitEvent.Raise();
        }
    }
}
