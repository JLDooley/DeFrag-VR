using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;
using Game.Utility;

namespace Game
{
    /// <summary>
    /// Controls the animation and interaction detection of the 'orb' object.
    /// </summary>
    [RequireComponent(typeof(Interactable))]
    public class OrbManager : MonoBehaviour
    {

        [Tooltip("For triggering events on this GameObject.")]
        public UnityEvent onTriggeredInstanced;
    
        [Tooltip("For triggering events elsewhere in the game.")]
        public GameEvent[] onTriggeredGlobal;

        public Animator orbAnimator;
        public float animationRampTime = 0.5f;
        private float timer;

        protected Interactable interactable;

        private void Awake()
        {
            interactable = GetComponent<Interactable>();
        }

        private void Update()
        {
            AnimateHandHover();

            timer += Time.deltaTime;
        }



        protected virtual void OnHandHoverBegin(Hand hand)
        {
            orbAnimator.SetBool("IsTouched", true);

            timer = 0f;
        }


        protected virtual void OnHandHoverEnd(Hand hand)
        {
            orbAnimator.SetBool("IsTouched", false);

            timer = 0f;
        }


        protected virtual void HandHoverUpdate(Hand hand)
        {
            GrabTypes startingGrabType = hand.GetGrabStarting();

            if (startingGrabType == GrabTypes.Grip && onTriggeredGlobal.Length > 0)
            {
                onTriggeredInstanced.Invoke();

                //Fade here
                for (int i = onTriggeredGlobal.Length - 1; i >= 0; i--)
                {
                    onTriggeredGlobal[i].Raise();
                }
            }
        }



        public void AnimateHandHover()
        {
            bool touchCheck = orbAnimator.GetBool("IsTouched");
            float currentAnimationSpeed = orbAnimator.GetFloat("SpinSpeedMultiplier");

            if (touchCheck && currentAnimationSpeed < 3f)
            {
                currentAnimationSpeed = RampOverTime(currentAnimationSpeed, 3f, animationRampTime, timer);
            }
            else if (!touchCheck && currentAnimationSpeed > 1f)
            {
                currentAnimationSpeed = RampOverTime(currentAnimationSpeed, 1f, animationRampTime, timer);
            }
            orbAnimator.SetFloat("SpinSpeedMultiplier", currentAnimationSpeed);
        }
    

        public float RampOverTime(float current, float target, float duration, float currenttime)
        {
            current = Mathf.Lerp(current, target, currenttime / duration);

            if (CalculationFunctions.FastApproximately(current, target, 0.01f))
            {
                current = target;
            }
            return current;
        }
    }

}
