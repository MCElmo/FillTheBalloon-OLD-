using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance { get {return _instance;}}

    private int level;
    public static event Action<int> LevelWon = delegate { };
    public static event Action<int> LevelLost = delegate { };

    public static event Action<int> LevelChanged = delegate { };

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

    void Start()
    {
        PlayerData data = GameManager.Instance.getPlayerData();
        level = data.level;
        LevelChanged(level);
    }

    void Update()
    {
        
    }
    public int getLevel()
    {
        return level;
    }

}
