using UnityEngine;
using Unity.Entities;


namespace ProjectMecha
{
    [UpdateAfter(typeof(CalculateCameraBoundSystem))]
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
                // TODO:~ Follow Player.
                if (player.Heading[i].isRight && player.Position[i].Value.x > camera.Position[i].Value.x + camera.Camera[i].rightBound)
                {
                    // TODO:~ Interpolate
                    camera.Position[i].Value.x = player.Position[i].Value.x + camera.Camera[i].leftBound;
                }
                else if (!player.Heading[i].isRight && player.Position[i].Value.x < camera.Position[i].Value.x - camera.Camera[i].leftBound)
                {
                    // TODO:~ Interpolate
                    camera.Position[i].Value.x = player.Position[i].Value.x - camera.Camera[i].rightBound;
                }

                camera.Position[i].Value.y = player.Position[i].Value.y + camera.Camera[i].bottomBound;
            }
        }
    }
}