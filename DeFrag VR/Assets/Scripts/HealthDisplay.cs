using Game.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Game.Utility
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField]
        private FloatReference currentHealth;

        [SerializeField]
        private FloatReference maxHealth;

        [SerializeField]
        private PipData[] healthPips;

        [SerializeField]
        private Color pipColour = Color.red;

        [SerializeField]
        private float pulseDuration = 0.5f;

        [SerializeField]
        private AnimationCurve pulse;


        private float ratio;
        private float timer = 0f;

        public void UpdateHealthDisplay()
        {
            ratio = currentHealth / maxHealth;

            Debug.Log("Current Health Ratio: " + ratio);

            switch (ratio)
            {
                case float n when (n >= 1f):
                    foreach (PipData pip in healthPips)
                    {
                        DisplayHealthPip(pip.healthPip, pipColour, true);
                        pip.pulsePip = false;
                    }
                    //Debug.Log("Health 100%");
                    break;

                case float n when (n < 1f && n >= 0.8f):
                    DisplayHealthPip(healthPips[0].healthPip, pipColour, true);
                    healthPips[0].pulsePip = false;

                    DisplayHealthPip(healthPips[1].healthPip, pipColour, true);
                    healthPips[1].pulsePip = false;

                    StartCoroutine(HealthPipPulse(healthPips[2]));
                    //Debug.Log("Health 99% - 80%");
                    break;

                case float n when (n < 0.8f && n >= 0.6f):
                    DisplayHealthPip(healthPips[0].healthPip, pipColour, true);
                    healthPips[0].pulsePip = false;

                    DisplayHealthPip(healthPips[1].healthPip, pipColour, true);
                    healthPips[1].pulsePip = false;

                    DisplayHealthPip(healthPips[2].healthPip, pipColour, false);
                    healthPips[2].pulsePip = false;
                    //Debug.Log("Health 79% - 60%");
                    break;

                case float n when (n < 0.6f && n >= 0.4f):
                    DisplayHealthPip(healthPips[0].healthPip, pipColour, true);
                    healthPips[0].pulsePip = false;

                    StartCoroutine(HealthPipPulse(healthPips[1]));

                    DisplayHealthPip(healthPips[2].healthPip, pipColour, false);
                    healthPips[2].pulsePip = false;
                    //Debug.Log("Health 59% - 40%");
                    break;

                case float n when (n < 0.4f && n >= 0.2f):
                    DisplayHealthPip(healthPips[0].healthPip, pipColour, true);
                    healthPips[0].pulsePip = false;

                    DisplayHealthPip(healthPips[1].healthPip, pipColour, false);
                    healthPips[1].pulsePip = false;

                    DisplayHealthPip(healthPips[2].healthPip, pipColour, false);
                    healthPips[2].pulsePip = false;
                    //Debug.Log("Health 39% - 20%");
                    break;

                case float n when (n < 0.2f && n >= 0f):
                    StartCoroutine(HealthPipPulse(healthPips[0]));

                    DisplayHealthPip(healthPips[1].healthPip, pipColour, false);
                    healthPips[1].pulsePip = false;

                    DisplayHealthPip(healthPips[2].healthPip, pipColour, false);
                    healthPips[2].pulsePip = false;
                    //Debug.Log("Health 19% - 0%");
                    break;

                default:
                    DisplayHealthPip(healthPips[0].healthPip, pipColour, false);
                    healthPips[0].pulsePip = false;

                    DisplayHealthPip(healthPips[1].healthPip, pipColour, false);
                    healthPips[1].pulsePip = false;

                    DisplayHealthPip(healthPips[2].healthPip, pipColour, false);
                    healthPips[2].pulsePip = false;
                    //Debug.Log("Default Health Case");
                    break;
            }

        
        }

        private void DisplayHealthPip(GameObject pip, Color colour, bool setActive)
        {
            TextMeshPro tmpRef = pip.GetComponent<TextMeshPro>();
        
            if (colour != tmpRef.color)
            {
                tmpRef.color = colour;
            }

            pip.SetActive(setActive);
        }

        private IEnumerator HealthPipPulse(PipData pip)
        {
            Debug.Log("Running HealthPipPulse()");
            if (!pip.pulsePip)
            {
                pip.pulsePip = true;
                
                timer = 0f;
                
                Color newColour = pip.healthPip.GetComponent<TextMeshPro>().color;

                while (pip.pulsePip)
                {
                    //if ((timer / pulseDuration) >= 1f)
                    //{
                    //    timer = 0f;
                    //}
                    newColour.a = pulse.Evaluate(timer / pulseDuration);

                    DisplayHealthPip(pip.healthPip, newColour, true);

                    yield return null;
                }
            }
            


        }

        private void Update()
        {
            //Only expect one pulse at any time, so one timer instance will do.
            timer += Time.deltaTime;
        }
    }

    [Serializable]
    public class PipData
    {
        [SerializeField]
        public GameObject healthPip;
        public bool pulsePip = false;
    }
}
