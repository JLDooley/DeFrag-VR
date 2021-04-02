using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Game.Effects
{
    public class EffectPlayerGunTrail : EffectBase
    {
        [SerializeField]
        private LineRenderer projectileTrail;

        [SerializeField]
        private float leadInDistance = 2f;

        [SerializeField]
        private float maxDrawDistance = 30f;

        //public Vector3 end;

        [SerializeField]
        private float fadeOutDuration;

        [SerializeField]
        private int fadeOutSteps;

        void OnEnable()
        {
            projectileTrail.positionCount = 3;
        }

        public override void Raise()
        {
            CalculateTrail(transform.forward * maxDrawDistance);
        }

        public override void Raise(Vector3 vector3)
        {
            //Debug.Log("Input Vector3: " + vector3);
            CalculateTrail(vector3);
        }

        /// <summary>
        /// Draws a trail between the origin of the gameobject and an input end point.
        /// </summary>
        /// <param name="endPoint">The world space position of an end point for the line. Typical source: Raycast hit.point.</param>
        public void CalculateTrail(Vector3 endPoint)
        {
            Transform startTransform = projectileTrail.transform;
            Vector3 leadInPoint;
            Vector3 newEndPoint;

            float totalLength = Vector3.Magnitude(endPoint-startTransform.position);

            newEndPoint = startTransform.InverseTransformPoint(endPoint);

            if (totalLength < leadInDistance)
            {
                leadInPoint = newEndPoint;

                DrawTrail(leadInPoint);
            }
            else if (totalLength > maxDrawDistance)
            {
                leadInPoint = newEndPoint * (leadInDistance / totalLength);

                newEndPoint = newEndPoint * (maxDrawDistance / totalLength);

                DrawTrail(leadInPoint, newEndPoint);
            }
            else
            {
                leadInPoint = newEndPoint * (leadInDistance / totalLength);

                DrawTrail(leadInPoint, newEndPoint);
            }

            #region old
                /*
                endPoint = endPoint - transform.position;   //Makes relative to start of line

                float totalLength = Vector3.Magnitude(endPoint);

                Vector3 startPoint = projectileTrail.transform.position;
                Vector3 leadInPoint;
                Vector3 newendPoint;


                if (totalLength < leadInDistance)
                {
                    leadInPoint = Vector3.forward * totalLength;

                    DrawTrail(leadInPoint);
                }
                else if (totalLength > maxDrawDistance)
                {
                    leadInPoint = Vector3.forward * leadInDistance;

                    newendPoint = Vector3.forward * maxDrawDistance;

                    DrawTrail(leadInPoint, newendPoint);
                }
                else
                {
                    leadInPoint = Vector3.forward * leadInDistance;

                    newendPoint = Vector3.forward * totalLength;

                    DrawTrail(leadInPoint, newendPoint);
                }
                */
                #endregion
        }

        private void DrawTrail(Vector3 endPoint)
        {
            projectileTrail.positionCount = 2;
            projectileTrail.SetPosition(1, endPoint);

            StartCoroutine(FadeOutLine());
        }

        private void DrawTrail(Vector3 leadInPoint, Vector3 endPoint)
        {
            projectileTrail.SetPosition(1, leadInPoint);
            projectileTrail.SetPosition(2, endPoint);

            StartCoroutine(FadeOutLine());
        }

        private IEnumerator FadeOutLine()
        {
            int stepCount = 0;

            while (stepCount < fadeOutSteps)
            {
                ++stepCount;

                float alpha = Mathf.Lerp(0f, 1f, ((float)fadeOutSteps - (float)stepCount) / (float)fadeOutSteps);

                int totalAlphaKeys = projectileTrail.colorGradient.alphaKeys.Length;

                Gradient colourGradient = projectileTrail.colorGradient;

                GradientAlphaKey[] alphakeys = new GradientAlphaKey[totalAlphaKeys];

                for (int i = totalAlphaKeys - 1; i >= 0; --i)
                {
                    //If an alpha key alpha is already lower than new value, don't change the key
                    if (colourGradient.alphaKeys[i].alpha > alpha)
                    {
                        alphakeys[i] = new GradientAlphaKey(alpha, colourGradient.alphaKeys[i].time);
                    }
                    else
                    {
                        alphakeys[i] = new GradientAlphaKey(colourGradient.alphaKeys[i].alpha, colourGradient.alphaKeys[i].time);
                    }
                }
                colourGradient.SetKeys(colourGradient.colorKeys, alphakeys);

                projectileTrail.colorGradient = colourGradient;

                yield return new WaitForSeconds(fadeOutDuration / fadeOutSteps);
            }
            Destroy(gameObject);
        }
    }
}


