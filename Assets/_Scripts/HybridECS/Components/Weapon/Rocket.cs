using UnityEngine;


public class Rocket : MonoBehaviour
{
	public enum Type
    {
        Straight,
        Follow
    }

    public Type Value;
    public float TimeToLive;
    public float Energy;
}
