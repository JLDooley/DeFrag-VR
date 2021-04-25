using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;
using Game.Utility;

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

                    //Add the active WaveTrigger instance to the PauseSet, so it can be disabled when pausing.
                    if (triggerArray[i].gameObject.TryGetComponent<Suspendable>(out Suspendable suspendable))
                    {
                        suspendable.SubscribeToSet();
                    }
                }
            
            }
        }
    }
}

