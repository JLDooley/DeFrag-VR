using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utility
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField]
        GameManager gameManager;

        [SerializeField]
        private Transform anchorPoint;

        [SerializeField]
        private GameObject pauseMenu;

        [SerializeField]
        private GameObject failStateMenu;

        private void OnEnable()
        {
            anchorPoint = transform;
        }

        public void SetPause()
        {
            if (gameManager != null)
            {
                if (pauseMenu.activeInHierarchy)
                {
                    //Pause game
                    gameManager.IsPaused = true;
                }
                else
                {
                    //Unpause game
                    gameManager.IsPaused = false;
                }
            }
            else
            {
                Debug.LogError(this.name + ": GameManager not assigned.");
            }
        }

        public void TogglePauseMenu()
        {
            Instantiate(pauseMenu, anchorPoint.position, anchorPoint.rotation);

            SetPause();

        }

        public void ToggleFailStateMenu()
        {
            Instantiate(failStateMenu, anchorPoint.position, anchorPoint.rotation);

            SetPause();
        }
    }
}

