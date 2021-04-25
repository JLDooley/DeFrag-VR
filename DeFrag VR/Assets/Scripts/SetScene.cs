using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Game.Utility
{
    public class SetScene : MonoBehaviour
    {
        [SerializeField]
        private GameManager gameManager;
        
        [SerializeField]
        private IntVariable targetScene;

        [SerializeField]
        private EventRaiser sceneChangeEvents;



        [Tooltip("To be passed to the GameManager for reference by the MovePlayer script.")]
        [SerializeField]
        private FadeProfileVR fadeProfile;


        public void TargetThisScene()
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

            targetScene.SetValue(SceneManager.GetActiveScene().buildIndex);

            Debug.Log("Target Scene: " + SceneManager.GetSceneByBuildIndex(targetScene.Value).name);


            if (sceneChangeEvents != null)
            {
                sceneChangeEvents.Raise();
            }

        }

        public void TargetScene(int sceneIndex)
        {
            targetScene.SetValue(sceneIndex);

            if (sceneChangeEvents != null)
            {
                sceneChangeEvents.Raise();
            }
        }
    }
}

