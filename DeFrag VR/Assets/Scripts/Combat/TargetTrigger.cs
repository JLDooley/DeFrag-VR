using Game.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Combat
{
    public class TargetTrigger : Target
    {
        [Space]
        public GameEvent[] onHitGameEvent;
        [Space]
        public UnityEvent onHitEvent;

        public override void OnHit(WeaponProfile damageSourceProfile)
        {
            onHitEvent.Invoke();

            if (onHitGameEvent.Length > 0)
            {
                for (int i = 0; i < onHitGameEvent.Length; i++)
                {
                    if (onHitGameEvent[i] != null)
                    {
                        onHitGameEvent[i].Raise();
                    }

                }
            }
        }
    }
}

