using System.Collections.Generic;
using UnityEngine;

namespace Game.Utility
{
    //  Keeps a list of GameEventListeners and triggers them when called upon to do so by other scripts.
    [CreateAssetMenu(fileName = "New Game Event", menuName = "Events/Game Event", order = 0)]
    public class GameEvent : ScriptableObject
    {
    #if UNITY_EDITOR
        [Multiline]
        public string Description = "";
    #endif

        private List<GameEventListener> listeners = new List<GameEventListener>();

        public void Raise()
        {
            for (int i = listeners.Count -1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (listeners.Contains(listener))
            {
                listeners.Remove(listener);
            }
        }

    }
}

