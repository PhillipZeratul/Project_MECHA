using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;


namespace ProjectMecha
{
    [UpdateInGroup(typeof(CalculatePosition))]
    [UpdateAfter(typeof(CollideSystem))]
    public class MovementSystem : ComponentSystem
    {
        private struct Group
        {
            public int Length;
            public ComponentArray<Position2D> Position;
            [ReadOnly] public ComponentArray<Velocity> Velocity;
        } 
        [Inject] private Group group;


        protected override void OnUpdate()
        {
            float deltaTime = Time.deltaTime;

            for (int i = 0; i < group.Length; i++)
            {
                float2 velocity = group.Velocity[i].Value;

                group.Position[i].Local += velocity * deltaTime;
            }
        }
    }
}