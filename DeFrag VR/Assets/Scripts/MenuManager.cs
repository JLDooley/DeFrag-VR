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

        [SerializeField]
        private GameObject currentMenu;

        private void OnEnable()
        {
            anchorPoint = transform;
        }

        public void TogglePauseMenu()
        {
            if (currentMenu != failStateMenu)
            {
                CreateMenu(pauseMenu);
            }
            
        }

        public void ToggleFailStateMenu()
        {
            CreateMenu(failStateMenu);
        }

        public void RemoveMenu()
        {
            if (currentMenu != null)
            {
                Destroy(currentMenu);
            }

            SetPause();

        }

        private void CreateMenu(GameObject menu)
        {
            Debug.Log("Current Menu: " + currentMenu);
            
            if (currentMenu == null)    //No menu active
            {
                Debug.Log("Creating new menu.");
                currentMenu = Instantiate(menu, anchorPoint.position, anchorPoint.rotation);
            }
            else if (currentMenu = menu)   //This menu already exists, remove it.
            {
                Debug.Log("Destroying existing menu.");
                Destroy(currentMenu);
            }
            else    //A different menu already exists, replace it.
            {
                Debug.Log("Replacing existing menu.");
                Destroy(currentMenu);   
                currentMenu = Instantiate(menu, anchorPoint.position, anchorPoint.rotation);
            }

            SetPause();

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

