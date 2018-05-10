using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;


namespace ProjectMecha
{
    [UpdateAfter(typeof(CollideSystem))]
    public class MovementSystem : ComponentSystem
    {
        private struct Group
        {
            public int Length;
            public ComponentArray<Position2D> Position;
            public ComponentArray<Heading2D> Heading;
            [ReadOnly] public ComponentArray<Velocity> Velocity;
        } 
        [Inject] private Group group;


        protected override void OnUpdate()
        {
            float deltaTime = Time.deltaTime;

            for (int i = 0; i < group.Length; i++)
            {
                float2 velocity = group.Velocity[i].Value;

                group.Position[i].Value += velocity * deltaTime;

                if (velocity.x > 0)
                    group.Heading[i].isRight = true;
                else if (velocity.x < 0)
                    group.Heading[i].isRight = false;
            }
        }
    }
}