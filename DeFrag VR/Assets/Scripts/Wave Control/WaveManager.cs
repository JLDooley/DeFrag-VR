using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;
using Game.Utility;

namespace Game
{
    /// <summary>
    /// Controls the Spawners in a wave. Triggered by a WaveTrigger Game Event.
    /// </summary>
    public class WaveManager : MonoBehaviour
    {
        [SerializeField]
        private GameManager gameManager;

        [Tooltip("Index condition to meet for triggering this Instance")]
        [Min(1)]    //Shouldn't be any waves in StageIndex 0
        public int stage;

        public Spawner[] spawners;

        //Global Properties
        #region Global Properties
    
        [SerializeField]
        private IntReference StageIndex;

        [SerializeField]
        private IntVariable waveStep;

        [SerializeField]
        private ActiveEnemiesSet ActiveEnemiesSet;

        [SerializeField]
        [Tooltip("Runs the wave regardless of the current stage. TESTING ONLY, do not call with a GameEvent.")]
        private bool ignoreStage = false;

        /// <summary>
        /// Trigger immediate wave complete events (e.g. music changes).
        /// </summary>
        [SerializeField]
        [Tooltip("Trigger immediate wave complete events (e.g. music changes).")]
        private EventRaiser WaveCompleteEvent;

        /// <summary>
        /// Triggers WaveHandler or Level Complete Menu via a Game Event Listener.
        /// </summary>
        [SerializeField]
        [Tooltip("Triggers WaveHandler or Level Complete Menu via a Game Event Listener.")]
        private EventRaiser DelayedWaveCompleteEvent;

        #endregion
        //Internal Properties
        #region Internal Propeties
        private bool spawnersDone = false;
        private bool waveDone = false;
        #endregion


        /// <summary>
        /// Starts spawners or calls next WaveTrigger(s) if none detected.
        /// </summary>
        public void StartWave()
        {
            //Make sure Current Wave Step is reset to 0
            waveStep.SetValue(0);
            spawnersDone = false;
            waveDone = false;

            if ((StageIndex == stage || ignoreStage) && spawners.Length > 0)
            {
                for (int i = spawners.Length-1; i >= 0; i--)
                {
                    spawners[i].Raise();
                }
                StartCoroutine(CheckWaveDone());
            }
            else if (spawners.Length <= 0)
            {
                Debug.LogError("No spawners detected, preparing next stage.");
                WaveCompleteEvent.Raise();
                DelayedWaveCompleteEvent.Raise();
            }
        }

        /// <summary>
        /// While a wave is running, check to see if all spawners are finished and all enemies are defeated. Enables next WaveTrigger(s) once done.
        /// </summary>
        /// <returns></returns>
        private IEnumerator CheckWaveDone()
        {
            float slowUpdateFrequency = gameManager.SlowUpdateFrequency;
            while (!spawnersDone)
            {
                float timer = 0f;
                while (timer < slowUpdateFrequency)
                {
                    yield return new WaitWhile(() => gameManager.IsPaused);
                    timer += GameManager.deltaTime;
                    yield return null;
                }

                spawnersDone = CheckSpawnersStatus();
            
                //Check every half a second, to reduce performance load (and hopefully avoid race conditions)
                //yield return new WaitForSeconds(0.5f);
            }

            int counter = 0;

            while (!waveDone)
            {
                if (!ActiveEnemiesSet.IsEmpty())
                {
                    counter = 0;    //Reset the counter
                    //Debug.Log("Set not empty.");
                }
                else if (!gameManager.IsPaused)
                {
                    Debug.Log("GameManager not paused.");
                    while (!gameManager.IsPaused && counter <= 3)   //Set is empty and game isn't paused (a common cause of an empty set)
                    {
                        counter++;  //Does the set remain empty for 3 unpaused frames
                        Debug.Log("Counter: " + counter);
                        yield return null;
                    }
                    if (counter > 3)
                    {
                        waveDone = true;    //If so, wave is probably done
                    }
                }
                else
                {
                    counter = 0;    //Reset the counter
                }

                yield return new WaitForSeconds(slowUpdateFrequency);
            }

            Debug.Log("Wave complete, enabling WaveTriggers for next stage.");

            //Event: Enable Wave Triggers
            WaveCompleteEvent.Raise();     //If this is the WaveManager for the final wave, trigger the level complete events instead
            DelayedWaveCompleteEvent.Raise();
        }

        /// <summary>
        /// Checks if all spawners in the wave have finished spawning.
        /// </summary>
        /// <returns>Returns true if all spawners have finished spawning enemies.</returns>
        private bool CheckSpawnersStatus()
        {
            for (int i = spawners.Length - 1; i >= 0; i--)
            {
                //Debug.Log(spawners[i].SpawnerActive);
                if (spawners[i].SpawnerActive)
                {
                    //Debug.Log(spawners[i] + " status: False");
                    //A spawner was not finished (SpawnerActive = True), return false
                    return false;
                }
            }
            //All spawners were finished, return true
            Debug.Log("Status status: True");
            return true;
        }
    }
}

