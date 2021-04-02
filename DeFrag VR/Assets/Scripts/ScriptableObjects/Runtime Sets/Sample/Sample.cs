using UnityEngine;
using Game.Data;

namespace Game
{
    public class Sample : MonoBehaviour
    {
        public SampleRuntimeSet RuntimeSet;

        private void OnEnable()
        {
            RuntimeSet.Add(this);
        }

        private void OnDisable()
        {
            RuntimeSet.Remove(this);
        }
    }
}