using UnityEngine;
using Unity.Mathematics;


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
        public static void SpawnShot(GunBase shot, ShotSpawnData data)
        {
            var newShot = Object.Instantiate(shot);

            var shotPosition = newShot.GetComponent<Position2D>();
            shotPosition.Global = data.Position.Global;
            var shotRotation = newShot.GetComponent<Rotation2D>();
            shotRotation.GlobalZ = data.Rotation.GlobalZ;
            var faction = newShot.GetComponent<Faction>();
            faction.Value = data.Faction;
            var velocity = newShot.GetComponent<Velocity>();
            velocity.Value.x = math.cos(Mathf.Deg2Rad * data.Rotation.GlobalZ) * shot.Speed;
            velocity.Value.y = math.sin(Mathf.Deg2Rad * data.Rotation.GlobalZ) * shot.Speed;
        }
    }
}