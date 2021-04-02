using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "New Active Enemies Set", menuName = "Runtime Sets/Active Enemies")]
    public class ActiveEnemiesSet : RuntimeSet<GameObject>
    {
        public bool IsEmpty()
        {
            if (Items.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

