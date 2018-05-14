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
            public ComponentArray<Heading2D> Heading;
        }
        [Inject] private GroupWithHeading group;

        private struct GroupWithoutHeading
        {
            public int Length;
            public ComponentArray<Transform> Transform;
            public ComponentArray<Position2D> Position;
            public SubtractiveComponent<Heading2D> Heading;
        }
        [Inject] private GroupWithoutHeading group2;


        protected override void OnUpdate()
        {
            if (Application.isEditor && !Application.isPlaying)
            {
                for (int i = 0; i < group.Length; i++)
                {
                    group.Position[i].Value = SyncPosition(group.Transform[i]);
                    group.Heading[i].isRight = SyncHeading(group.Transform[i]);
                }
                for (int i = 0; i < group2.Length; i++)
                    group2.Position[i].Value = SyncPosition(group2.Transform[i]);
            }
            else
            {
                for (int i = 0; i < group.Length; i++)
                {
                    group.Transform[i].position = SyncTransform(group.Position[i], group.Transform[i]);
                    group.Transform[i].localScale = SyncScale(group.Heading[i], group.Transform[i]);
                }
                for (int i = 0; i < group2.Length; i++)
                    group2.Transform[i].position = SyncTransform(group2.Position[i], group2.Transform[i]);
            }
        }

        private float2 SyncPosition(Transform transform)
        {
            return new float2(transform.position.x, transform.position.y);
        }

        private bool SyncHeading(Transform transform)
        {
            return transform.localScale.x > 0;
        }

        private Vector3 SyncTransform(Position2D position, Transform transform)
        {
            return new Vector3(position.Value.x, position.Value.y, transform.position.z);
        }

        private Vector3 SyncScale(Heading2D heading, Transform transform)
        {
            int headingSign = heading.isRight ? 1 : -1;
            float3 oriScale = transform.localScale;
            return new Vector3(headingSign * math.abs(oriScale.x), oriScale.y, oriScale.z);
        }
    }
}