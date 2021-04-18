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

        //private Queue<SpawnProfileBase> spawnQueue = new Queue<SpawnProfileBase>();

        private GameObject spawnActiveEnemy;

        //private ActiveEnemiesSet activeEnemiesSet;

        [SerializeField]
        private IntReference currentStep;

        //[SerializeField]
        //private int spawnCounter = 0;

        private bool spawnListFinished = false;

        private void OnEnable()
        {
            //activeEnemiesSet = gameManager.ActiveEnemiesSet;
        }

        public void Raise()
        {
            _SpawnerActive = true;

            spawnListFinished = false;

            //spawnCounter = 0;
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
            #region Old Code
            //for (int i = 0; i < spawnList.Length; i++)
            //{
            //    //Wait for steps to catch up
            //    yield return new WaitWhile(() => spawnList[i].step > currentStep);

            //    spawnQueue.Enqueue(spawnList[i].prefab);

            //    Debug.Log(spawnList[i].prefab + " added to spawn queue.");
            //}
            //yield return new WaitWhile(() => spawnQueue.Count > 0); //Wait until the spawn queue is empty

            //spawnListFinished = true;

            //FinishSpawner();
            #endregion

            for (int i = 0; i < spawnList.Length; i++)
            {
                yield return new WaitWhile(() => spawnActiveEnemy != null);

                yield return new WaitWhile(() => spawnList[i].step > currentStep);

                //yield return new WaitForSeconds(spawnList[i].spawnDelay);

                float timer = 0f;
                while (timer < spawnList[i].spawnDelay)
                {
                    yield return new WaitWhile(() => gameManager.IsPaused);
                    timer += Time.deltaTime;
                    yield return null;
                }

                

                Spawn(spawnList[i].prefab);
            }
            spawnListFinished = true;
        }

        /// <summary>
        /// Low frequency update
        /// </summary>
        /// <returns></returns>
        private IEnumerator SlowUpdate()
        {
            float slowUpdateFrequency = gameManager.SlowUpdateFrequency;
            Debug.Log(slowUpdateFrequency);

            #region Old Code
            //while (SpawnerActive || spawnQueue.Count > 0)
            //{
            //    if (spawnListFinished && spawnQueue.Count == 0)
            //    {
            //        FinishSpawner();
            //    }

            //    if (spawnActiveEnemy == null || !activeEnemiesSet.Items.Contains(spawnActiveEnemy))
            //    {
            //        if (spawnList[spawnCounter].spawnDelay > 0)
            //        {
            //            yield return new WaitForSeconds(spawnList[spawnCounter].spawnDelay);
            //        }

            //        spawnCounter++;
            //        Debug.Log(gameObject.name + " Spawn Counter: " + spawnCounter);
            //        Debug.Log(Time.time);

            //        Spawn();
            //    }

            //    #region Fail Safe
            //    //Stop the game hanging if queue not populating but all enemies are defeated.
            //    //if (spawnQueue.Count == 0)
            //    //{
            //    //    if (activeEnemiesSet.Items.Count == 0)
            //    //    {
            //    //        throwErrorDelay = throwErrorDelay + slowUpdateFrequency;
            //    //        if (throwErrorDelay >= Mathf.Max(10f, timeToNextSpawn))
            //    //        {
            //    //            FinishSpawner();
            //    //            throw new UnityException("Current wave not resolving, check spawners to ensure spawn conditions are being met.");
            //    //        }
            //    //    }
            //    //}
            //    #endregion

            //    yield return new WaitForSeconds(slowUpdateFrequency);
            //}
            #endregion

            while (SpawnerActive)
            {
                if (spawnListFinished)
                {
                    FinishSpawner();
                }
                //yield return new WaitForSeconds(slowUpdateFrequency);
                float timer = 0f;
                while (timer < slowUpdateFrequency)
                {
                    yield return new WaitWhile(() => gameManager.IsPaused);
                    timer += GameManager.deltaTime;
                    yield return null;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void Spawn(SpawnProfileBase spawnProfile)
        {
            #region Old Code
            //// There is no active enemy associated with this spawner
            //if (spawnActiveEnemy == null || !activeEnemiesSet.Items.Contains(spawnActiveEnemy))
            //{


            //    if (spawnQueue.Count > 0)   //Avoids a 'ThrowForEmptyQueue' error
            //    {

            //        //Spawn first item on the list
            //        spawnActiveEnemy = Instantiate(spawnQueue.Peek().prefab, transform.position, transform.rotation);

            //        //Remove from the list
            //        spawnQueue.Dequeue();

            //        //Assign path
            //        Movement prefabMovementClass = spawnActiveEnemy.GetComponentInChildren<Movement>();
            //        if (prefabMovementClass != null)
            //        {
            //            prefabMovementClass.path = path;
            //        }
            //    }
            //}
            #endregion
            //Debug.Log("Spawner: " + gameObject.name + "; Spawning: " + spawnProfile.prefab);
            spawnActiveEnemy = Instantiate(spawnProfile.prefab, transform.position, transform.rotation);

            //Assign path
            Movement prefabMovementClass = spawnActiveEnemy.GetComponentInChildren<Movement>();
            if (prefabMovementClass != null)
            {
                prefabMovementClass.path = path;
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



