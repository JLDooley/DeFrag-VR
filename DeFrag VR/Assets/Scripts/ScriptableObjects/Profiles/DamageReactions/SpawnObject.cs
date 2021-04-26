using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    [CreateAssetMenu(fileName = "SpawnObject", menuName = "Profiles/Damage/Spawn GameObject", order = 3)]
    public class SpawnObject : BaseDamageReaction
    {
        public GameObject prefab;

        public override void Raise(Target target)
        {
            Instantiate(prefab, target.transform.position, target.transform.rotation);
        }

        public override void Raise(Target target, WeaponProfile weaponProfile)
        {
            Instantiate(prefab, target.transform.position, target.transform.rotation);
        }
    }
}

