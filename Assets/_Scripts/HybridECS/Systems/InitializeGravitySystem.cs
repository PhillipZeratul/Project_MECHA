using UnityEngine;
using Unity.Entities;


namespace ProjectMecha
{
    public class InitializeGravitySystem : ComponentSystem
    {
        private struct Group
        {
            public int Length;
            public EntityArray Entity;
            public GameObjectArray GameObject;
            public ComponentArray<Rigidbody2D> Rigidbody2D;
            public ComponentArray<Gravity> Gravity;
            public ComponentArray<InitializeGravity> InitializeGravity;
        }
        [Inject] private Group group;


        protected override void OnUpdate()
        {
            for (int i = 0; i < group.Length; i++)
            {
                var contactFilter2D = group.Gravity[i].ContactFilter2D;
                contactFilter2D.useTriggers = false;
                contactFilter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(group.GameObject[i].layer));
                contactFilter2D.useLayerMask = true;
                PostUpdateCommands.RemoveComponent<InitializeGravity>(group.Entity[i]);
                GameObject.Destroy(group.GameObject[i].GetComponent<InitializeGravity>());
            }
        }
    }
}