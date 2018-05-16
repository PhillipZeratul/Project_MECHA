using UnityEngine;


namespace ProjectMecha
{
    [RequireComponent(typeof(Position2D))]
    [RequireComponent(typeof(Heading2D))]
    public class Rotation2D : MonoBehaviour
    {
        public float LocalZ;
        public float GlobalZ
        {
            get {
                return transform.eulerAngles.z;
            }
            set {
                if (transform.parent)
                {
                    temp = value - transform.parent.transform.localEulerAngles.z;
                    LocalZ = temp; 
                }
                else
                    LocalZ = value;
            }
        }

        private float temp;
    }
}