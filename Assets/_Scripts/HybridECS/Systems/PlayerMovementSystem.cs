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
        } 
        [Inject] private PlayerData playerData;


        protected override void OnUpdate()
        {
            if (playerData.Length == 0)
                return;
            
            float deltaTime = Time.deltaTime;

            for (int i = 0; i < playerData.Length; i++)
            {
                Position2D position = playerData.Position[i];
                float horizontal = playerData.PlayerInput[i].Horizontal;
                position.Value.x += playerData.Velocity[i].Modifier.x * horizontal * deltaTime;

                if (horizontal > 0)
                    playerData.Heading[i].isRight = true;
                else if (horizontal < 0)
                    playerData.Heading[i].isRight = false;
            }
        }
    }
}