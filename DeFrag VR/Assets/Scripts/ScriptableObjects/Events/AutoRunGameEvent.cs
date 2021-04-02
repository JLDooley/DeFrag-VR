using System.Collections;
using UnityEngine;

namespace Game.Utility
{
    //Triggers an event when the game object is enabled (e.g. Level begins, object spawns, etc)
    public class AutoRunGameEvent : MonoBehaviour
    {
        public GameEvent[] gameEvent;

        public float delay;

        private void OnEnable()
        {
            if (gameEvent.Length > 0)
            {
                StartCoroutine(RaiseGameEvent());
            }
        }

        IEnumerator RaiseGameEvent()
        {
            yield return new WaitForSeconds(delay);

            for (int i = gameEvent.Length - 1; i >= 0; i--)
            {
                gameEvent[i].Raise();
            }
        
        }
    }
}

