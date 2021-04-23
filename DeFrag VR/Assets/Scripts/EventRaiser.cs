using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Utility
{
    public class EventRaiser : MonoBehaviour
    {
#if UNITY_EDITOR
        [Multiline]
        public string Description = "";
#endif
        [Tooltip("Postpone the running of the events by the specified number of seconds.")]
        [SerializeField]
        private float delay = 0f;

        [SerializeField]
        private GameEvent[] onRaiseGameEvents;
        
        [Space]

        [SerializeField]
        private UnityEvent onRaiseEvents;

        public void Raise()
        {
            StartCoroutine(RunEvents());
        }

        private IEnumerator RunEvents()
        {
            yield return new WaitForSecondsRealtime(delay);

            if (onRaiseGameEvents.Length > 0)
            {
                for (int i = onRaiseGameEvents.Length - 1; i >= 0; i--)
                {
                    if (onRaiseGameEvents[i] != null)
                    {
                        onRaiseGameEvents[i].Raise();
                    }
                }
            }

            onRaiseEvents.Invoke();
        }
    }
}

