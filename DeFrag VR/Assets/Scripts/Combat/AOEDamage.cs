using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Game.Effects;

namespace Game.Combat
{
    public class AOEDamage : MonoBehaviour
    {
        public Target spawningObject { get; set; }

        [SerializeField]
        private bool ignoreSpawningObject = true;

        [SerializeField]
        private WeaponProfile weaponProfile;

        [SerializeField]
        private float AOERadius;

        [SerializeField]
        [Tooltip("Layers to be considered by the explosion.")]
        private LayerMask AOELayers;

        [SerializeField]
        [Tooltip("Target Types to be automatically ignored by the explosion.")]
        private TargetType[] exemptions;

        [SerializeField]
        private GameObject arcEffect;

        public void Arc()
        {
            Debug.Log("Explosion triggered.");

            Vector3 centre = transform.position;
            Collider[] hitColliders = Physics.OverlapSphere(centre, AOERadius, AOELayers);



            foreach (var hitCollider in hitColliders)
            {
                GameObject hitObject = hitCollider.gameObject;

                if (hitObject.GetComponent<AimAssist>() == null)    //Ignore AimAssist colliders
                {
                    Target hitTarget = hitObject.GetComponentInParent<Target>();


                    if (hitTarget != null)
                    {
                        Debug.Log(hitObject.name);

                        if (hitTarget != spawningObject || !ignoreSpawningObject)
                        {
                            if (!exemptions.Contains(hitTarget.targetType))
                            {
                                hitTarget.OnHit(weaponProfile);

                                if (hitTarget != spawningObject)
                                {
                                    DrawArc(hitObject.transform);
                                }
                                
                            }
                        }
                    }



                }
            }

            Destroy(gameObject, 5f);
        }

        private void DrawArc(Transform target)
        {
            //Get vector from start to end
            //If magnitude is above a certain amount:
            //Get a start point
            //Get an end point
            //Draw a line from start to end

            /*
            Vector3 direction = target.position - transform.position;
            float distance = direction.magnitude;

            if (distance >= 1f)
            {
                Vector3 startPoint = direction.normalized * 0.25f;
                Vector3 endPoint = direction.normalized * (distance - 0.25f);
            }
            */

            GameObject arc = Instantiate(arcEffect, transform.position, Quaternion.identity);
            arc.GetComponent<EffectBase>().Raise(target.position);
        }
    }
}

