using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;


namespace ProjectMecha
{
    [ExecuteInEditMode]
    public class SyncTransformSystem : ComponentSystem
    {
        private struct Group
        {
            public int Length;
            public ComponentArray<Transform> Transform;
            public ComponentArray<Position2D> Position;
            public ComponentArray<Heading2D> Heading;
        }
        [Inject] private Group group;


        protected override void OnUpdate()
        {
            for (int i = 0; i < group.Length; i++)
            {
                if (Application.isEditor && !Application.isPlaying)
                {
                    group.Position[i].Value = new float2(group.Transform[i].position.x, group.Transform[i].position.y);
                    group.Heading[i].isRight = group.Transform[i].localScale.x > 0;
                }
                else
                {
                    float2 position = group.Position[i].Value;
                    int heading = group.Heading[i].isRight ? 1 : -1;
                    float3 oriScale = group.Transform[i].localScale;

                    group.Transform[i].position = new float3(position.x, position.y, group.Transform[i].position.z);
                    group.Transform[i].localScale = new float3(heading * math.abs(oriScale.x), oriScale.y, oriScale.z);
                }
            }
        }
    }
}