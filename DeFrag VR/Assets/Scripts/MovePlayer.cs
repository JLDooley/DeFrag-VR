using System.Collections;
using UnityEngine;
using Game.Data;

namespace Game.Utility
{

    /// <summary>
    ///Takes the position and rotation data from a TransformVariable Scriptable Object and converts it into a transform value for the GameObject.
    ///Optionally also accepts a fade profile for SteamVR fading.
    /// </summary>
    public class MovePlayer : MonoBehaviour
    {
        [SerializeField]
        private Transform playerTransform;

        [SerializeField]
        private TransformVariable destination;

        [SerializeField]
        private GameManager gameManager;

        private FadeProfileVR fadeProfile;

        private void Awake()
        {
            if (playerTransform == null)
            {
                playerTransform = transform.root;
            }
        }

    

        public void MovePlayerTo()
        {
            fadeProfile = gameManager.currentFadeProfile;
            Vector3 position = destination.posValue;
            Quaternion rotation = destination.rotValue;

            if (fadeProfile != null)
            {
                StartCoroutine(FadeAndMove(position, rotation, fadeProfile));
            }
            else
            {
                playerTransform.transform.SetPositionAndRotation(position, rotation);
            }
        }


        private IEnumerator FadeAndMove(Vector3 position, Quaternion rotation, FadeProfileVR fadeProfile)
        {
            fadeProfile.FadeOut();
            yield return new WaitForSeconds(fadeProfile.fadeTime + 0.1f);
            playerTransform.transform.SetPositionAndRotation(position, rotation);
            fadeProfile.FadeIn();
        }

    
    }

}

