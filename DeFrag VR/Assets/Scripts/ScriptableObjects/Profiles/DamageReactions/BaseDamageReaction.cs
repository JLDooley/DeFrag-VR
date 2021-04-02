using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    /// <summary>
    /// Empty class for Polymorphism. Hold the empty virtual methods to be overidden by derived classes.
    /// </summary>
    public abstract class BaseDamageReaction: ScriptableObject
    {
        public virtual void Raise(Target target)
        {

        }
        public virtual void Raise(Target target, WeaponProfile weaponProfile)
        {

        }
    }
}

