using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;


namespace ProjectMecha
{
    public class CollideSystem : ComponentSystem
    {
        //private struct Group
        //{
        //    public int Length;
        //    public ComponentArray<Collider> Collider;
        //    public ComponentArray<Rigidbody2D> RigidBody;
        //    public ComponentArray<Velocity> Velocity;
        //}
        //[Inject] private Group group;


        //private RaycastHit2D[] raycastHit2Ds = new RaycastHit2D[16];
        //private float shellRadius = 0.01f;
        //private float minGroundNormalY = 0.65f;


        protected override void OnUpdate()
        {
            //float deltaTime = Time.deltaTime;
             

            //for (int i = 0; i < group.Length; i++)
            //{
            //    float distance = math.length(group.Velocity[i].Value);
            //    int count = group.RigidBody[i].Cast(group.Velocity[i].Value, group.Gravity[i].ContactFilter2D, raycastHit2Ds, distance + shellRadius);

            //    for (int i = 0; i < count; i++)
            //    {
            //        if (raycastHit2Ds[i].normal.y > minGroundNormalY)
            //        {
            //            group.Gravity[index].Grounded = true;
            //            move.y = 0f;
            //            group.Velocity[index].Value.y = 0f;
            //        }
            //    }
            //}
        }
    }
}