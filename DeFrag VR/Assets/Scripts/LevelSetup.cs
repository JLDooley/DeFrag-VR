using Game.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utility
{
    public class LevelSetup : MonoBehaviour
    {
        [SerializeField]
        private GameManager gameManager;

        [SerializeField]
        private Transform levelStartPosition;

        [SerializeField]
        private IntVariable stageIndex;

        [SerializeField]
        private EventRaiser initialStartupEvents;

        [SerializeField]
        private EventRaiser playerFadeInEvents;

        [SerializeField]
        private EventRaiser finalStartupEvents;


        private void Awake()
        {
            if (levelStartPosition == null)
            {
                levelStartPosition = transform;
            }
            
            if (gameManager !=null)
            {
                //GameObject player = gameManager.playerInstance;   //Doesn't work, need to directly reference
                if (gameManager.playerInstance == null)
                {
                    gameManager.playerInstance = Instantiate(gameManager.playerPrefab, levelStartPosition.position, levelStartPosition.rotation);
                }
                else
                {
                    gameManager.playerInstance.transform.SetPositionAndRotation(levelStartPosition.position, levelStartPosition.rotation);
                }
            }
        }

        private void OnEnable()
        {
            stageIndex.SetValue(0);

            StartCoroutine(StartUp());
        }

        private IEnumerator StartUp()
        {
            if (initialStartupEvents != null)
            {
                initialStartupEvents.Raise();
            }
            
            yield return null;

            if (playerFadeInEvents != null)
            {
                playerFadeInEvents.Raise();
            }

            yield return null;

            if (finalStartupEvents != null)
            {
                finalStartupEvents.Raise();
            }
        }


    }
}

