using Game.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace Game.Effects
{
    public class PlayerHurtEffect : EffectBase
    {
        [SerializeField]
        private Color hitColour = Color.clear;

        [SerializeField]
        private float fadeOutTime = 0.1f;

        [SerializeField]
        private float fadeInTime = 0.1f;


        public override void Raise()
        {
            StartCoroutine(PlayerHurt());
        }

        private IEnumerator PlayerHurt()
        {
            SteamVR_Fade.Start(hitColour, fadeOutTime);
            yield return new WaitForSeconds(fadeOutTime);
            SteamVR_Fade.Start(Color.clear, fadeInTime);
        }
    }
}

