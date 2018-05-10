using UnityEngine;


public class FollowPlayerCamera : MonoBehaviour
{
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
