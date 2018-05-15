using UnityEngine;


namespace ProjectMecha
{
    [RequireComponent(typeof(Position2D))]
    [RequireComponent(typeof(Rotation2D))]
    public class Heading2D : MonoBehaviour
    {
        //[HideInInspector]
        public bool IsRight = true;
    }
}