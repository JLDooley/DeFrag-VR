using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Game.Combat
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Profiles/Weapon/Weapon", order = 0)]
    public class WeaponProfile : ScriptableObject
    {
        public DamageType damageType;
        public int damageAmount;

        [Tooltip("Rate of Fire (Shots per Second)")]
        [Range(0.1f, 10f)]  //Between 1 shot per 10 seconds and 10 shots per second
        public float rateOfFire;

    }
}