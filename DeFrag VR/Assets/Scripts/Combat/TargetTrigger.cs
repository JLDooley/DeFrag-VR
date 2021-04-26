using Game.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Combat
{
    public class TargetTrigger : Target
    {
        [SerializeField]
        private EventRaiser onHitEvent;

        public override void OnHit(WeaponProfile damageSourceProfile)
        {
            base.OnHit(damageSourceProfile);
        }

        public override void DamageReactionEvent()
        {
            onHitEvent.Raise();
        }
    }
}

