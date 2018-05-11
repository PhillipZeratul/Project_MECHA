using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;


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
                float2 destination;

                if (player.Heading[i].isRight)
                    destination.x = player.Position[i].Value.x + camera.Camera[i].leftBound;
                else
                    destination.x = player.Position[i].Value.x - camera.Camera[i].rightBound;

                destination.y = player.Position[i].Value.y + camera.Camera[i].bottomBound;

                float distanceSquared = math.lengthSquared(destination - camera.Position[i].Value);

                if (distanceSquared > camera.Camera[i].lerpStartThres)
                    camera.Camera[i].isLerping = true;

                if (camera.Camera[i].isLerping)
                {
                    if (distanceSquared < camera.Camera[i].lerpStopThres)
                    {
                        camera.Position[i].Value = destination;
                        camera.Camera[i].isLerping = false;
                    }
                    else
                        camera.Position[i].Value = math.lerp(camera.Position[i].Value, destination, 0.2f);
                }
                else
                    camera.Position[i].Value = destination;
            }
        }
    }
}