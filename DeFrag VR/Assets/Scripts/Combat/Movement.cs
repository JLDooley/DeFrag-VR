using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Utility;
using System.ComponentModel;
using System;
using System.Runtime.CompilerServices;

namespace Game
{
    public class Movement : MonoBehaviour
    {
        public Rigidbody rb;


        public Transform[] path;
        //public bool orientToPath = false;

        public float velocity = 1f;
        public float targetVelocity = 1f;
        public float acceleration = 1f;

        private float pathLength;
        private float pathPosition = 0f;

        private Vector3 previousPosition = Vector3.zero;
        private Vector3 currentPosition = Vector3.zero;

        public float currentVelocity;

        public float lookAheadAmount = 0.01f;

        private Vector3 lookTarget;
        private Vector3 aimVec;
        private Quaternion aimQuat;

        public float rotationSpeed = 45f;

        public float facingTolerance = 5f;

        [SerializeField]
        private AI _AI;
        public AI AI
        {
            get { return _AI; }
        }


        #region Input Signals

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

        #endregion

        #region Output Signals

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
        private void IsFacingListener(bool input)
        {
            IsFacing = input;
        }
        #endregion

        #endregion



        private void OnEnable()
        {
            SubscribeListeners();
        }

        private void SubscribeListeners()
        {
            if (AI != null)
            {
                AI.TrackPlayerEmitter += TrackPlayerListener;
            }
            
        }

        void Start()
        {
            transform.position = path[0].position;
            
            if (path.Length >= 2)
            {
                pathLength = iTween.PathLength(path);
            }

        }

        void FixedUpdate()
        {
            //Physics.Simulate(GameManager.fixedDeltaTime);

            if (path.Length >= 2)
            {
                TargetPathPosition();
            }

            if (path.Length > 0)
            {
                VelocityCompensation();
                MoveObject();
            }

            RotateObject();

            if (AI != null)
            {
                FacingCheck();
            }


            if (pathPosition >= 1 && velocity != 0)
            {
                StopObject();
            }

        }


        #region Movement
        /// <summary>
        /// Expected point along path for this frame (as a percentage of total path length).
        /// </summary>
        private void TargetPathPosition()
        {
            float changeRate = velocity * Time.fixedDeltaTime / pathLength;

            //Debug.Log(changeRate);

            pathPosition = Mathf.Max(pathPosition + changeRate, 0f);
        }

        /// <summary>
        /// Compensates for deviations between intended velocity and actual velocity due to iTween's percentage-based movement.
        /// </summary>
        private void VelocityCompensation()
        {
            previousPosition = currentPosition;
            currentPosition = transform.position;

            currentVelocity = ((currentPosition - previousPosition).magnitude) / Time.fixedDeltaTime;

            //  If using the PointOnPath method, velocity can change depending on the distance between nodes
            if (path.Length >= 2)
            {
                //  Adjust the speed to compensate, unless too close to merit a change
                if (!CalculationFunctions.FastApproximately(currentVelocity, targetVelocity, 0.01f))
                {
                    //Debug.Log("Calculating");

                    if (currentVelocity > targetVelocity)       // Too fast, slow down
                    {
                        velocity -= (acceleration * Time.fixedDeltaTime);
                    }
                    else if (currentVelocity < targetVelocity)  // Too slow, speed up
                    {
                        velocity += (acceleration * Time.fixedDeltaTime);
                    }
                }
            }
        }

        /// <summary>
        /// Move to the point calculated by TargetPathPosition() for complex paths, or towards the destination for linear paths. 
        /// </summary>
        private void MoveObject()
        {
            if (path.Length >= 2)
            {
                Vector3 coordinateOnPath = iTween.PointOnPath(path, pathPosition);

                //gameObject.transform.position = coordinateOnPath;

                //rb.velocity = (Vector3.Normalize(coordinateOnPath - transform.position)) * targetVelocity;

                //Debug.Log("Pre-Move Transform Position: " + transform.position);
                //Debug.Log("Pre-Move Rigidbody Position: " + rb.position);
                rb.MovePosition(coordinateOnPath);
                //Debug.Log("Post-Move Transform Position: " + transform.position);
                //Debug.Log("Post-Move Rigidbody Position: " + rb.position);

                //rb.AddForce((Vector3.Normalize(coordinateOnPath - transform.position)) * targetVelocity, ForceMode.VelocityChange);

            }
            else if (path.Length == 1)
            {
                iTween.MoveTo(gameObject, iTween.Hash("position", path[0], "speed", targetVelocity));
            }
        }

        /// <summary>
        /// Prevents a feedback loop when stopping. 
        /// </summary>
        private void StopObject()
        {
            targetVelocity = 0f;

            if (velocity < 0)
            {
                velocity = 0;   //Prevent a bug where setting target velocity to 0 would make the gameobject reverse direction and accelerate)
            }
        }
        #endregion

        #region Rotation
        
        private void RotateObject()
        {
            if (AI != null && TrackPlayer)
            {
                lookTarget = AI.PlayerPosition;
            }
            else
            {
                if (path.Length >= 2)
                {
                    lookTarget = iTween.PointOnPath(path, pathPosition + lookAheadAmount);
                }
                else if (path.Length == 1)
                {
                    lookTarget = path[0].position;
                }
                else
                {
                    lookTarget = transform.position + transform.forward;
                    //Debug.Log(lookTarget);
                }
            }
            //Project direction to target onto plane, and rotate towards it (use RotateTowards() so that it can update/chase the target)
            aimVec = Vector3.ProjectOnPlane(lookTarget - transform.position, Vector3.up);
            aimQuat = Quaternion.LookRotation(aimVec);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, aimQuat, rotationSpeed * Time.fixedDeltaTime);
        }

        private void FacingCheck()
        {
            Vector3 player = AI.PlayerPosition;
            
            //What is the x-z direction to the player (VR headset)
            Vector3 directionToPlayer = player - transform.position;
            directionToPlayer = Vector3.ProjectOnPlane(directionToPlayer, Vector3.up);

            //What is the current orientation of this gameobject
            Vector3 currentFacing = transform.forward;
            currentFacing = Vector3.ProjectOnPlane(currentFacing, Vector3.up);

            //The angle between the two directions
            float facingAngle = Vector3.Angle(directionToPlayer, currentFacing);

            if (facingAngle <= facingTolerance)
            {
                IsFacing = true;
            }
            else
            {
                IsFacing = false;
            }
        }

        #endregion

        #region Debugging
        private void OnDrawGizmos()
        {
            iTween.DrawPath(path);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, lookTarget);
        }
        #endregion
    }
}

