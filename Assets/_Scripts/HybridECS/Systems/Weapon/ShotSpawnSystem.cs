using UnityEngine;


namespace ProjectMecha
{
    public class ShotSpawnData
    {
        public Position2D Position;
        public Rotation2D Rotation;
        public Faction.Type Faction;
    }


    public static class ShotSpawnSystem
    {
        public static void SpawnShot(GameObject shot, ShotSpawnData data)
        {
            var newShot = Object.Instantiate(shot);

            var shotPosition = newShot.GetComponent<Position2D>();
            shotPosition.Global = data.Position.Global;
            var shotRotation = newShot.GetComponent<Rotation2D>();
            shotRotation.GlobalZ = data.Rotation.GlobalZ;
            var faction = newShot.GetComponent<Faction>();
            faction.Value = data.Faction;
        }
    }
}