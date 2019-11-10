using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private GameState _gameState = GameState.MainMenu;
    public static GameManager Instance {get {return _instance;}}
    public GameState GameState {get{return _gameState;}}
    
    [SerializeField] private int testLevel;
    public static event Action StateChanged = delegate { };
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        LevelManager.LevelLost += LevelLost;
        LevelManager.LevelWon += LevelWon;
        StateChanged();
    }

    public void PrepareGame()
    {
        _gameState = GameState.LevelRunning;
        StateChanged();
    }
    
    public void LevelWon(int level)
    {
        _gameState = GameState.LevelWon;
        if(level == getPlayerData().level)
        {
            SaveSystem.SavePlayerLevel(level + 1);
        }
        StateChanged();
    }

    public void LevelLost(int level)
    {
        _gameState = GameState.LevelLost;
        StateChanged();
    }

    public PlayerData getPlayerData()
    {
        return new PlayerData(testLevel);
        //return SaveSystem.LoadLevel();
    }

    public void TryAgainPressed()
    {
        PrepareGame();
    }

    public void touchedToStart()
    {
        PrepareGame();
    }
}


