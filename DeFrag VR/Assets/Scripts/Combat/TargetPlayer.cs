using Game.Data;
using Game.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public class TargetPlayer : Target
    {
        #region Health
        [SerializeField]
        private FloatReference _MaxHealth;

        public override float MaxHealth
        {
            get { return _MaxHealth; }
        }

        [SerializeField]
        private FloatVariable _CurrentHealth;

        public override float CurrentHealth
        {
            get { return _CurrentHealth.Value; }
            set 
            { 
                if (value != _CurrentHealth.Value)
                {
                    _CurrentHealth.SetValue(value);
                    HealthCheck();
                }
            }
        }
        #endregion

        public GameEvent onHit;
        public GameEvent onDefeat;

        public override void Setup()
        {
            base.Setup();

            //set health
        }

        public override void HealthCheck()
        {
            base.HealthCheck();
        }

        public override void OnHit(WeaponProfile damageSourceProfile)
        {
            Debug.Log("Player Hit.");
            base.OnHit(damageSourceProfile);

            onHit.Raise();  //Screen flash, etc
        }

        public override void OnHit(WeaponProfile damageSourceProfile, ProjectileBase projectile)
        {
            Debug.Log("Player Hit.");
            base.OnHit(damageSourceProfile, projectile);

            onHit.Raise();  //Screen flash, etc
        }

        public override void Die()
        {
            Debug.Log("Player Killed.");
            base.Die(); //Is any base functionality needed?

            //Pause game, bring up reset screen
            onDefeat.Raise();
        }
    }
}

