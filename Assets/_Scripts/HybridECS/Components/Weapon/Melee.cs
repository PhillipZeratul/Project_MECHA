using UnityEngine;


public class Melee : MonoBehaviour
{
	public enum Type
    {
        Sword,
        Hammer
    }

    public Type Value;
    public float TimeToLive;
    public float Energy;
}
