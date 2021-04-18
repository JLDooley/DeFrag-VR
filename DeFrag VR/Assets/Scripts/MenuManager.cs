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
        private GameObject pauseMenu;

        public void TogglePauseMenu()
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);

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
    }
}

