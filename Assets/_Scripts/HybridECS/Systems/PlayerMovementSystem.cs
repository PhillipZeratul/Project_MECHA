using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;


namespace ProjectMecha
{
    public class PlayerMovementSystem : ComponentSystem
    {
        private struct PlayerData
        {
            public int Length;
            public GameObjectArray GameObject;
            public ComponentArray<Position2D> Position;
            public ComponentArray<Heading2D> Heading;
            public ComponentArray<PlayerInput> PlayerInput;
            public ComponentArray<Speed> Speed;
        } 
        [Inject] private PlayerData playerData;


        protected override void OnUpdate()
        {
            if (playerData.Length == 0)
                return;
            
            float deltaTime = Time.deltaTime;

            for (int i = 0; i < playerData.Length; i++)
            {
                float2 position = playerData.Position[i].Value;
                position.x += playerData.Speed[i].Value * playerData.PlayerInput[i].Horizontal * deltaTime;
                playerData.Position[i].Value = position;
            }
        }
    }
}