using Game.Data;
using Game.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "New Pause Set", menuName = "Runtime Sets/Pause")]
    public class PauseSet : RuntimeSet<Suspendable>
    {
        public void StartPause()
        {
            for (int i = Items.Count - 1; i >= 0; i--)
            {
                Items[i].gameObject.SetActive(false);
            }
        }
        
        public void EndPause()
        {
            for (int i = Items.Count - 1; i >= 0; i--)
            {
                Items[i].gameObject.SetActive(true);
            }
        }
    }
}

