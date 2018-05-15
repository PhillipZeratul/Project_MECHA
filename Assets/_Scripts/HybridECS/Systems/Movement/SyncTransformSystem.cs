using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;


namespace ProjectMecha
{
    [ExecuteInEditMode]
    [UpdateInGroup(typeof(UpdateTransform))]
    public class SyncTransformSystem : ComponentSystem
    {
        private struct GroupWithHeading
        {
            public int Length;
            public ComponentArray<Transform> Transform;
            public ComponentArray<Position2D> Position;
            public ComponentArray<Rotation2D> Rotation;
            public ComponentArray<Heading2D> Heading;
        }
        [Inject] private GroupWithHeading group;


        protected override void OnUpdate()
        {
            if (Application.isEditor && !Application.isPlaying)
            {
                for (int i = 0; i < group.Length; i++)
                {
                    SyncPosition2D(group.Position[i], group.Transform[i]);
                    SyncRotation2D(group.Rotation[i], group.Transform[i]);
                    SyncHeading2D(group.Heading[i], group.Transform[i]);
                }
            }
            else
            {
                for (int i = 0; i < group.Length; i++)
                {
                    SyncPosition(group.Transform[i], group.Position[i]);
                    SyncRotation(group.Transform[i], group.Rotation[i]);
                    SyncScale(group.Transform[i], group.Heading[i]);
                }
            }
        }

        private void SyncPosition2D(Position2D position, Transform transform)
        {
            position.Local = new float2(transform.localPosition.x, transform.localPosition.y);
        }

        private void SyncRotation2D(Rotation2D rotation, Transform transform)
        {
            rotation.LocalZ = transform.localEulerAngles.z;
        }

        private void SyncHeading2D(Heading2D heading, Transform transform)
        {
            heading.LocalIsRight = transform.localScale.x > 0;
        }

        private void SyncPosition(Transform transform, Position2D position)
        {
            transform.localPosition = new Vector3(position.Local.x, position.Local.y, transform.localPosition.z);
        }

        private void SyncRotation(Transform transform, Rotation2D rotation)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, rotation.LocalZ);
        }

        private void SyncScale(Transform transform, Heading2D heading)
        {
            int headingSign = heading.LocalIsRight ? 1 : -1;
            float3 oriScale = transform.localScale;
            transform.localScale = new Vector3(headingSign * math.abs(oriScale.x), oriScale.y, oriScale.z);
        }
    }
}