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
                Items[i].gameObject.SetActive(false);
            }
        }
    }
}