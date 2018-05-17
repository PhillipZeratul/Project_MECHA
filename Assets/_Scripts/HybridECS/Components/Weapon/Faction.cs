using UnityEngine;


namespace ProjectMecha
{
    public class Faction : MonoBehaviour
    {
    	public enum Type
        {
            Player,
            Enemy
        }

        public Type Value;
    }
}