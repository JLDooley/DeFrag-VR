using UnityEngine;
using UnityEngine.Events;

namespace Game.Utility
{
    //  Assigned to a GameEvent by the developer.
    //  While active, is registered with target GameEvent and calls assigned methods when requested by GameEvent
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;
        public UnityEvent response;

        public void OnEventRaised()
        {
            response.Invoke();
        }

        private void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }
    }
}

