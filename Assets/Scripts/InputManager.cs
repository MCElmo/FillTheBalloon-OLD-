using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance { get { return _instance; } }

    public static event Action ScreenDown = delegate { };
    public static event Action ScreenUp = delegate { };

    public ScreenState screenState = ScreenState.ScreenUp;
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
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GameManager.Instance.GameState == GameState.MainMenu)
            {
                GameManager.Instance.touchedToStart();
            }
            else if (GameManager.Instance.GameState == GameState.LevelRunning)
            {
                ScreenDown();
                screenState = ScreenState.ScreenDown;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (GameManager.Instance.GameState == GameState.LevelRunning)
            {
                ScreenUp();
                screenState = ScreenState.ScreenUp;
            }
        }


        if (GameManager.Instance.GameState == GameState.LevelRunning)
        {
            //Mobile Code
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch t = Input.GetTouch(i);
                    if (t.phase == TouchPhase.Began)
                    {
                        ScreenDown();
                        screenState = ScreenState.ScreenDown;
                    }
                    else if (t.phase == TouchPhase.Ended)
                    {
                        ScreenUp();
                        screenState = ScreenState.ScreenUp;
                    }
                }
            }
        }
    }

}
