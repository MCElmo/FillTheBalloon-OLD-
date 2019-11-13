using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonScript : MonoBehaviour
{
    [SerializeField] private float maxScale = 5f;
    [SerializeField] private float scaleTime = 5f;
    [SerializeField] private float gameDescaleSpeed = 5f;

    [SerializeField] private float minScale = 2;

    [Tooltip("Descale time while in Game")]
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
        balloonObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", level.balloonColor * level.intensity);
        LevelManager.Instance.updatePercent(0);
    }

    private IEnumerator levelScale()
    {
        float scale = 0;
        while (true)
        {
            print(InputManager.Instance.screenState);
            if (InputManager.Instance.screenState == ScreenState.ScreenDown)
            {
                scale = level.scaleRate * Time.deltaTime;
                gameObject.transform.localScale += new Vector3(scale, scale, scale);
            }
            else if (InputManager.Instance.screenState == ScreenState.ScreenUp)
            {
                //Print out the progress
                float currentScale, minScale, maxScale, percentage;
                currentScale = gameObject.transform.localScale.x;
                minScale = level.startScale.x;
                maxScale = level.targetScale;
                percentage = ((currentScale - minScale) / (maxScale - minScale)) * 100;
                //Reset the position
                //If progress is good, call level won
                if (percentage > level.winZone)
                {
                    //LevelManager.Instance.wonLevel();
                   // StopAllCoroutines();
                }
                if(gameObject.transform.localScale != level.startScale)
                {
                    yield return StartCoroutine(descale(percentage));
                }
            }
            if (gameObject.transform.localScale.x >= level.targetScale * level.targetThreshold)
            {
                LevelManager.Instance.lostLevel();
            }
            yield return null;
        }
    }

    private IEnumerator descale(float percentage)
    {
        LevelManager.Instance.updatePercent(percentage);
        float descale = 0;
        while (true)
        {
            descale = Time.deltaTime * gameDescaleSpeed;
            if (gameObject.transform.localScale.x > level.startScale.x)
            {
                gameObject.transform.localScale -= new Vector3(descale, descale, descale);
            }
            else
            {
                gameObject.transform.localScale = level.startScale;
                break;
            }
            yield return new WaitForSeconds(0.001f);
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
