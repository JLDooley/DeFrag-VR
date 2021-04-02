using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace Game.Utility.Interaction
{
    public class HandListener : MonoBehaviour
    {
        [SerializeField]
        protected SteamVR_Action_Boolean inputAction;

        [SerializeField]
        protected SteamVR_Input_Sources source;

        public GameObject assignedObject;

        public Matrix4x4 mOffset { get; set; }


        public void DetachObject()
        {
            FixedJoint joint = GetComponent<FixedJoint>();

            if (joint.connectedBody != null)
            {
                joint.connectedBody = null;

                Debug.Log("Decoupling models from controllers");
            }
        }

        public void AttachObject()
        {
        
            FixedJoint joint = GetComponent<FixedJoint>();

            //Controller transform plus an offset (to the controller's trigger)
            Matrix4x4 m = transform.localToWorldMatrix * mOffset;

            //Align the grabbed object to the controller
            assignedObject.transform.SetPositionAndRotation(m.GetColumn(3), m.GetRotation());

            joint.connectedBody = assignedObject.GetComponent<Rigidbody>();

            //For multiple inputs
            InteractableBase[] interactables = assignedObject.GetComponents<InteractableBase>();
            for (int i = interactables.Length - 1; i >= 0; i--)
            {
                interactables[i].SetInputSource(source);
            }
        }
    }
}

