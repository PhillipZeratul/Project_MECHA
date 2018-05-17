using UnityEngine;


namespace ProjectMecha
{
    public class PlayerFireInput : MonoBehaviour
    {
        public bool IsFiringGun;
        public bool IsFiringRocket;
        public bool IsMelee;
        public bool IsSheild;

        public float GunCoolDown;
        public float RocketCoolDown;
        public float MeleeCoolDown;
    }
}