using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;


namespace ProjectMecha
{
    [UpdateInGroup(typeof(CalculatePosition))]
    [UpdateBefore(typeof(CollideSystem))]
    public class GravitySystem : ComponentSystem
    {
        private struct Group
        {
            public int Length;
            public ComponentArray<Position2D> Position;
            public ComponentArray<Rigidbody2D> Rigidbody;
            public ComponentArray<Gravity> Gravity;
            public ComponentArray<Velocity> Velocity;
        }
        [Inject] private Group group;


        protected override void OnUpdate()
        {
            float2 deltaTime = Time.deltaTime;

            for(int i = 0; i < group.Length; i++)
                group.Velocity[i].Value += group.Gravity[i].Modifier * new float2(Physics2D.gravity.x, Physics2D.gravity.y) * deltaTime;
        }
    }
}