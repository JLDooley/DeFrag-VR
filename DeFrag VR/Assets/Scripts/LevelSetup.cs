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
        private GameEvent enableWaveTriggers;

        private void Awake()
        {
            if (levelStartPosition == null)
            {
                levelStartPosition = transform;
            }
            
            if (gameManager !=null)
            {
                if (gameManager.playerInstance == null)
                {
                    gameManager.playerInstance = Instantiate(gameManager.playerPrefab, levelStartPosition.position, levelStartPosition.rotation);
                }
            }
        }

        private void OnEnable()
        {
            stageIndex.SetValue(0);

        
        }

        private void Start()
        {
            enableWaveTriggers.Raise();
        }


    }
}

