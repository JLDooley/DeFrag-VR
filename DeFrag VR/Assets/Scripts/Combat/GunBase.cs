using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace Game.Combat
{
    /// <summary>
    /// Base code for grabbable guns, use InteractableGun (with HandListener) if weapons are hard-bound to controllers.
    /// </summary>
    public class GunBase : MonoBehaviour
    {
        [SerializeField]
        protected Interactable interactable;

        [SerializeField]
        protected SteamVR_Action_Boolean inputAction;

        [SerializeField]
        protected SteamVR_Input_Sources source;

        [SerializeField]
        protected WeaponProfile weaponProfile;


        private void OnEnable()
        {
            if (interactable == null)
            {
                interactable = GetComponent<Interactable>();
            }

            if (interactable == null)
            {
                Debug.LogError(gameObject.name + ": No interactable detected.");
            }
        }

        private void Update()
        {
            if (interactable.attachedToHand)
            {
                source = interactable.attachedToHand.handType;

                if (inputAction[source].stateDown)
                {
                    //Debug.Log("Shooting");
                    Shoot();
                }
            }
            else if (!interactable.attachedToHand)
            {
                //Debug.Log(gameObject.name + ": Detached from hand, returning to holster.");

                //transform.SetPositionAndRotation(holster.position, holster.rotation);
            }
        }

        public virtual void Shoot()
        {

        }
    }
}

