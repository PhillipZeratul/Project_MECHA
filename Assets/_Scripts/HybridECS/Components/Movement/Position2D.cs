using UnityEngine;
using Unity.Mathematics;


namespace ProjectMecha
{
    [RequireComponent(typeof(Heading2D))]
    [RequireComponent(typeof(Rotation2D))]
    public class Position2D : MonoBehaviour
    {
        //[HideInInspector]
        public float2 Value;
    }
}