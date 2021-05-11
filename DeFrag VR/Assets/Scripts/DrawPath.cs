using Game;
using Game.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    [SerializeField]
    private Spawner spawner;

    private void OnDrawGizmos()
    {
        iTween.DrawPath(spawner.path);
    }
}
