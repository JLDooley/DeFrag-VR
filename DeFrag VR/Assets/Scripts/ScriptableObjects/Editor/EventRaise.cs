using UnityEngine;
using UnityEditor;
using Game.Utility;

namespace Game.DevEditor
{
    //Create a test button in the GameEvent S/O and any derived classes, for manual triggering from Inspector
    [CustomEditor(typeof(GameEvent), editorForChildClasses: true)]
    public class EventRaise : Editor
    {
        //Extend the functionality of the base Inspector
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;    //Enable button when game is running

            GameEvent e = target as GameEvent;      //'e' is the current target of the editor cast as a GameEvent (in the event of there being multiple instances of GameEvent present)
        
            if (GUILayout.Button("Raise"))          //Create Button and check if pressed?
            {
                e.Raise();                          //Call GameEvent Raise() function
            }
        }
    }
}

