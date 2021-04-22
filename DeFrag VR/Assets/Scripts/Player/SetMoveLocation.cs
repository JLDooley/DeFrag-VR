using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;

namespace Game.Utility
{
    public class SetMoveLocation : MonoBehaviour
    {
        [SerializeField]
        private Transform targetDestination;

        [SerializeField]
        private TransformVariable targetReference;

        [SerializeField]
        private GameEvent movePlayerEvent;

        [SerializeField]
        private GameManager gameManager;

        [SerializeField]
        private FadeProfileVR fadeProfile;

        public void SetDestinationAndMove()
        {
            //Debug.Log("Preparing to move player.");
            if (targetDestination != null)
            {
                //Debug.Log("Destination provided: " + targetDestination);
                if (gameManager != null)
                {
                    //Debug.Log("GameManager present.");
                    if (fadeProfile != null)
                    {
                        //Debug.Log("FadeProfile available.");
                        gameManager.currentFadeProfile = fadeProfile;
                    }
                    else
                    {
                        //Debug.Log("Using default FadeProfile.");
                        gameManager.currentFadeProfile = gameManager.defaultFadeProfile;
                    }
                }
                
                targetReference.SetTransformValue(targetDestination);
                //Debug.Log(targetDestination.position);
                //Debug.Log("Moving");
                movePlayerEvent.Raise();
            }
            else
            {
                Debug.LogError(this + ": No destination provided.");
            }

        }

    }
}

