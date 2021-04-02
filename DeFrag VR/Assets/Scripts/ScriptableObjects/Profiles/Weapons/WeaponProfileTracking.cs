using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Combat
{
    /// <summary>
    /// Extension for weapons which should track their target independent of their 'user' (e.g. turrets).
    /// </summary>
    [CreateAssetMenu(fileName = "New Tracking Weapon", menuName = "Profiles/Weapon/Tracking Weapon", order = 1)]
    public class WeaponProfileTracking : WeaponProfile
    {

        [Tooltip("Max acceptable angle to target.")]
        public float aimingTolerance;

        [Tooltip("Turn speed of weapon.")]
        [Range(0f, 90f)]
        public float rotationSpeed;

        [Tooltip("Allowable turning range independent of holder's orientation.")]
        [Range(0f, 90f)]
        public float pivotRange;
    }
}

