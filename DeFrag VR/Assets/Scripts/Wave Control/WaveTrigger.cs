using UnityEngine;
using Game.Data;
using Game.Utility;

namespace Game
{
    /// <summary>
    /// Attached to triggers in a scene used to start a wave. Can support multiple triggers and will disable all once one is selected.
    /// </summary>
    public class WaveTrigger : MonoBehaviour
    {
        public WaveTriggerRuntimeSet RuntimeSet;

        public GameEvent TriggerWaveEvent;


        public IntVariable stageIndex;


        #region Required Index

        [SerializeField]
        [Tooltip("What stage should this trigger be used on.")]
        [Min(0)]
        private int _RequiredIndex = 0;
        /// <summary>
        /// What stage should this trigger be used on.
        /// </summary>
        public int RequiredIndex
        {
            get { return _RequiredIndex; }
        }
        #endregion
        #region Wave Instance
        [SerializeField]
        [Tooltip("The Stage Index (and wave) this trigger transitions to. Should be greater than Required Index.")]
        [Min(1)]
        private int _WaveInstance = 1;
        /// <summary>
        /// The Stage Index (and wave) this trigger transitions to. Should be greater than Required Index.
        /// </summary>
        public int WaveInstance
        {
            get { return _WaveInstance; }
        }
        #endregion

        private void OnEnable()
        {
            RuntimeSet.Add(this);
        }

        private void OnDisable()
        {
            RuntimeSet.Remove(this);
        }

        /// <summary>
        /// Triggered remotely by player interaction with the WaveTrigger gameobject. Raises a Game Event.
        /// </summary>
        public void OnWaveTriggered()
        {
            Debug.Log("Wave Triggered");
            //Set Stage Index (the next Stage Index)
            stageIndex.SetValue(WaveInstance);

            //Event: Trigger Wave - Triggers WaveManager
            TriggerWaveEvent.Raise();

            //Purge Active Trigger Set (disables this and unselected WaveTriggers where relevant)
            RuntimeSet.Purge();

        }
    }
}
