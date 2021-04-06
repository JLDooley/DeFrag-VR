using UnityEngine;
using Game.Data;


namespace Game.Utility
{
    /// <summary>
    /// Starts a timer when the number of remaining enemies drops below a threshold. Increments step when timer expires.
    /// </summary>
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

            //Start timer when below threshold.
            if (activeEnemiesSet.Items.Count <= enemiesLeft)
            {
                updateStepDelay += Time.deltaTime;
            }
            else
            {
                //Reset timer when above threshold.
                updateStepDelay = 0f;
            }

        }
    }
}

