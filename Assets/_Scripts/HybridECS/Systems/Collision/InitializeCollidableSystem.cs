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
                contactFilter2D.SetLayerMask(LayerManager.PlayerCollsionMask());
                contactFilter2D.useLayerMask = true;
                PostUpdateCommands.RemoveComponent<InitializeCollidable>(group.Entity[i]);
            }
        }
    }
}