using UnityEngine;
using Unity.Entities;


namespace ProjectMecha
{
    [ExecuteInEditMode]
    public class FollowPlayerCameraSystem : ComponentSystem
    {
        private struct CameraData
        {
            public int Length;
            public ComponentArray<FollowPlayerCamera> Camera;
            public ComponentArray<Position2D> Position;
        }
        [Inject] CameraData camera;

        private struct PlayerData
        {
            public int Length;
            public ComponentArray<PlayerInput> PlayerInput;
            public ComponentArray<Position2D> Position;
            public ComponentArray<Heading2D> Heading;
        }
        [Inject] PlayerData player;


        protected override void OnUpdate()
        {
            if (camera.Length > player.Length)
                Debug.LogWarningFormat("Num of Camera: {0} is greater than Num of Player: {1}, Some camera may Not following!", camera.Length, player.Length);

            for (int i = 0; i < player.Length; i++)
            {
                camera.Position[i].Value = player.Position[i].Value;
            }
        }
    }
}