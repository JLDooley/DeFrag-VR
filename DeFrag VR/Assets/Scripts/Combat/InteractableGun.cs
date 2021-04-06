using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Utility.Interaction;
using Game.Effects;

namespace Game.Combat
{
    public class InteractableGun : InteractableBase
    {
        [SerializeField]
        private WeaponProfile weaponProfile;

        [SerializeField]
        private Transform muzzle;

        [SerializeField]
        private float range;

        [SerializeField]
        private GameObject onShootEffectPrefab;

        protected override void OnTriggerDown()
        {
            base.OnTriggerDown();

            //Shoot gun
            Shoot();
        }

        private void Shoot()
        {
            if (muzzle != null)
            {
                Vector3 impactPoint;
                Target target;
                
                if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit, range))
                {
                    //Debug.Log("Collider: " + hit.collider);
                    AimAssist aimAssistCollider = hit.collider.GetComponent<AimAssist>();
                    //Debug.Log(aimAssistCollider);

                    if (aimAssistCollider != null)  //Hit the aim assist hitbox, fire a new ray at main collider
                    {
                        //Debug.Log("Aim Assist Hit");
                        //Debug.Break();

                        Vector3 hitTargetCentre = aimAssistCollider.newTarget.position;
                        Vector3 hitTargetImpactPoint = hit.point;

                        Vector3 secondaryOrigin = hitTargetImpactPoint + ((hitTargetCentre - hitTargetImpactPoint) * 0.1f);

                        if (Physics.Raycast(secondaryOrigin, hitTargetCentre - hitTargetImpactPoint, out RaycastHit hitSecondary, Vector3.Magnitude(hitTargetCentre - hitTargetImpactPoint)))
                        {
                            impactPoint = hitSecondary.point;

                            //DebugHitPoint(impactPoint);

                            target = hitSecondary.transform.GetComponentInParent<Target>();
                            if (target != null)
                            {
                                target.OnHit(weaponProfile);
                            }

                            SpawnTrail(impactPoint);
                        }
                    }
                    else
                    {
                        

                        //Debug.Log("Main Target Hit");
                        //Debug.Break();

                        impactPoint = hit.point;

                        //DebugHitPoint(impactPoint);

                        target = hit.collider.GetComponentInParent<Target>();
                        if (target != null)
                        {
                            //Debug.Log(target);
                            target.OnHit(weaponProfile);
                        }

                        SpawnTrail(impactPoint);
                    } 
                }
                else
                {
                    SpawnTrail();
                }

            }
        }

        private void SpawnTrail()
        {
            EffectPlayerGunTrail onShootEffect = Instantiate(onShootEffectPrefab, muzzle.position, muzzle.rotation).GetComponentInChildren<EffectPlayerGunTrail>();

            if (onShootEffect != null)
            {
                onShootEffect.Raise();
            }
        }

        private void SpawnTrail(Vector3 endPoint)
        {
            EffectPlayerGunTrail onShootEffect = Instantiate(onShootEffectPrefab, muzzle.position, muzzle.rotation).GetComponentInChildren<EffectPlayerGunTrail>();

            if (onShootEffect != null)
            {
                //onShootEffect.CalculateTrail(endPoint);

                onShootEffect.Raise(endPoint);
            }
        }

        private void DebugHitPoint(Vector3 position, string name = "")
        {
            Instantiate(new GameObject(), position, Quaternion.identity).gameObject.name = name;
        }

        private void DebugHitPoint(Transform transform, string name = "")
        {
            Instantiate(new GameObject(), transform.position, transform.rotation).gameObject.name = name;
        }
    }

}
