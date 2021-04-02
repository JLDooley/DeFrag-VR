using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Effects
{
    /// <summary>
    /// Polymorphism support for effects, relevant scripts should inherit from this class
    /// </summary>
    public abstract class EffectBase : MonoBehaviour
    {
        /// <summary>
        /// Triggers the effect methods when called remotely.
        /// </summary>
        public virtual void Raise()
        {

        }

        /// <summary>
        /// Triggers the effect methods when called remotely.
        /// </summary>
        /// <param name="vector3">For use in derived methods (e.g. end point in a line vfx)</param>
        public virtual void Raise(Vector3 vector3)
        {

        }
    }
}

