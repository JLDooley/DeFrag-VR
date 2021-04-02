using UnityEngine;
using Game.Data;


namespace Game.Utility
{
    public class WaveStepChangeNoEnemies : MonoBehaviour
    {
        [SerializeField]
        private IntVariable waveStepReference;

        [SerializeField]
        private ActiveEnemiesSet activeEnemiesSet;

        [SerializeField]
        private int enemiesLeft = 0;

        private float updateStepDelay = 0f;

        private void Update()
        {
            //Delay to give new spawns a chance.
            if (updateStepDelay >= 1f)
            {
                waveStepReference.ChangeValue(1);
                Destroy(gameObject);
            }

            if (activeEnemiesSet.Items.Count <= enemiesLeft)
            {
                updateStepDelay += Time.deltaTime;
            }
            else
            {
                updateStepDelay = 0f;
            }

        }
    }
}

