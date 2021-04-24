using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace Game.Utility.Interaction
{
    /// <summary>
    /// Manages the models to be used with each controller and updates when required.
    /// </summary>
    public class HandModelManager : MonoBehaviour
    {
        public GameObject rightHandPrefab;
        public GameObject leftHandPrefab;


        private GameObject rightPrefab;
        private GameObject leftPrefab;
        private bool handsSwapped = false;

        [Tooltip("Offset of trigger position from controller tracked position (for right hand).")]
        public Vector3 controllerOffset;

        public Vector3 controllerRotation;

        private Matrix4x4 rightOffset;

        private Matrix4x4 leftOffset;

        void Start()
        {
            #region Prepare Offset Matrices

            Vector3 leftControllerOffset = controllerOffset;
            leftControllerOffset[0] = -leftControllerOffset[0];

            Quaternion rotation = Quaternion.Euler(controllerRotation);

            Vector3 scale = Vector3.one;

            rightOffset = Matrix4x4.TRS(controllerOffset, rotation, scale);
            leftOffset = Matrix4x4.TRS(leftControllerOffset, rotation, scale);

            #endregion


        }

        public void SpawnModels()
        {
            if (rightPrefab == null && leftPrefab == null)
            {
                if (!handsSwapped)
                {
                    rightPrefab = Instantiate(rightHandPrefab);
                    leftPrefab = Instantiate(leftHandPrefab);
                }
                else
                {
                    rightPrefab = Instantiate(leftHandPrefab);
                    leftPrefab = Instantiate(rightHandPrefab);
                }

                SetModels();
            }
        }

        public void RemoveModels()
        {
            UnsetModels();

            Destroy(rightPrefab);
            Destroy(leftPrefab);
        }

        public void SwapHands()
        {
            if (PrefabsAvailable())
            {
                Debug.Log("Swapping Hands.");

                RemoveModels();

                handsSwapped = !handsSwapped;

                SpawnModels();
                
                //SetModels();
            }
        }

        public void SetModels()
        {
            if (PrefabsAvailable())
            {
   
                for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
                {
                    Hand hand = Player.instance.hands[handIndex];

                    if (hand != null)
                    {
                        if (hand.handType == SteamVR_Input_Sources.RightHand)
                        {
                            HandListener rightHand = hand.GetComponent<HandListener>();
                            rightHand.assignedObject = rightPrefab;
                            rightHand.mOffset = rightOffset;
                            rightHand.AttachObject();
                        }
                        if (hand.handType == SteamVR_Input_Sources.LeftHand)
                        {
                            HandListener leftHand = hand.GetComponent<HandListener>();
                            leftHand.assignedObject = leftPrefab;
                            leftHand.mOffset = leftOffset;
                            leftHand.AttachObject();
                        }
                    }
                }
            }
        }

        void UnsetModels()
        {
            for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
            {
                Hand hand = Player.instance.hands[handIndex];

                if (hand != null)
                {
                    if (hand.handType == SteamVR_Input_Sources.RightHand)
                    {
                        HandListener rightHand = hand.GetComponent<HandListener>();
                        rightHand.DetachObject();
                    }
                    if (hand.handType == SteamVR_Input_Sources.LeftHand)
                    {
                        HandListener leftHand = hand.GetComponent<HandListener>();
                        leftHand.DetachObject();
                    }
                }
            }
        }

        private bool PrefabsAvailable()
        {
            if (rightHandPrefab == null || leftHandPrefab == null)
            {
                Debug.LogError("Hand Prefabs not assigned, skipping request.");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

