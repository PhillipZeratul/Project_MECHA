using UnityEngine;
using Unity.Entities;


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
            public ComponentArray<Velocity> Velocity;
            public ComponentArray<Gravity> Gravity;
        } 
        [Inject] private PlayerData playerData;


        protected override void OnUpdate()
        {
            if (playerData.Length == 0)
                return;
            
            float deltaTime = Time.deltaTime;

            for (int i = 0; i < playerData.Length; i++)
            {
                float horizontal = playerData.PlayerInput[i].Horizontal;
                playerData.Velocity[i].Value.x = playerData.Velocity[i].Modifier.x * horizontal;
                playerData.Position[i].Value.x += playerData.Velocity[i].Value.x * deltaTime;

                if (horizontal > 0)
                    playerData.Heading[i].isRight = true;
                else if (horizontal < 0)
                    playerData.Heading[i].isRight = false;

                if (playerData.Gravity[i].Grounded && playerData.PlayerInput[i].Jump)
                {
                    playerData.Gravity[i].Grounded = false;
                    playerData.Velocity[i].Value.y = playerData.Velocity[i].Modifier.y;
                }
            }
        }
    }
}