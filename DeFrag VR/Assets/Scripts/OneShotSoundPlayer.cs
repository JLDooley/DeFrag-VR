using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Effects
{
    public class OneShotSoundPlayer : MonoBehaviour
    {
        [SerializeField]
        [FMODUnity.EventRef]
        private string soundEffect;

        public void PlayOneShotSoundEffect()
        {
            FMODUnity.RuntimeManager.PlayOneShot(soundEffect, transform.position);
        }
    }
}

