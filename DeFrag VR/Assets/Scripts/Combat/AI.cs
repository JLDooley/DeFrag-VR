using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Game.Utility;
using Game.Combat;
using Game.Data;
using System.Reflection;

namespace Game
{
    public class AI : MonoBehaviour
    {
        public GameManager gameManager;

        public float updateFrequency = 0.3f;



        [Tooltip("The root object of the hierarchy. Defaults to this object if blank.")]
        public GameObject parentEntity;

        //Supporting Scripts
        #region Supporting Scripts
        [Tooltip("Controls Health, Damage To, and Destruction of Prefab")]
        [SerializeField]
        private TargetAI _Target;
        public TargetAI Target
        {
            get { return _Target; }
        }

        [Tooltip("Controls Movement and Orientation of Prefab")]
        [SerializeField]
        private Movement _Movement;
        public Movement Movement
        {
            get { return _Movement; }
        }


        [Tooltip("Weapon Systems of Prefab")]
        [SerializeField]
        private WeaponAim[] Weapons;


        #endregion

        //Remote Properties
        #region Remote Properties
        private ActiveEnemiesSet activeEnemiesSet;

        private Vector3 _PlayerPosition;

        public Vector3 PlayerPosition
        {
            get { return _PlayerPosition; }
        }
        #endregion

        //States
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
        #region IsFacing
        //IsFacing
        
        private bool _IsFacing = false;
        public bool IsFacing
        {
            get { return _IsFacing; }
            set
            {
                if (value != _IsFacing)
                {
                    _IsFacing = value;
                    if (IsFacingEmitter != null)
                    {
                        IsFacingEmitter(value);
                    }
                }
            }
        }
        public event Action<bool> IsFacingEmitter;
        public void IsFacingListener(bool input)
        {
            IsFacing = input;
        }
        #endregion
        #region IsBerserk
        //IsBerserk

        private bool _IsBerserk = false;
        public bool IsBerserk
        {
            get { return _IsBerserk; }
            set
            {
                if (value != _IsBerserk)
                {
                    _IsBerserk = value;
                    if (IsBerserkEmitter != null)
                    {
                        IsBerserkEmitter(value);
                    }
                }
            }
        }
        public event Action<bool> IsBerserkEmitter;
        public void IsBerserkListener(bool input)
        {
            IsBerserk = input;
        }
        #endregion
        #region IsHostile
        //IsHostile

        private bool _IsHostile = false;
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
        public void IsHostileListener(bool input)
        {
            IsHostile = input;
        }
        #endregion

        #endregion

        [SerializeField]
        private GameObject berserkParticleEffect;

        [SerializeField]
        private GameObject berserkArcEffect;

        [SerializeField]
        private int arcInstances;

        [SerializeField]
        private float arcInterval;




        private void OnEnable()
        {
            SubscribeListeners();
        }

        private void OnDisable()
        {
            
        }

        private void SubscribeListeners()
        {
            if (Movement != null)
            {
                Movement.IsFacingEmitter += IsFacingListener;
            }

            if (Target != null)
            {
                Target.TrackPlayerEmitter += TrackPlayerListener;
                Target.IsHostileEmitter += IsHostileListener;
            }

            if (Weapons.Length > 0)
            {
                for (int i = Weapons.Length - 1; i >= 0; i--)
                {
                    
                }
            }


        }

        void Start()
        {
            StartCoroutine(SlowUpdate());
        }

        /// <summary>
        /// Called at a slower rate than once per frame.
        /// </summary>
        /// <returns></returns>
        IEnumerator SlowUpdate()
        {
            while(true)
            {
                GetPlayerPosition();

                yield return new WaitForSecondsRealtime(updateFrequency);   //Unscaled time, in case slow motion is implemented at any point
            }

        }

        // Update is called once per frame
        void Update()
        {
        
        }


        /// <summary>
        /// Use the Player's Transform data to update the PlayerPosition Transform
        /// </summary>
        public void GetPlayerPosition()
        {
            //gameManager.playerTransform.GetTransformValue(_PlayerPosition);
            _PlayerPosition = gameManager.playerTransform.posValue;
        }

        public void TriggerBerserk()
        {
            Debug.Log("Going berserk.");

            IsBerserk = true;

            StartCoroutine(ArcBlasts());
        }

        private IEnumerator ArcBlasts()
        {
            //Instantiate(berserkParticleEffect, parentEntity.transform);

            int counter = 0;
            
            yield return new WaitForSeconds(0.5f);

            while (counter < arcInstances)
            {
                Debug.Log("Spawning arc blast.");

                GameObject currentArcBlast = Instantiate(berserkArcEffect, transform.position, Quaternion.identity);

                currentArcBlast.GetComponent<AOEDamage>().spawningObject = Target;
                currentArcBlast.GetComponent<AOEDamage>().Arc();

                counter++;

                yield return new WaitForSeconds(arcInterval);
            }
            


        }
    }

}
