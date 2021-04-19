using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Game.Utility
{
    public class MenuDisplayBlur : MonoBehaviour
    {
        [SerializeField]
        private Camera menuCamera;

        [SerializeField]
        private float rampTime = 0.5f;

        [SerializeField]
        private int rampSteps = 10;

        [SerializeField]
        private AnimationCurve timeRampDown;

        [SerializeField]
        private PostProcessProfile menuProfile;
        [SerializeField]
        private float focusDistance;

        /// <summary>
        /// The focus distance to use when the blur effect is disabled.
        /// </summary>
        private float disabledFocusDistance = 10f;
        [SerializeField]
        private AnimationCurve blurRampUp;

        private float currentTimeScale = 1f;

        private void OnEnable()
        {
            //This should be called by an event in the final version
            StartCoroutine(DisplayMenu());
        }

        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public IEnumerator DisplayMenu()
        {
            int stepCount = 0;
            currentTimeScale = Time.timeScale;  //Store the time scale in case a slow motion effect was running.
            DepthOfField depthOfField = menuProfile.GetSetting<DepthOfField>();
            depthOfField.focusDistance.value = disabledFocusDistance;

            menuCamera.gameObject.SetActive(true);


            while (stepCount <= rampSteps)
            {
                //Debug.Log("Step: " + stepCount);
                float currentStep = (float) stepCount / (float) rampSteps;  //Cast is not redundant
                //Debug.Log("Current Step: " + currentStep);


                float currentTimeRamp = timeRampDown.Evaluate(currentStep);
                //Debug.Log("Current Time Ramp: " + currentTimeRamp);
                float currentBlurRamp = blurRampUp.Evaluate(currentStep);
                //Debug.Log("Current Blur Ramp: " + currentBlurRamp);

                Time.timeScale = currentTimeScale * currentTimeRamp;
                //Debug.Log("Current Time Scale: " + Time.timeScale);

                depthOfField.focusDistance.Interp(disabledFocusDistance, focusDistance, currentBlurRamp);
                //Debug.Log("Current Focus Distance: " + depthOfField.focusDistance.value);

                stepCount++;

                yield return new WaitForSecondsRealtime(rampTime / rampSteps);
            }
        }
    }
}

