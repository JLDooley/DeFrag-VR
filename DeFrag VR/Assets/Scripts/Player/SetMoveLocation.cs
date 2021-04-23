using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;

namespace Game.Utility
{
    /// <summary>
    /// Place on objects used to move the player around (e.g. Triggers) and call as part of the trigger.
    /// </summary>
    public class SetMoveLocation : MonoBehaviour
    {
        [SerializeField]
        private Transform DefaultTargetDestination;

        [SerializeField]
        private TransformVariable targetReference;

        [SerializeField]
        private EventRaiser movePlayerEvents;

        [SerializeField]
        private GameManager gameManager;

        [Tooltip("To be passed to the GameManager for reference by the MovePlayer script.")]
        [SerializeField]
        private FadeProfileVR fadeProfile;

        public void UseDefaultTarget()
        {
            if (gameManager != null)
            {
                if (fadeProfile != null)
                {
                    gameManager.QueueFadeProfile(fadeProfile);
                }
                else
                {
                    Debug.Log(gameObject.name + ": No FadeProfile found, using default FadeProfile.");
                    gameManager.QueueFadeProfile(gameManager.defaultFadeProfile);
                }
            }


            //Debug.Log("Preparing to move player.");
            if (DefaultTargetDestination != null)
            {
                SetDestinationAndMove(DefaultTargetDestination);
            }
            else
            {
                Debug.LogError(this + ": No destination provided.");
            }

        }

        public void SetDestinationAndMove(Transform target)
        {
            //Debug.Log("Destination provided: " + targetDestination);                
            targetReference.SetTransformValue(target); 
            //Debug.Log(targetDestination.position);
            //Debug.Log("Moving");
            movePlayerEvents.Raise();


        }

    }
}

