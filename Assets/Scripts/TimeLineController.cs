using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineController : MonoBehaviour
{
    [SerializeField] private PlayableAsset[] timeLines;
    [SerializeField] private PlayableDirector director;

    private void Awake()
    {
        GameManager.StateChanged += StateChanged;
    }

    private void StateChanged()
    {
        if(GameManager.Instance.GameState == GameState.MainMenu)
        {
            director.Play(timeLines[0]);
        }
        else if(GameManager.Instance.GameState == GameState.LevelRunning)
        {
            director.Play(timeLines[1]);
        }
    }
}
