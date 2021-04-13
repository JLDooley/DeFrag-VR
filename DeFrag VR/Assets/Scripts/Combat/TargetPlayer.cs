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
                    //Debug.LogWarning(value);
                    UpdateHealth(value);
                    //_CurrentHealth.SetValue(value);   //Will handle this in HealthCheck, so it can be determined if healing or hurting is taking place
                    
                }
            }
        }
        #endregion

        public GameEvent onDamage;
        public GameEvent onHeal;
        public GameEvent onDefeat;

        public override void Setup()
        {
            base.Setup();

            //set health
            SetHealth(MaxHealth);
        }


        public override void SetHealth(float health)
        {
            _CurrentHealth.SetValue(health);
            CurrentHealth = _CurrentHealth.Value;
        }

        public override void UpdateHealth(float newHealthValue)
        {
            Debug.LogWarning("NewHealthValue" + newHealthValue);
            Debug.LogWarning("_CurrentHealth: " + _CurrentHealth.Value);
            Debug.LogWarning("CurrentHealth: " + CurrentHealth);

            if (newHealthValue <= 0f)
            {
                Die();
            }
            else
            {
                if (_CurrentHealth.Value > newHealthValue)   //Player has been hurt
                {
                    Debug.Log("Player hurt.");
                    if (onDamage != null)
                    {
                        onDamage.Raise();
                    }
                    
                }
                else
                {
                    Debug.Log("Player healed.");
                    if (onHeal != null)
                    {
                        onHeal.Raise();
                    }

                }

                _CurrentHealth.SetValue(newHealthValue);
            }

            
            

        }

        public override void OnHit(WeaponProfile damageSourceProfile)
        {
            Debug.Log("Player Hit.");
            base.OnHit(damageSourceProfile);    //Check damage type against target type

            onDamage.Raise();  //Screen flash, etc     This may have to be called elsewhere as it may trigger automatically
        }

        public override void OnHit(WeaponProfile damageSourceProfile, ProjectileBase projectile)
        {
            Debug.Log("Player Hit.");
            base.OnHit(damageSourceProfile, projectile);

            onDamage.Raise();  //Screen flash, etc     This may have to be called elsewhere as it may trigger automatically
        }

        public override void Die()
        {
            Debug.Log("Player Killed.");

            //Pause game, bring up reset screen
            if (onDefeat != null)
            {
                onDefeat.Raise();
            }

        }
    }
}

