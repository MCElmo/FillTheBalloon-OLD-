using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "Level/Level")]
public class Level : ScriptableObject
{
    public int levelNumber;

    public float winZone;
    public float scaleRate;
    public float deScaleRate;

    public Color balloonColor;

    public float intensity;

    public float targetScale;

    public float targetThreshold;

    public Vector3 startScale;

    public Vector3 startPosition;
}