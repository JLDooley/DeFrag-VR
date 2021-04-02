using Game.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Combat
{
    public class TargetTrigger : Target
    {
        public UnityEvent onHitEvent;

        public override void OnHit(WeaponProfile damageSourceProfile)
        {
            onHitEvent.Invoke();
        }
    }
}

