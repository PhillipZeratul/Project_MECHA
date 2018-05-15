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

                group.Position[i].Local += velocity * deltaTime;

                // [Deprecated] Use left or right key to change facing.
                // Trying to change this to use aim position to change facing.
                //if (velocity.x > 0)
                //    group.Heading[i].IsRight = true;
                //else if (velocity.x < 0)
                    //group.Heading[i].IsRight = false;
            }
        }
    }
}