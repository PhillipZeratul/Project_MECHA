using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;


namespace ProjectMecha
{
    [UpdateAfter(typeof(PlayerFireInput))]
    public class PlayerShotSystem : ComponentSystem
    {
        private struct Group
        {
            public int Length;
            public ComponentArray<PlayerFireInput> PlayerFireInput;
            public ComponentArray<Weapon> Weapon;
        }
        [Inject] private Group group;


        protected override void OnUpdate()
        {
            float deltaTime = Time.deltaTime;

            for (int i = 0; i < group.Length; i++)
            {
                var fireInput = group.PlayerFireInput[i];
                var weapon = group.Weapon[i];

                if (fireInput.IsFiringGun)
                {
                    fireInput.GunCoolDown = weapon.Gun.CoolDown;

                    ShotSpawnData shotSpawnData = new ShotSpawnData
                    {
                        Position = weapon.GunPointPosition,
                        Rotation = weapon.GunPointRotation,
                        Faction = Faction.Type.Player
                    };

                    ShotSpawnSystem.SpawnShot(weapon.Gun, shotSpawnData);
                }
                else
                    fireInput.GunCoolDown = math.max(fireInput.GunCoolDown - deltaTime, 0f);
            }
        }
    }
}