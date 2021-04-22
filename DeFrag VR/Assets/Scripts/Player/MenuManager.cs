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

        [SerializeField]
        private GameObject failStateMenu;

        private GameObject currentMenu;


        public void TogglePauseMenu()
        {
            if (currentMenu != failStateMenu)
            {
                EnableMenu(pauseMenu);
            }
            
        }

        public void ToggleFailStateMenu()
        {
            EnableMenu(failStateMenu);
        }

        public void RemoveMenu()
        {
            if (currentMenu != null)
            {
                Destroy(currentMenu);
            }

            SetPause();

        }

        private void EnableMenu(GameObject menu)
        {
            if (currentMenu == null)
            {
                menu.SetActive(true);
                currentMenu = menu;
            }
            else if (currentMenu = menu)
            {
                currentMenu.SetActive(false);
                currentMenu = null;
            }
            else    //Different menu active, switch menus.
            {
                currentMenu.SetActive(false);
                menu.SetActive(true);
                currentMenu = menu;
            }

            SetPause(); //Toggle pause state in the Game Manager
        }

        public void SetPause()
        {
            if (gameManager != null)
            {
                if (currentMenu != null)
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

