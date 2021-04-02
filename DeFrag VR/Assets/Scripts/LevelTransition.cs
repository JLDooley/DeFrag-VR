using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Data;

namespace Game.Utility
{
    //Called by an event listener
    //GUI controlled by LevelTransitionEditor
    public class LevelTransition : MonoBehaviour
    {
        public IntReference targetScene;

        #region Loading Screen Properties
        public bool useLoadingScreen = false;
        public Transform loadScreenLocation;
        public TransformVariable loadScreenLocationVariable;
        public float minLoadingWaitTime;
        public GameEvent loadingScreenEvent;
        #endregion

        public FadeProfileVR profileVR;

        private void OnEnable()
        {
            profileVR.FadeIn();     //Fade In at the beginning of a level
        }

        public void LaunchCoroutine()
        {
            StartCoroutine(SceneTransition(targetScene));
        }

        IEnumerator SceneTransition(IntReference sceneIndex)
        {
            if (useLoadingScreen)
            {
                //set move location if necessary data available
                if (loadScreenLocation != null && loadScreenLocationVariable != null)
                {
                    loadScreenLocationVariable.SetTransformValue(loadScreenLocation);
                    loadingScreenEvent.Raise(); //This event triggers player relocation (make a script) and any loading space effects

                    yield return new WaitForSeconds(minLoadingWaitTime);
                }
                else
                {
                    Debug.LogError("Loading Screen position data not assigned, skipping loading screen");
                }

            }

            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneIndex);
            loadOperation.allowSceneActivation = false;
            while (!loadOperation.isDone)
            {
                if (loadOperation.progress >= 0.9f)
                {
                    //Fade Out
                    profileVR.FadeOut();
                    yield return new WaitForSeconds(profileVR.fadeTime + 0.1f);
                    loadOperation.allowSceneActivation = true;
                    //Fade In has to be handled by a different script

                }
            }

        }
    }

}
