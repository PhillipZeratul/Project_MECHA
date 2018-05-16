using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections;


namespace ProjectMecha
{
    [UpdateInGroup(typeof(CalculatePosition))]
    [UpdateAfter(typeof(CalculateCameraBoundSystem))]
    [UpdateAfter(typeof(PlayerInputSystem))]
    public class FollowPlayerCameraSystem : ComponentSystem
    {
        private struct CameraGroup
        {
            public int Length;
            public ComponentArray<FollowPlayerCamera> Camera;
            public ComponentArray<Position2D> Position;
        }
        [Inject] CameraGroup cameraGroup;

        private struct PlayerAimGroup
        {
            public int Length;
            [ReadOnly] public ComponentArray<Position2D> Position;
            [ReadOnly] public ComponentArray<Aim> Aim;
            [ReadOnly] public ComponentArray<PlayerAimInput> PlayerAimInput;
        }
        [Inject] PlayerAimGroup aimGroup;

        private float distance = 3f;
        private float2 destination;


        // Follow with Aim
        protected override void OnUpdate()
        {
            if (cameraGroup.Length > aimGroup.Length)
                Debug.LogWarningFormat("Num of Camera: {0} is greater than Num of Player: {1}, Some camera may Not following!", cameraGroup.Length, aimGroup.Length);

            for (int i = 0; i < aimGroup.Length; i++)
            {
                destination = aimGroup.Position[i].Global + distance * math.normalize(aimGroup.Aim[i].Position - aimGroup.Position[i].Global);

                float distanceSquared = math.lengthSquared(destination - cameraGroup.Position[i].Local);

                if (distanceSquared > cameraGroup.Camera[i].lerpStartThres)              
                    cameraGroup.Camera[i].isLerping = true;

                if (cameraGroup.Camera[i].isLerping)
                {
                    if (distanceSquared < cameraGroup.Camera[i].lerpStopThres)
                    {
                        cameraGroup.Position[i].Local = destination;
                        cameraGroup.Camera[i].isLerping = false;
                    }
                    else
                        cameraGroup.Position[i].Local = math.lerp(cameraGroup.Position[i].Local, destination, cameraGroup.Camera[i].lerpSpeed);
                }
                else
                    cameraGroup.Position[i].Local = destination;
            }
        }

        // [Deprecated] Follow with Heading
        /*protected override void OnUpdate()
        {
            if (camera.Length > player.Length)
                Debug.LogWarningFormat("Num of Camera: {0} is greater than Num of Player: {1}, Some camera may Not following!", camera.Length, player.Length);

            for (int i = 0; i < player.Length; i++)
            {
                float2 destination;

                if (player.Heading[i].Value == Heading.Right)
                    destination.x = player.Position[i].Local.x + camera.Camera[i].leftBound;
                else if (player.Heading[i].Value == Heading.Left)
                    destination.x = player.Position[i].Local.x - camera.Camera[i].rightBound;
                else
                    destination.x = player.Position[i].Local.x;
                destination.y = player.Position[i].Local.y + camera.Camera[i].bottomBound;

                float distanceSquared = math.lengthSquared(destination - camera.Position[i].Local);

                if (distanceSquared > camera.Camera[i].lerpStartThres)              
                    camera.Camera[i].isLerping = true;

                if (camera.Camera[i].isLerping)
                {
                    if (distanceSquared < camera.Camera[i].lerpStopThres)
                    {
                        camera.Position[i].Local = destination;
                        camera.Camera[i].isLerping = false;
                    }
                    else
                        camera.Position[i].Local = math.lerp(camera.Position[i].Local, destination, camera.Camera[i].lerpSpeed);
                }
                else
                    camera.Position[i].Local = destination;
            }
        }*/
    }
}