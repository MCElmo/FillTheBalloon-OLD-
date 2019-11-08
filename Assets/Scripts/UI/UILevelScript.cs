using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILevelScript : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;

    private void Awake()
    {
        LevelManager.LevelChanged += updateLevelText;
    }

    private void OnEnable()
    {
        updateLevelText(LevelManager.Instance.getLevel());
    }
    
    private void updateLevelText(int level)
    {
        levelText.text = "Level " + level;
    }

}
