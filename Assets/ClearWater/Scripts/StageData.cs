using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StageData
{
    public int stageNumber;
    public int stars;
    public bool isLocked;
    public bool hasAttempted;

    public StageData()
    {
        stageNumber = 0;
        stars = 0;
        isLocked = true;
        hasAttempted = false;
    }
}
