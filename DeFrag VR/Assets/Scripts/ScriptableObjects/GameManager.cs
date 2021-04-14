using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;


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

        #region Active Enemies Set
        [SerializeField]
        private ActiveEnemiesSet _ActiveEnemiesSet;
        public ActiveEnemiesSet ActiveEnemiesSet
        {
            get { return _ActiveEnemiesSet; }
        }
        #endregion

        public TransformVariable playerTransform;

        public FadeProfileVR defaultFadeProfile;

        public FadeProfileVR currentFadeProfile;

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
        #endregion

        public enum DifficultyLevel
        {
            Easy, Normal, Hard
        }

        /// <summary>
        /// Set in-game, should apply a modifier to certain attributes (enemy health, damage, speed, etc.)
        /// </summary>
        public DifficultyLevel difficultyLevel;

        #region External Stats
        //[SerializeField]
        //private FloatReference _MaxHealth;
        public FloatVariable MaxHealth;


        #endregion





    }
}

