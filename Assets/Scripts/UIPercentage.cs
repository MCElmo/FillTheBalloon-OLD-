using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPercentage : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private void Awake()
    {
        LevelManager.PercentageUpdated += PercentageUpdated;    
    }

    void PercentageUpdated(float percent)
    {
        if(percent > 100)
            percent = 100;
        text.text = String.Format("{0:0.##} %", percent);
        text.color = Color.Lerp(Color.red, Color.green, percent/100);
    }
}
