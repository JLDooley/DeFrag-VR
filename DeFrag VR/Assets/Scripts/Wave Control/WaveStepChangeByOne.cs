using Game.Data;
using UnityEngine;

namespace Game.Utility
{
    public class WaveStepChangeByOne : MonoBehaviour
    {
        [SerializeField]
        private IntVariable waveStepReference;

        private void Start()
        {
            waveStepReference.ChangeValue(1);
            Destroy(gameObject);
        }
    }
}

