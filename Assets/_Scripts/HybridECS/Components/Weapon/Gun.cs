using UnityEngine;


namespace ProjectMecha
{
    [RequireComponent(typeof(Position2D))]
    [RequireComponent(typeof(Faction))]
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
        public float CoolDown;
    }
}