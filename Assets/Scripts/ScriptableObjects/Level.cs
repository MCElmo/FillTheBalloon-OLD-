using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "Level/Level")]
public class Level : ScriptableObject
{
    public int levelNumber;
    public int levelScoreTarget;
    public LaunchInfo[] launchInfos; 

    public float ringHeight;
}