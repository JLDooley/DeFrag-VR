﻿using System.Collections;
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

        /// <summary>
        /// Triggers WaveHandler via a Game Event Listener.
        /// </summary>
        [SerializeField]
        [Tooltip("Triggers WaveHandler via a Game Event Listener.")]
        private GameEvent EnableWaveTriggers;

        #endregion
        //Internal Properties
        #region Internal Propeties
        private bool spawnersDone = false;

        #endregion


        /// <summary>
        /// Starts spawners or calls next WaveTrigger(s) if none detected.
        /// </summary>
        public void StartWave()
        {
            //Make sure Current Wave Step is reset to 0
            waveStep.SetValue(0);
            
            if (StageIndex == stage && spawners.Length > 0)
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
                EnableWaveTriggers.Raise();
            }
        }

        /// <summary>
        /// While a wave is running, check to see if all spawners are finished and all enemies are defeated. Enables next WaveTrigger(s) once done.
        /// </summary>
        /// <returns></returns>
        private IEnumerator CheckWaveDone()
        {
        
            while (!spawnersDone)
            {
                
                spawnersDone = CheckSpawnersStatus();
            
                //Check every half a second, to reduce performance load (and hopefully avoid race conditions)
                yield return new WaitForSeconds(0.5f);
            }

            while (!ActiveEnemiesSet.IsEmpty())
            {
                yield return new WaitForSeconds(0.5f);
            }

            Debug.Log("Wave complete, enabling WaveTriggers for next stage.");

            //Event: Enable Wave Triggers
            EnableWaveTriggers.Raise();
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

