using Game.Utility;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "New Wave Trigger Set", menuName = "Runtime Sets/Wave Trigger")]
    public class WaveTriggerRuntimeSet : RuntimeSet<WaveTrigger>
    {
        public void Purge()
        {
            for (int i = Items.Count-1; i >= 0; i--)
            {
                //If the active WaveTrigger instance was added to the PauseSet, unsubscribe it now.
                if (Items[i].gameObject.TryGetComponent<Suspendable>(out Suspendable suspendable))
                {
                    suspendable.UnsubscribeToSet();
                }

                Items[i].gameObject.SetActive(false);
            }
        }
    }
}