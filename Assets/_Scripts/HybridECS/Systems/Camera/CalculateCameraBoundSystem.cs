using UnityEngine;
using Unity.Entities;


namespace ProjectMecha
{
    [ExecuteInEditMode]
    [UpdateInGroup(typeof(CalculatePosition))]
    public class CalculateCameraBoundSystem : ComponentSystem
    {
        private struct CameraData
        {
            public int Length;
            public ComponentArray<Position2D> Position;
            public ComponentArray<Camera> Camera;
            public ComponentArray<FollowPlayerCamera> FollowPlayerCamera;
        }
        [Inject] private CameraData camera;


        protected override void OnUpdate()
        {
            for (int i = 0; i < camera.Length; i++)
            {
                float leftBound = 0.5f * (1 - camera.FollowPlayerCamera[i].leftPortion);
                float rightBound = 0.5f * (1 + camera.FollowPlayerCamera[i].rightPortion);
                float bottomBound = 0.5f * (1 - camera.FollowPlayerCamera[i].bottomPortion);

                float z = camera.Camera[i].nearClipPlane;
                Vector3 pBL = camera.Camera[i].ViewportToWorldPoint(new Vector3(leftBound, bottomBound, z));
                Vector3 pBR = camera.Camera[i].ViewportToWorldPoint(new Vector3(rightBound, bottomBound, z));

                camera.FollowPlayerCamera[i].leftBound = camera.Position[i].Value.x - pBL.x;
                camera.FollowPlayerCamera[i].rightBound = pBR.x - camera.Position[i].Value.x;
                camera.FollowPlayerCamera[i].bottomBound = camera.Position[i].Value.y - pBL.y;

                DrawBounds(i, pBL, pBR);
            }
        }

        private void DrawBounds(int i, Vector3 pBL, Vector3 pBR)
        {
            Vector3 pTL = new Vector3(pBL.x, pBL.y + 2f, pBL.z);
            Vector3 pTR = new Vector3(pBR.x, pBR.y + 2f, pBR.z);

            Debug.DrawLine(pBL, pBR, new Color(255, 100, 0));
            Debug.DrawLine(pBL, pTL, new Color(255, 100, 0));
            Debug.DrawLine(pBR, pTR, new Color(255, 100, 0));
        }
    }
}