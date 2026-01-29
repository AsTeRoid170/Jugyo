using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StageTutorialData
{
    public int  BatteryStock;
    public float MaxEnergy;
}

[CreateAssetMenu(menuName = "ScriptableObject/StageTutorialSetting", fileName = "StageTutorialSetting")]
public class StageTutorialSetting : ScriptableObject
{
    public List<StageTutorialData> DataList;
}
