using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Levels", menuName = "Level/GameLevels")]
public class GameLevels : ScriptableObject
{
    public List<Level> levels;

    public Level getLevelData(int level)
    {
        // while(level > levels.Count)
        // {
        //     Level lastLevel = getLastLevel();
        //     Level levelNew = new Level();
        //     levelNew.levelNumber = lastLevel.levelNumber + 1;
        //     levelNew.launchInfos = lastLevel.launchInfos;
        //     levelNew.levelScoreTarget = lastLevel.levelScoreTarget + 3;
        //     levelNew.ringHeight = lastLevel.ringHeight;
        //     levels.Add(levelNew);
        // }
        return levels.Where(t => t.levelNumber == level).FirstOrDefault();
    }

    public Level getLastLevel()
    {
        return levels.Last();
    }
}
