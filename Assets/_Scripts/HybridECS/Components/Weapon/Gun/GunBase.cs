using UnityEngine;


namespace ProjectMecha
{
    [RequireComponent(typeof(Position2D))]
    [RequireComponent(typeof(Faction))]
    [RequireComponent(typeof(Velocity))]
    public class GunBase : MonoBehaviour
    {
        public float TimeToLive;
        public float Energy;
        public float CoolDown;
        public float Speed;
    }
}