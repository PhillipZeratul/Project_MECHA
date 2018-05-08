using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;


namespace ProjectMecha
{
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
            foreach (var entity in GetEntities<Data>())
            {
                float2 position = entity.Position.Value;
                int heading = entity.Heading.isRight ? 1 : -1;
                float3 oriScale = entity.Transform.localScale;

                entity.Transform.position = new float3(position.x, position.y, 0f);
                entity.Transform.localScale = new float3(heading * oriScale.x, oriScale.y, oriScale.z);
            }
        }
    }
}