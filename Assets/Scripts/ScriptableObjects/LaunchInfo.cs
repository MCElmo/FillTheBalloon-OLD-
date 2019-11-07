using UnityEngine;

[CreateAssetMenu(fileName = "Launch Info", menuName = "Level/LaunchInfo")]
public class LaunchInfo : ScriptableObject
{
    public float launchDelay;
    public bool randomLaunchPoint;

    public bool leftLaunchPoint;
    //public JugglingBall ballPrefab;
    public float initialVelocity = 10f;

    public float throwVelocity = 8.5f;
}