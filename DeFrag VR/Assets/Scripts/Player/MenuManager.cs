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
        private GameObject successStateMenu;

        [SerializeField]
        private GameObject failStateMenu;

        [SerializeField]
        private GameObject currentMenu;


        public void TogglePauseMenu()
        {
            if (currentMenu != successStateMenu && currentMenu != failStateMenu)
            {
                //Debug.Log("Toggle Pause Menu.");

                EnableMenu(pauseMenu);
            }
            
        }

        public void ToggleSuccessStateMenu()
        {
            //Debug.Log("Toggle Success State Menu.");

            EnableMenu(successStateMenu);
        }

        public void ToggleFailStateMenu()
        {
            //Debug.Log("Toggle Fail State Menu.");

            EnableMenu(failStateMenu);
        }

        public void RemoveMenu()
        {
            if (currentMenu != null)
            {
                currentMenu.SetActive(false);
                currentMenu = null;
            }

            //SetPause();   //Will call this separately so that the game doesn't unpause while fading to change scene

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

        /// <summary>
        /// Pauses the game if a menu is active, otherwise unpauses the game.
        /// </summary>
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

