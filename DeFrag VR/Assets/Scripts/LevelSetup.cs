using Game.Data;
using Game.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    [SerializeField]
    private IntVariable stageIndex;

    [SerializeField]
    private GameEvent enableWaveTriggers;

    private void OnEnable()
    {
        stageIndex.SetValue(0);

        
    }

    private void Start()
    {
        enableWaveTriggers.Raise();
    }


}
