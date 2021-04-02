using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace Game.Utility.Interaction
{
    public abstract class InteractableBase : MonoBehaviour
    {
        [SerializeField]
        protected SteamVR_Action_Boolean inputAction;

        [SerializeField]
        protected SteamVR_Input_Sources source;

        /// <summary>
        /// Get the source type of the currently assigned controller (e.g. left hand or right hand)
        /// </summary>
        /// <param name="controllerSource"></param>
        public void SetInputSource(SteamVR_Input_Sources controllerSource)
        {
            source = controllerSource;
        }


        void Update()
        {
            if (inputAction[source].stateDown)
            {
                //Debug.Log(source + " Trigger Pressed.");

                OnTriggerDown();
            }

            if (inputAction[source].stateUp)
            {
                //Debug.Log(source + " Trigger Released.");

                OnTriggerUp();
            }
        }

        protected virtual void OnTriggerDown()
        {

        }

        protected virtual void OnTriggerUp()
        {

        }
    }
}

