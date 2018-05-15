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
                    group.Position[i].Value = SyncPosition2D(group.Transform[i]);
                    group.Rotation[i].z = SyncRotation2D(group.Transform[i]);
                    group.Heading[i].isRight = SyncHeading2D(group.Transform[i]);
                }
            }
            else
            {
                for (int i = 0; i < group.Length; i++)
                {
                    group.Transform[i].position = SyncTransform(group.Position[i], group.Transform[i]);
                    group.Transform[i].rotation = SyncRotation(group.Rotation[i], group.Transform[i]);
                    group.Transform[i].localScale = SyncScale(group.Heading[i], group.Transform[i]);
                }
            }
        }

        private float2 SyncPosition2D(Transform transform)
        {
            return new float2(transform.position.x, transform.position.y);
        }

        private float SyncRotation2D(Transform transform)
        {
            return transform.eulerAngles.z;
        }

        private bool SyncHeading2D(Transform transform)
        {
            return transform.localScale.x > 0;
        }

        private Vector3 SyncTransform(Position2D position, Transform transform)
        {
            return new Vector3(position.Value.x, position.Value.y, transform.position.z);
        }

        private Quaternion SyncRotation(Rotation2D rotation, Transform transform)
        {
            return Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, rotation.z);
        }

        private Vector3 SyncScale(Heading2D heading, Transform transform)
        {
            int headingSign = heading.isRight ? 1 : -1;
            float3 oriScale = transform.localScale;
            return new Vector3(headingSign * math.abs(oriScale.x), oriScale.y, oriScale.z);
        }
    }
}