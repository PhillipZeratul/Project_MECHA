using UnityEngine;


public class Gun : MonoBehaviour
{
	public enum Type
    {
        Bullet,
        Laser
    }

    public Type Value;
    public float TimeToLive;
    public float Energy;
}
