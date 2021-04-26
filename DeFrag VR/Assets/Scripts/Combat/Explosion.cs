using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Combat
{
    public class Explosion : MonoBehaviour
    {
        //public GameObject explosionPrefab;
        [SerializeField]
        private WeaponProfile weaponProfile;

        [SerializeField]
        private float explosionRadius;

        [SerializeField]
        [Tooltip("Layers to be considered by the explosion.")]
        private LayerMask explosionLayer;

        [SerializeField]
        [Tooltip("Target Types to be automatically ignored by the explosion.")]
        private TargetType[] exemptions;

        private void Start()
        {
            Explode();
        }

        public void Explode()
        {
            Debug.Log("Explosion triggered.");

            Vector3 centre = transform.position;
            Collider[] hitColliders = Physics.OverlapSphere(centre, explosionRadius, explosionLayer);



            foreach (var hitCollider in hitColliders)
            {
                GameObject hitObject = hitCollider.gameObject;

                if (hitObject.GetComponent<AimAssist>() == null)    //Ignore AimAssist colliders
                {
                    Target hitTarget = hitObject.GetComponentInParent<Target>();

                    Debug.Log(hitObject.name);

                    if (hitTarget != null)
                    {
                        if (!exemptions.Contains(hitTarget.targetType))
                        {
                            hitTarget.OnHit(weaponProfile);
                            //hitTarget.Die();
                        }
                    }
                }

            }
        }
    }
}

