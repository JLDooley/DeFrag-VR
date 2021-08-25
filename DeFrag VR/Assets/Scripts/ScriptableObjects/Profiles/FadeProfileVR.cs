using System.Collections;
using UnityEngine;
using Valve.VR;
using Game.Data;

namespace Game.Utility
{
    //Requires SteamVR plugin
    [CreateAssetMenu(fileName = "New Fade Profile", menuName = "Profiles/VFX/Fade(VR)", order = 1)]

    public class FadeProfileVR : ScriptableObject
    {
    #if UNITY_EDITOR
        [Multiline]
        public string Description = "";
    #endif

        public Color fadeInColour = Color.clear;
        public Color fadeOutColour = Color.black;
        public FloatReference fadeTime;

        public GameObject fadeInEffect;

        public void FadeIn()
        {
            SteamVR_Fade.Start(fadeInColour, fadeTime);
        }

        public void FadeOut()
        {
            SteamVR_Fade.Start(fadeOutColour, fadeTime);
        }
    }
}