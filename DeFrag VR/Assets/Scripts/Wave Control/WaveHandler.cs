using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;

namespace Game
{
    /// <summary>
    /// Manages all WaveTriggers in a level. Enables any WaveTriggers for a given stage.
    /// </summary>
    public class WaveHandler : MonoBehaviour
    {
        public WaveTrigger[] triggerArray;

        public IntReference StageIndex;

        /// <summary>
        /// Typically called by an EnableWaveTriggers Game Event.
        /// </summary>
        public void EnableTriggers()
        {
            for (int i = triggerArray.Length - 1; i >= 0; i--)
            {
                if(triggerArray[i].RequiredIndex == StageIndex)
                {
                    triggerArray[i].gameObject.SetActive(true);
                }
            
            }
        }
    }
}

