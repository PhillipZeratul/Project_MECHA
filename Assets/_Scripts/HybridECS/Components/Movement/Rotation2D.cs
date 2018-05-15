using UnityEngine;


namespace ProjectMecha
{
    [RequireComponent(typeof(Position2D))]
    [RequireComponent(typeof(Rotation2D))]
    public class Rotation2D : MonoBehaviour
    {
        public float z;
    }
}