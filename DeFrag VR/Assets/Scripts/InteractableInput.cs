using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utility.Interaction
{
    public class InteractableInput : InteractableBase
    {
        [SerializeField]
        GameEvent onInputTriggered;
        protected override void OnTriggerDown()
        {
            onInputTriggered.Raise();
        }
    }
}

