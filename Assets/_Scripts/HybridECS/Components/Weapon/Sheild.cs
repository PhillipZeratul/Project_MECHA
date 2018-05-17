using UnityEngine;


public class Sheild : MonoBehaviour
{
	public enum Type
    {
        Energy,
        Material
    }

    public Type Value;
    public float TimeToLive;
    public float Energy;
}
