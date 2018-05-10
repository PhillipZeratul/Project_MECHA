using UnityEngine;
using Unity.Mathematics;


namespace ProjectMecha
{
    public class Velocity : MonoBehaviour
    {
        // Currently Modifier.y is player's jump speed. May need to change this.
        public float2 Modifier;
        public float2 Value;
        public bool Grounded;
    }
}