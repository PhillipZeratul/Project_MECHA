using UnityEngine;


namespace ProjectMecha
{
    [RequireComponent(typeof(Position2D))]
    [RequireComponent(typeof(Faction))]
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
}