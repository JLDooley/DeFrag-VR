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

        [SerializeField] private bool playOnStart = false;

        private void Start()
        {
            if (playOnStart)
            {
                PlayOneShotSoundEffect();
            }
        }

        public void PlayOneShotSoundEffect()
        {
            FMODUnity.RuntimeManager.PlayOneShot(soundEffect, transform.position);
        }
    }
}

