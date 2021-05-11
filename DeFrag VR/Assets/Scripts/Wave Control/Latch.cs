using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    /// <summary>
    /// Upon receiving an input, prevents further inputs from triggering events until reset by an event.
    /// </summary>
    public class Latch : MonoBehaviour
    {
        public UnityEvent onTriggered;

        public bool inUse = false;

        [Tooltip("Latches to update in tandem with this one.")]
        public Latch[] otherLatches;

        public void CallEvent()
        {
            if (!inUse)
            {
                inUse = true;

                onTriggered.Invoke();

                UpdateLatches(true);
            }
        }

        public void UnSet()
        {
            inUse = false;

            UpdateLatches(false);
        }

        private void UpdateLatches(bool state = false)
        {
            foreach (Latch latch in otherLatches)
            {
                latch.inUse = state;
            }
        }
    }
}

