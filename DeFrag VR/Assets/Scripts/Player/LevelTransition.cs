using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Data;
using UnityEngine.Events;

namespace Game.Utility
{
    //Called by an event listener
    //GUI controlled by LevelTransitionEditor
    public class LevelTransition : MonoBehaviour
    {
        [SerializeField]
        private GameManager gameManager;
        
        [SerializeField]
        private IntReference targetScene;

        [SerializeField]
        private FadeProfileVR fadeProfile;

        [SerializeField]
        private float minLoadingWaitTime;

        [SerializeField]
        private GameEvent movePlayerEvent;

        [SerializeField]
        private GameEvent loadingScreenEvent;

        #region Loading Screen Properties
        //public bool useLoadingScreen = false;
        //public Transform loadScreenLocation;
        //public TransformVariable loadScreenLocationVariable;
        //public float minLoadingWaitTime;
        //public GameEvent loadingScreenEvent;
        #endregion

        //public FadeProfileVR profileVR;

        //private void OnEnable()
        //{
        //    profileVR.FadeIn();     //Fade In at the beginning of a level
        //}

        public void FadeIn()
        {
            if (gameManager != null)
            {
                gameManager.currentFadeProfile.FadeIn();
            }

        }

        public void StartLoadingScreen()
        {
            StartCoroutine(LoadingScreen());


        }

        public void ChangeScene()
        {
            if (gameManager != null)
            {
                gameManager.SetCurrentFadeProfile();
            }

            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadingScreen()
        {
            if (movePlayerEvent != null)
            {
                movePlayerEvent.Raise();
            }
            loadingScreenEvent.Raise();

            yield return new WaitForSeconds(minLoadingWaitTime);

            ChangeScene();
        }

        private IEnumerator LoadScene()
        {
            

            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(targetScene);
            loadOperation.allowSceneActivation = false;
            while (!loadOperation.isDone)
            {
                if (loadOperation.progress >= 0.9f)
                {
                    //Fade Out
                    if (gameManager != null)
                    {
                        gameManager.currentFadeProfile.FadeOut();
                        yield return new WaitForSeconds(gameManager.currentFadeProfile.fadeTime + 0.1f);
                    }

                    loadOperation.allowSceneActivation = true;
                    //Fade In has to be handled by a different script (Level Setup)
                }
            }
        }

        #region Old Code
        //public void LaunchCoroutine()
        //{
        //    StartCoroutine(SceneTransition(targetScene));
        //}

        //IEnumerator SceneTransition(IntReference sceneIndex)
        //{
        //    if (useLoadingScreen)
        //    {
        //        //set move location if necessary data available
        //        if (loadScreenLocation != null && loadScreenLocationVariable != null)
        //        {
        //            loadScreenLocationVariable.SetTransformValue(loadScreenLocation);
        //            loadingScreenEvent.Raise(); //This event triggers player relocation (make a script) and any loading space effects

        //            yield return new WaitForSeconds(minLoadingWaitTime);
        //        }
        //        else
        //        {
        //            Debug.LogError("Loading Screen position data not assigned, skipping loading screen");
        //        }

        //    }

        //    AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneIndex);
        //    loadOperation.allowSceneActivation = false;
        //    while (!loadOperation.isDone)
        //    {
        //        if (loadOperation.progress >= 0.9f)
        //        {
        //            //Fade Out
        //            profileVR.FadeOut();
        //            yield return new WaitForSeconds(profileVR.fadeTime + 0.1f);
        //            loadOperation.allowSceneActivation = true;
        //            //Fade In has to be handled by a different script

        //        }
        //    }

        //}
        #endregion
    }

}
