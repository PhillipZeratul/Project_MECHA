using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;


namespace ProjectMecha
{
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

        private float minMoveThres = 0.001f;
        private RaycastHit2D[] raycastHit2Ds = new RaycastHit2D[16];
        private float shellRadius = 0.01f;
        private float minNormalY = 0.65f;


        protected override void OnUpdate()
        {
            float2 deltaTime = Time.deltaTime;

            for(int i = 0; i < group.Length; i++)
            {
                group.Velocity[i].Value += group.Gravity[i].Modifier * new float2(Physics2D.gravity.x, Physics2D.gravity.y) * deltaTime;
                float2 deltaPosition = group.Velocity[i].Value * deltaTime;
                Move(i, deltaPosition);
            }
        }

        private void Move(int index, float2 move)
        {
            float distance = math.length(move);
            if (distance > minMoveThres)
            {
                int count = group.Rigidbody[index].Cast(move, group.Gravity[index].ContactFilter2D, raycastHit2Ds, distance + shellRadius);

                for (int i = 0; i < count; i++)
                {
                    Vector2 currentNormal = raycastHit2Ds[i].normal;
                    if (currentNormal.y > minNormalY)
                        move.y = 0f;
                }

                group.Position[index].Value += move;
            }
        }
    }
}