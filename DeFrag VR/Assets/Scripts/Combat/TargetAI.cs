using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Data;
using Game.Utility;

namespace Game.Combat
{
    /// <summary>
    /// Extends Target to include interactability with AI.
    /// </summary>
    public class TargetAI : Target
    {
        public GameManager gameManager;

        [SerializeField]
        private AI _AI;
        public AI AI
        {
            get { return _AI; }
        }

        #region Health

        [SerializeField]
        private float _MaxHealth;

        public override float MaxHealth
        {
            get { return _MaxHealth; }
        }

        [SerializeField]
        private float _CurrentHealth;

        public override float CurrentHealth
        {
            get { return _CurrentHealth; }
            set
            {
                if (value != _CurrentHealth)
                {
                    _CurrentHealth = value;
                    HealthCheck();
                }
            }
        }

        #endregion

        #region States
        #region TrackPlayer
        //TrackPlayer

        private bool _TrackPlayer;
        public bool TrackPlayer
        {
            get { return _TrackPlayer; }
            set
            {
                if (value != _TrackPlayer)
                {
                    _TrackPlayer = value;
                    if (TrackPlayerEmitter != null)
                    {
                        TrackPlayerEmitter(value);
                    }
                }
            }
        }
        public event Action<bool> TrackPlayerEmitter;
        private void TrackPlayerListener(bool input)
        {
            TrackPlayer = input;
        }
        #endregion
        #region IsHostile
        //IsHostile

        private bool _IsHostile;
        public bool IsHostile
        {
            get { return _IsHostile; }
            set
            {
                if (value != _IsHostile)
                {
                    _IsHostile = value;
                    if (IsHostileEmitter != null)
                    {
                        IsHostileEmitter(value);
                    }
                }
            }
        }
        public event Action<bool> IsHostileEmitter;
        private void IsHostileListener(bool input)
        {
            IsHostile = input;
        }
        #endregion
        #endregion

        //Remote Properties
        #region Remote Properties
        private ActiveEnemiesSet activeEnemiesSet;
        #endregion

        //private void OnEnable()
        //{
        //    Setup();
        //}

        private void OnDisable()
        {
            activeEnemiesSet.Remove(parentEntity);
        }

        public override void Setup()
        {
            base.Setup();

            activeEnemiesSet = gameManager.ActiveEnemiesSet;
            activeEnemiesSet.Add(parentEntity);

            SubscribeListeners();
        }

        public override void HealthCheck()
        {
            base.HealthCheck();
        }

        private void SubscribeListeners()
        {
            if (AI != null)
            {
                AI.TrackPlayerEmitter += TrackPlayerListener;
                AI.IsHostileEmitter += IsHostileListener;
            }
        }

        /// <summary>
        /// What happens when being hit by a damage source. Also makes AI aggressive to Player.
        /// </summary>
        /// <param name="damageSourceProfile"></param>
        public override void OnHit(WeaponProfile damageSourceProfile)
        {
            TrackPlayer = true;
            IsHostile = true;
            base.OnHit(damageSourceProfile);
        }

        public override void Die()
        {
            base.Die();

            //Remove from Active Enemies Set
            activeEnemiesSet.Remove(parentEntity);

            //Give points for destroying an enemy, add GameController score (for display) and WaveManager score (for removing if a stage needs to be repeated) Not necessary if restarting from level start on fail

            Destroy(parentEntity);
        }
    }
}

