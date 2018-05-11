using UnityEngine;


public class FollowPlayerCamera : MonoBehaviour
{
    public float lerpStartThres = 1f;
    public float lerpStopThres = 0.01f;
    public bool isLerping;

    public float leftPortion;
    public float rightPortion;
    public float bottomPortion;

    [HideInInspector]
    public float leftBound;
    [HideInInspector]
    public float rightBound;
    [HideInInspector]
    public float bottomBound;
}
