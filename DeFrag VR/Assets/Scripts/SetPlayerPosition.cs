using Game.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utility
{
    public class SetPlayerPosition : MonoBehaviour
    {
        
        public TransformVariable playerPosition;

        void Update()
        {
            playerPosition.SetTransformValue(transform);
        }
    }
}

