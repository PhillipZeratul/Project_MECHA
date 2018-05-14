using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections;


namespace ProjectMecha
{
    [UpdateInGroup(typeof(CalculatePosition))]
    [UpdateAfter(typeof(CalculateVelocitySystem))]
    public class CollideSystem : ComponentSystem
    {
        private struct Group
        {
            public int Length;
            public ComponentArray<Rigidbody2D> RigidBody;
            public ComponentArray<Position2D> Position;
            [ReadOnly] public ComponentArray<Collidable> Collidable;
            [ReadOnly] public ComponentArray<Velocity> Velocity;
        }
        [Inject] private Group group;


        private RaycastHit2D[] raycastHit2Ds = new RaycastHit2D[16];
        private float shellRadius = 0.01f;
        private float minCollideNormal = 0.65f;


        protected override void OnUpdate()
        {
            float deltaTime = Time.deltaTime;
             
            for (int i = 0; i < group.Length; i++)
            {
                float distance = math.length(group.Velocity[i].Value * deltaTime);
                int count = group.RigidBody[i].Cast(group.Velocity[i].Value, group.Collidable[i].ContactFilter2D, raycastHit2Ds, distance + shellRadius);

                for (int j = 0; j < count; j++)
                {
                    // Move the object to collide point if collision is detected,
                    // but object is adjacent to collider, collision system will always report collision,
                    // so check if the object is still moving to the collider with the sign of velocity and normal.

                    // Vertical Down Collision
                    if (raycastHit2Ds[j].normal.y > minCollideNormal && group.Velocity[i].Value.y < 0f)
                    {
                        group.Velocity[i].Grounded = true;
                        group.Velocity[i].Value.y = 0f;
                        group.Position[i].Value.y -= raycastHit2Ds[j].distance * raycastHit2Ds[j].normal.y;
                    }
                    // Vertical Up Collision
                    else if (-raycastHit2Ds[j].normal.y > minCollideNormal && group.Velocity[i].Value.y > 0f)
                    {
                        if (raycastHit2Ds[j].transform.gameObject.layer != LayerMask.NameToLayer("Platform"))
                        {
                            group.Velocity[i].Value.y = 0f;
                            group.Position[i].Value.y -= raycastHit2Ds[j].distance * raycastHit2Ds[j].normal.y;
                        }
                    }
                    // Horizontal Collision
                    else if (-raycastHit2Ds[j].normal.x * math.sign(group.Velocity[i].Value.x) > minCollideNormal)
                    {
                        if (raycastHit2Ds[j].transform.gameObject.layer != LayerMask.NameToLayer("Platform"))
                        {
                            group.Velocity[i].Value.x = 0f;
                            group.Position[i].Value.x -= raycastHit2Ds[j].distance * raycastHit2Ds[j].normal.x;
                        }
                    }
                }

                if (!CommonUtility.NearlyEqual(group.Velocity[i].Value.y, 0f))
                    group.Velocity[i].Grounded = false;
            }
        }
    }
}