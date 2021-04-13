using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Game.Utility
{
    public class MenuDisplay : MonoBehaviour
    {
        [SerializeField]
        private Camera menuCamera;

        [SerializeField]
        private float rampTime;

        [SerializeField]
        private AnimationCurve timeRampDown;

        [SerializeField]
        private PostProcessProfile menuProfile;
        [SerializeField]
        private AnimationCurve blurRampUp;


        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public IEnumerator DisplayMenu()
        {

        }
    }
}

