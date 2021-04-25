using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;
using Packages.Rider.Editor.UnitTesting;

namespace Game.Utility
{
    /// <summary>
    /// Central control for the game mechanics (player position, health, difficulty level, lists of entities, etc.).
    /// </summary>
    [CreateAssetMenu(fileName = "Game Manager", menuName = "Utility/Game Manager", order = 0)]
    public class GameManager : ScriptableObject
    {
    #if UNITY_EDITOR
        [Multiline]
        public string Description = "";
#endif
        
        #region Sets
        [Header("Sets")]
        #region Active Enemies Set
        [SerializeField]
        private ActiveEnemiesSet _ActiveEnemiesSet;
        public ActiveEnemiesSet ActiveEnemiesSet
        {
            get { return _ActiveEnemiesSet; }
        }
        #endregion
        #region
        [SerializeField]
        private PauseSet _PauseSet;
        public PauseSet PauseSet
        {
            get { return _PauseSet; }
        }
        #endregion

        #endregion

        [Header("Player Properties")]
        public GameObject playerPrefab;
        public GameObject playerInstance { get; set; }
        public TransformVariable playerTransform;
        public FloatVariable MaxHealth;

        [Header("Fade Profiles")]
        public FadeProfileVR defaultFadeProfile;

        public FadeProfileVR currentFadeProfile { get; set; }

        public Queue<FadeProfileVR> fadeQueue = new Queue<FadeProfileVR>();

        [Header("Game Settings")]
        #region Slow Update Frequency
        [SerializeField]
        [Min(0.01f)]
        private float _SlowUpdateFrequency = 0.05f;
        /// <summary>
        /// Update rate for Slow Update Coroutines (Fastest: 100fps).
        /// </summary>
        public float SlowUpdateFrequency
        {
            get { return _SlowUpdateFrequency; }
        }
        #endregion

        #region Custom Time Control
        [Min(0f)]
        public static float LocalTimeScale = 1f;
        public static float deltaTime
        {
            get
            {
                return Time.deltaTime * LocalTimeScale;
            }
        }
        public static float fixedDeltaTime
        {
            get
            {
                return Time.fixedDeltaTime * LocalTimeScale;
            }
        }
        //[SerializeField]
        private bool _IsPaused = false;
        public bool IsPaused
        {
            get { return _IsPaused; }
            set
            {
                if (value != _IsPaused)
                {
                    _IsPaused = value;
                    Debug.Log("Pause State: " + _IsPaused);
                    PauseGame();
                }
            }
        }

        #endregion

        public enum DifficultyLevel
        {
            Easy, Normal, Hard
        }

        /// <summary>
        /// Set in-game, should apply a modifier to certain attributes (enemy health, damage, speed, etc.)
        /// </summary>
        public DifficultyLevel difficultyLevel;



        private void PauseGame()
        {
            if (_IsPaused)
            {
                PauseSet.StartPause();
                
            }
            else
            {
                PauseSet.EndPause();
            }
        }

        public void QueueFadeProfile(FadeProfileVR fadeProfile)
        {
            fadeQueue.Enqueue(fadeProfile);
        }

        public void SetCurrentFadeProfile()
        {
            if (fadeQueue.Count > 0)
            {
                currentFadeProfile = fadeQueue.Dequeue();
            }
            else
            {
                currentFadeProfile = defaultFadeProfile;
            }
        }

    }
}

