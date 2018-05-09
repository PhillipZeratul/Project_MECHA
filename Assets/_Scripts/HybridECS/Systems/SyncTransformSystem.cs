using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;


namespace ProjectMecha
{
    [ExecuteInEditMode]
    public class SyncTransformSystem : ComponentSystem
    {
        private struct Data
        {
            [ReadOnly] public Position2D Position;
            [ReadOnly] public Heading2D Heading;
            public Transform Transform;
        }


        protected override void OnUpdate()
        {
            if (Application.isEditor && !Application.isPlaying)
            {
                foreach (var entity in GetEntities<Data>())
                {
                    entity.Position.Value = new float2(entity.Transform.position.x, entity.Transform.position.y);
                    entity.Heading.isRight = entity.Transform.localScale.x > 0;
                }
            }
            else
            {
                foreach (var entity in GetEntities<Data>())
                {
                    float2 position = entity.Position.Value;
                    int heading = entity.Heading.isRight ? 1 : -1;
                    float3 oriScale = entity.Transform.localScale;

                    entity.Transform.position = new float3(position.x, position.y, 0f);
                    entity.Transform.localScale = new float3(heading * math.abs(oriScale.x), oriScale.y, oriScale.z);
                }
            }
        }
    }
}