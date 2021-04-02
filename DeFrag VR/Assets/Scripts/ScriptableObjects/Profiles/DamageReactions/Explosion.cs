using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Combat
{
    /// <summary>
    /// Create an explosion, damaging non-player characters.
    /// </summary>
    [CreateAssetMenu(fileName = "Explosion", menuName = "Profiles/Damage/Explosion", order = 5)]
    public class Explosion : BaseDamageReaction
    {
    
        public GameObject explosionPrefab;
    
        public float explosionRadius;

        public LayerMask explosionLayer;

        public TargetType[] exemptions;

        public override void Raise(Target target, WeaponProfile weaponProfile)
        {
            Debug.Log("Explosion triggered.");

            Vector3 centre = target.transform.position;
            Collider[] hitColliders = Physics.OverlapSphere(centre, explosionRadius, explosionLayer);



            foreach (var hitCollider in hitColliders)
            {
                GameObject hitObject = hitCollider.gameObject;
                Target hitTarget = hitObject.GetComponent<Target>();

                if (hitTarget != null)
                {
                    if (!exemptions.Contains(hitTarget.targetType))
                    //if (!hitObject.CompareTag("Player"))    //TargetType != Player?
                    {
                        //Debug.Log(hitObject.name);
                        hitTarget.Die();
                    }
                }
            }
        }
    }
}
