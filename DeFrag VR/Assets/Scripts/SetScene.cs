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
        private IntVariable targetScene;

        [SerializeField]
        private GameEvent sceneChangeEvent;


        public void TargetThisScene()
        {
            targetScene.SetValue(SceneManager.GetActiveScene().buildIndex);

            Debug.Log("Target Scene: " + SceneManager.GetSceneByBuildIndex(targetScene.Value).name);

            sceneChangeEvent.Raise();
        }

        public void TargetScene(int sceneIndex)
        {
            targetScene.SetValue(sceneIndex);

            sceneChangeEvent.Raise();
        }
    }
}

