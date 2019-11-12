using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonScript : MonoBehaviour
{
    [SerializeField] private float maxScale = 5f;
    [SerializeField] private float scaleTime = 5f;
    [SerializeField] private float minScale = 2;

    [SerializeField] private float stayScaledTime = 5f;

    [SerializeField] private AnimationCurve balloonUpCurve;
    [SerializeField] private AnimationCurve balloonDownCurve;

    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float yScaleOffset = -.32f;

    [SerializeField] GameObject balloonObject;
    private Coroutine scale;
    private bool active = true;
    // Start is called before the first frame update

    private Level level;

    void Awake()
    {
        GameManager.StateChanged += GameStateChanged;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GameStateChanged()
    {
        if (GameManager.Instance.GameState == GameState.MainMenu)
        {
            loadLevelData();
            scale = StartCoroutine(bounceMenu());
        }
        else if (GameManager.Instance.GameState == GameState.LevelRunning)
        {
            print("Got here");
            loadLevelData();
            StopCoroutine(scale);
            scale = StartCoroutine(levelScale());
        }
        else
        {
            StopCoroutine(scale);
        }
    }

    private void loadLevelData()
    {
        level = LevelManager.Instance.getCurrentLevel();
        gameObject.transform.localPosition = level.startPosition;
        gameObject.transform.localScale = level.startScale;
        balloonObject.GetComponent<Renderer>().material.color = level.balloonColor;
        balloonObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", level.balloonColor);
    }

    private IEnumerator levelScale()
    {
        float scale = 0;
        while (true)
        {
            if (InputManager.Instance.screenState == ScreenState.ScreenDown)
            {  
                print("scren down");
                scale = level.scaleRate * Time.deltaTime;
            }
            else if (InputManager.Instance.screenState == ScreenState.ScreenUp)
            {
                //Print out the progress
                float currentScale;
                float minScale;
                float maxScale;
                float percentage;
                /*
                    1.)Get Current scale
                    2.) get Minimum Scale
                    3.) Get Max scale
                    4.) Calculate total scale increase necessary
                    5.) Subtract min scale from current scale
                    6.) Print (5) / (4)
                */
                currentScale = gameObject.transform.localScale.x;
                minScale = level.startScale.x;
                maxScale = level.targetScale;
                percentage = ((currentScale - minScale) / (maxScale - minScale)) * 100;
                print(percentage + "%");

                //Reset the position
                gameObject.transform.localScale = level.startScale;

                //If progress is good, call level won
                if(percentage > 99.5)
                {
                    LevelManager.Instance.wonLevel();
                }

/*
                print("screen up");
                //Check if at bottom
                if(gameObject.transform.localScale.x <= level.startScale.x)
                {
                    gameObject.transform.localScale = level.startScale;
                    scale = 0;
                }else{
                    scale = -level.deScaleRate * Time.deltaTime;
                }*/
            }
            gameObject.transform.localScale += new Vector3(scale,scale,scale);
            if(gameObject.transform.localScale.x >= level.targetScale * level.targetThreshold)
            {
                LevelManager.Instance.lostLevel();
                print("You fucked up");
            }
            yield return null;
        }
    }
    private IEnumerator bounceMenu()
    {
        float time = 0, scale;
        while (true)
        {
            // time = 0;
            //Scale up to sizetime = 0;
            while (time < 1)
            {
                //  print("Delta Time: " + Time.deltaTime);
                // print("Scale: " + gameObject.transform.localScale);
                time += Time.deltaTime / scaleTime;
                scale = minScale + (maxScale - minScale) * balloonUpCurve.Evaluate(time);
                gameObject.transform.localScale = new Vector3(scale, scale, scale);
                yield return null;
            }
            // wait
            yield return new WaitForSeconds(stayScaledTime);
            //Scale down to size 
            // time = 1;
            while (time > 0)
            {
                time -= Time.deltaTime / scaleTime;
                scale = minScale + (maxScale - minScale) * balloonDownCurve.Evaluate(time);
                gameObject.transform.localScale = new Vector3(scale, scale, scale);
                yield return null;
            }
            yield return new WaitForSeconds(stayScaledTime);
        }
    }
}
