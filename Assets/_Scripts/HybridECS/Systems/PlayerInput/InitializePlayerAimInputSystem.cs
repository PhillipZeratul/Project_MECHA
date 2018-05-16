using Unity.Entities;
using UnityEngine;


namespace ProjectMecha
{
    [UpdateInGroup(typeof(CalculatePosition))]
    [UpdateBefore(typeof(PlayerInputSystem))]
    public class InitializePlayerAimInputSystem : ComponentSystem
    {
        private struct Group
        {
            public int Length;
            public EntityArray Entity;
            public ComponentArray<InitializePlayerAimInput> Init;
            public ComponentArray<PlayerAimInput> PlayerAimInput;
        }
        [Inject] private Group group;


        protected override void OnUpdate()
        {
            for (int i = 0; i < group.Length; i++)
            {
                group.PlayerAimInput[i].camera = Camera.main;
                EntityManager.RemoveComponent<InitializePlayerAimInput>(group.Entity[i]);
            }
        }
    }
}