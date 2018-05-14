using UnityEngine;
using Unity.Entities;


namespace ProjectMecha
{
    [UpdateInGroup(typeof(CalculatePosition))]
    public class InitializeCollidableSystem : ComponentSystem
    {
        private struct Group
        {
            public int Length;
            public EntityArray Entity;
            public GameObjectArray GameObject;
            public ComponentArray<Collidable> Collidable;
            public ComponentArray<InitializeCollidable> InitializeGravity;
        }
        [Inject] private Group group;


        protected override void OnUpdate()
        {
            for (int i = 0; i < group.Length; i++)
            {
                var contactFilter2D = group.Collidable[i].ContactFilter2D;
                contactFilter2D.useTriggers = false;
                contactFilter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(group.GameObject[i].layer));
                contactFilter2D.useLayerMask = true;
                PostUpdateCommands.RemoveComponent<InitializeCollidable>(group.Entity[i]);
                GameObject.Destroy(group.GameObject[i].GetComponent<InitializeCollidable>());
            }
        }
    }
}