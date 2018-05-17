using UnityEngine;


namespace ProjectMecha
{
    public class Weapon : MonoBehaviour
    {
        public Gun Bullet;
        public Position2D GunPointPosition;
        public Rotation2D GunPointRotation;

        public Rocket Rocket;
        public Position2D RocketPointPosition;
        public Rotation2D RocketPointRotation;

        public Melee Melee;
        public Position2D MeleePointPosition;
        public Rotation2D MeleePointRotation;

        public Sheild Sheild;
        public Position2D SheildPointPosition;
        public Rotation2D SheildPointRotation;
    }
}