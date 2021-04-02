using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Utility;
using Game.Data;

namespace Game
{
    /// <summary>
    /// Spawns gameobjects into the scene on a defined schedule
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private GameManager gameManager;
        
        public SpawnInstance[] spawnList;

        public Transform[] path;

        #region Spawner Active
        private bool _SpawnerActive = true;
        public bool SpawnerActive
        {
            get { return _SpawnerActive; }
        }
        #endregion

        private Queue<SpawnProfileBase> spawnQueue = new Queue<SpawnProfileBase>();

        private GameObject spawnActiveEnemy;

        private ActiveEnemiesSet activeEnemiesSet;

        [SerializeField]
        private IntReference currentStep;

        private float timeToNextSpawn;

        [SerializeField]
        private int spawnCounter = 0;

        private void OnEnable()
        {
            activeEnemiesSet = gameManager.ActiveEnemiesSet;
        }

        public void Raise()
        {
            _SpawnerActive = true;
            spawnCounter = 0;
            StartCoroutine(StartSpawner());
            StartCoroutine(SlowUpdate());
        }

        private IEnumerator StartSpawner()
        {
            //Wait for SpawnInstance.interval
            //Add to spawn queue
            //Check if no active enemy uses this spawner
            //Check if SpawnInstance.step <= GameManager.step
            //Spawn when free

            for (int i = 0; i < spawnList.Length; i++)
            {
                //Wait for steps to catch up
                yield return new WaitWhile(() => spawnList[i].step > currentStep);

                spawnQueue.Enqueue(spawnList[i].prefab);

                Debug.Log(spawnList[i].prefab + " added to spawn queue.");
            }
            yield return new WaitWhile(() => spawnQueue.Count > 0); //Wait until the spawn queue is empty
            FinishSpawner();
        }

        /// <summary>
        /// Low frequency update
        /// </summary>
        /// <returns></returns>
        private IEnumerator SlowUpdate()
        {
            float slowUpdateFrequency = gameManager.SlowUpdateFrequency;
            //float throwErrorDelay = 0f;

            while (SpawnerActive || spawnQueue.Count > 0)
            {
                if (spawnActiveEnemy == null || !activeEnemiesSet.Items.Contains(spawnActiveEnemy))
                {
                    Debug.LogWarning(spawnList.Length);
                    yield return new WaitForSeconds(spawnList[spawnCounter].spawnDelay);
                    spawnCounter++;

                    //StartCoroutine(Spawn());
                    Spawn();
                }
                #region Fail Safe
                //Stop the game hanging if queue not populating but all enemies are defeated.
                //if (spawnQueue.Count == 0)
                //{
                //    if (activeEnemiesSet.Items.Count == 0)
                //    {
                //        throwErrorDelay = throwErrorDelay + slowUpdateFrequency;
                //        if (throwErrorDelay >= Mathf.Max(10f, timeToNextSpawn))
                //        {
                //            FinishSpawner();
                //            throw new UnityException("Current wave not resolving, check spawners to ensure spawn conditions are being met.");
                //        }
                //    }
                //}
                #endregion
                yield return new WaitForSeconds(slowUpdateFrequency);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Spawn()
        {
            // There is no active enemy associated with this spawner
            if (spawnActiveEnemy == null || !activeEnemiesSet.Items.Contains(spawnActiveEnemy))
            {


                if (spawnQueue.Count > 0)   //Avoids a 'ThrowForEmptyQueue' error
                {

                    //Spawn first item on the list
                    spawnActiveEnemy = Instantiate(spawnQueue.Peek().prefab, transform.position, transform.rotation);

                    //Remove from the list
                    spawnQueue.Dequeue();

                    //Assign path
                    Movement prefabMovementClass = spawnActiveEnemy.GetComponentInChildren<Movement>();
                    if (prefabMovementClass != null)
                    {
                        prefabMovementClass.path = path;
                    }
                }
            }
        }

        private void FinishSpawner()
        {
            Debug.Log("Spawner finished.");
            _SpawnerActive = false;
        }
    }



    [System.Serializable]
    public struct SpawnInstance
    {
        /// <summary>
        /// Additional control for the pacing across spawners. Can hold until step is reached.
        /// </summary>
        [Min(0)]
        public int step;    //Step is in GameManager, how to update it?

        [Min(0)]
        public float spawnDelay;

        public SpawnProfileBase prefab;   //Use a scriptable object and polymorphism to construct different types of spawn?
    }

}



