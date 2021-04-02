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
            if (targetDestination != null)
            {
                if (gameManager != null)
                {
                    if (fadeProfile != null)
                    {
                        gameManager.currentFadeProfile = fadeProfile;
                    }
                    else
                    {
                        gameManager.currentFadeProfile = gameManager.defaultFadeProfile;
                    }
                }
                
                targetReference.SetTransformValue(targetDestination);
                movePlayerEvent.Raise();
            }
            else
            {
                Debug.LogError(this + ": No destination provided.");
            }

        }

    }
}

