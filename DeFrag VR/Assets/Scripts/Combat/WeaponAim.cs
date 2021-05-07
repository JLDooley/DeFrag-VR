using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.Combat
{
    /// <summary>
    /// Controls the movement and logic of a weapon object (specifically turret-style weapons).
    /// </summary>
    public class WeaponAim : MonoBehaviour
    {
        

        [SerializeField]
        private AI _AI;
        public AI AI
        {
            get { return _AI; }
        }

        [SerializeField]
        private WeaponProfileTracking _WeaponProfile;
        public WeaponProfileTracking WeaponProfile
        {
            get { return _WeaponProfile; }
        }
        

        [Header("Tracking")]
        [SerializeField]
        [Tooltip("Base transform of the object to be rotated.")]
        private Transform rotationObject;

        [SerializeField]
        [Tooltip("Orientation to use when idle.")]
        private Transform defaultOrientation;

        [SerializeField]
        [Tooltip("Orientation from which to calculate the maximum pivot range.")]
        private Transform rangeCentre;

        [Header("Shooting")]
        [SerializeField]
        private GameObject projectilePrefab;

        [SerializeField]
        [Tooltip("Point from which to spawn projectiles.")]
        private Transform muzzle;

        [SerializeField]
        [Tooltip("Wait period before spawning a projectile.")]
        private float attackTelegraphDuration;

        [SerializeField]
        [FMODUnity.EventRef]
        private string telegraphSoundEffect;

        [SerializeField]
        [FMODUnity.EventRef]
        private string onShootSoundEffect;

        //public event EventHandler AttackTelegraphEmitter;

        //public event EventHandler MuzzleFlashEventEmitter;

        #region Properties
        


        private float rateOfFire;
        private float aimingTolerance;
        private float rotationSpeed;
        private float pivotRange;

        [SerializeField]
        private float berserkMultiplier;

        #region Other Properties

        private float timer;
        Vector3 aimVec;
        Quaternion aimQuat;
        Quaternion targetQuat;
        Vector3 projection;
        Vector3 projectionNorm;
        Vector3 newTarget;
        #endregion


        #endregion

        #region States
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
                }
            }
        }
        public void IsFacingListener(bool input)
        {
            IsFacing = input;
        }
        #endregion
        #region IsHostile
        //IsHostile
        //[SerializeField]
        private bool _IsHostile = false;
        public bool IsHostile
        {
            get { return _IsHostile; }
            set
            {
                if (value != _IsHostile)
                {
                    _IsHostile = value;
                }
            }
        }
        public void IsHostileListener(bool input)
        {
            IsHostile = input;
        }
        #endregion
        #region IsBerserk
        //IsBerserk
        //Listens for a change, but doesn't cause one.
        private bool _IsBerserk = false;
        public bool IsBerserk
        {
            get { return _IsBerserk; }
            set
            {
                if (value != _IsBerserk)
                {
                    _IsBerserk = value;
                    SetBerserk();
                }
            }
        }
        public void IsBerserkListener(bool input)
        {
            IsBerserk = input;
        }
        #endregion
        #region IsAimed
        //[SerializeField]
        private bool _IsAimed = false;
        public bool IsAimed
        {
            get { return _IsAimed; }
            set
            {
                if (value != _IsAimed)
                {
                    _IsAimed = value;
                }
            }
        }
        #endregion
        #region ShouldAttack
        //[SerializeField]
        private bool _ShouldAttack = false;
        public bool ShouldAttack
        {
            get { return _ShouldAttack; }
            set
            {
                if (value != _ShouldAttack)
                {
                    _ShouldAttack = value;
                }
            }
        }
        #endregion

        #endregion

        private void OnEnable()
        {
            SetProperties();
            SubscribeListeners();
        }

        private void SubscribeListeners()
        {
            if (AI != null)
            {
                AI.IsFacingEmitter += IsFacingListener;
                AI.IsHostileEmitter += IsHostileListener;
                AI.IsBerserkEmitter += IsBerserkListener;
            }
        }

        private void SetProperties()
        {
            rateOfFire = 1 / _WeaponProfile.rateOfFire;  //Convert from shots per second to shot cooldown
            aimingTolerance = _WeaponProfile.aimingTolerance;
            rotationSpeed = _WeaponProfile.rotationSpeed;
            pivotRange = _WeaponProfile.pivotRange;

            CheckDefaultOrientation();
        }

        private void SetBerserk()
        {
            if (IsBerserk)
            {
                rateOfFire = 1 / (_WeaponProfile.rateOfFire * berserkMultiplier);
            }
            else
            {
                rateOfFire = 1 / _WeaponProfile.rateOfFire;
            }
            
        }

        void Update()
        {
            timer += Time.deltaTime;



            if (IsHostile)
            {
                //Get direction to player
                aimVec = AI.PlayerPosition - transform.position;
                aimQuat = Quaternion.LookRotation(aimVec);

                //Should shoot at the current target (Could do with some target obstructed check here, maybe check if has LOS to stage position (add collider))
                ShouldAttack = true;

                AimWeapon(CheckOrientation(aimVec));
                CheckIfAimed(aimQuat);
                
            }
            else
            {
                ShouldAttack = false;

                AimWeapon(defaultOrientation.rotation);
            }

            if (IsHostile && IsAimed && ShouldAttack && timer >= rateOfFire)
            {
                StartCoroutine(Shoot());
                timer = 0f;
            }
        }

        #region Aiming
        /// <summary>
        /// Turn towards the specified rotation.
        /// </summary>
        /// <param name="target"></param>
        public void AimWeapon(Quaternion target)
        {
            rotationObject.rotation = Quaternion.RotateTowards(rotationObject.rotation, target, rotationSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Controls IsAimed state based on input rotation.
        /// </summary>
        /// <param name="target"></param>
        private void CheckIfAimed(Quaternion target)
        {
            if (Quaternion.Angle(rotationObject.rotation, target) <= aimingTolerance)
            {
                IsAimed = true;
            }
            else
            {
                IsAimed = false;
            }
        }

        /// <summary>
        /// Limits the range of motion allowed.
        /// </summary>
        /// <param name="target">Direction to desired target.</param>
        /// <returns>Amended rotation to use. Does not affect original target rotation or IsAimed state.</returns>
        private Quaternion CheckOrientation(Vector3 target)
        {
            targetQuat = Quaternion.LookRotation(target);

            if (Quaternion.Angle(rangeCentre.rotation, targetQuat) <= pivotRange)
            {
                return targetQuat;
            }
            else
            {
                projection = Vector3.ProjectOnPlane(target, rangeCentre.forward);

                projectionNorm = projection.normalized;

                if (pivotRange > 0f)    
                {
                    newTarget = projectionNorm + (rangeCentre.forward * (Mathf.Tan( Mathf.Deg2Rad* (90f - pivotRange))));
                }
                else     //Tan(90) edge case
                {
                    newTarget = rangeCentre.forward;
                }

                targetQuat = Quaternion.LookRotation(newTarget);
                return targetQuat;
            }
        }

        /// <summary>
        /// Adjusts the default orientation if it lies outside the travel range of the weapon.
        /// </summary>
        private void CheckDefaultOrientation()
        { 
            defaultOrientation.rotation = CheckOrientation(defaultOrientation.forward);
        }
        #endregion

        #region Shooting
        private IEnumerator Shoot()
        {
            Debug.Log("Shooting");
            //Some shot telegraphing, followed by the spawning of a projectile with stats from the weapon profile
            #region Telegraph Code
            //if (AttackTelegraphEmitter != null)
            //{
            //    AttackTelegraphEmitter(this, EventArgs.Empty);
            //}

            if (telegraphSoundEffect != "")
            {
                FMODUnity.RuntimeManager.PlayOneShot(telegraphSoundEffect, muzzle.position);
            }

            #endregion

            yield return new WaitForSecondsRealtime(attackTelegraphDuration);

            #region Shoot Effect Code
            //if (MuzzleFlashEventEmitter != null)
            //{
            //    MuzzleFlashEventEmitter(this, EventArgs.Empty);
            //}

            if (onShootSoundEffect != "")
            {
                FMODUnity.RuntimeManager.PlayOneShot(onShootSoundEffect, muzzle.position);
            }
            
            #endregion

            Debug.Log("Spawning Projectile");
            GameObject prefab = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
            ProjectileBase projectile = prefab.GetComponent<ProjectileBase>();
            if (projectile != null)
            {
                projectile.spawningWeapon = this;
                projectile.SetProperties();
            }
            
        }
        #endregion


        #region Debugging
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Vector3 refPoint = transform.position;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(refPoint, rangeCentre.forward + refPoint);

            //Gizmos.color = Color.blue;
            //Gizmos.DrawLine(refPoint, AI.PlayerPosition);

            //Gizmos.color = Color.green;
            //Gizmos.DrawLine(refPoint, projection + refPoint);

            //Gizmos.color = Color.yellow;
            //Gizmos.DrawLine(refPoint, projectionNorm + refPoint);
            //Gizmos.DrawLine(projectionNorm + refPoint, newTarget + refPoint);

            //Gizmos.color = Color.magenta;
            //Gizmos.DrawLine(refPoint, newTarget + refPoint);

            if (_WeaponProfile != null)
            {
                Gizmos.color = new Color(255/255f, 201/255f, 14/255f);
                Vector3 offset;
                float angle = _WeaponProfile.pivotRange;
                if (angle > 0f)
                {
                    offset = rangeCentre.forward * (Mathf.Tan(Mathf.Deg2Rad * (90f - angle)));


                    Gizmos.DrawLine(rangeCentre.position, Vector3.Normalize(rangeCentre.right + offset) + refPoint);
                    Gizmos.DrawLine(rangeCentre.position, Vector3.Normalize((-1 * rangeCentre.right) + offset) + refPoint);
                    Gizmos.DrawLine(rangeCentre.position, Vector3.Normalize(rangeCentre.up + offset) + refPoint);
                    Gizmos.DrawLine(rangeCentre.position, Vector3.Normalize((-1 * rangeCentre.up) + offset) + refPoint);
                }
            }

        }
#endif
        #endregion

    }
}


