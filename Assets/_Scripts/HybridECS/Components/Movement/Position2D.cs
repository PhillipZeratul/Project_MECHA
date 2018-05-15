using UnityEngine;
using Unity.Mathematics;


namespace ProjectMecha
{
    [RequireComponent(typeof(Heading2D))]
    [RequireComponent(typeof(Rotation2D))]
    public class Position2D : MonoBehaviour
    {
        //[HideInInspector]
        public float2 Local;
        public float2 Global
        {
            get {
                    temp.x = transform.position.x;
                    temp.y = transform.position.y;
                    return temp;
            }
            set {
                if (transform.parent)
                {
                    temp.x = value.x - transform.parent.transform.localPosition.x;
                    temp.y = value.y - transform.parent.transform.localPosition.y;
                    Local = temp; 
                }
                else
                    Local = value;
            }
        }

        private float2 temp;
    }
}