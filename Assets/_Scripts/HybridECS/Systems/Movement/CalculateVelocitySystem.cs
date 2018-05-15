using Unity.Entities;
using Unity.Collections;


namespace ProjectMecha
{
    [UpdateInGroup(typeof(CalculatePosition))]
    [UpdateAfter(typeof(PlayerInput))]
    public class CalculateVelocitySystem : ComponentSystem
    {
        private struct Group
        {
            public int Length;
            public ComponentArray<Velocity> Velocity;
            public ComponentArray<Collidable> Collidable;
            [ReadOnly] public ComponentArray<PlayerInput> PlayerInput;
        }
        [Inject] Group group;


        protected override void OnUpdate()
        {
            for (int i = 0; i < group.Length; i++)
            {
                group.Velocity[i].Value.x = group.PlayerInput[i].Horizontal * group.Velocity[i].Modifier.x;

                if (group.Velocity[i].Grounded)
                {
                    group.Collidable[i].ContactFilter2D.SetLayerMask(LayerManager.PlayerCollsionMask());

                    if (group.PlayerInput[i].Jump)
                    {
                        group.Velocity[i].Grounded = false;
                        group.Velocity[i].Value.y = group.Velocity[i].Modifier.y;
                    }

                    if (group.PlayerInput[i].Down)
                    {
                        group.Velocity[i].Grounded = false;
                        group.Collidable[i].ContactFilter2D.SetLayerMask(LayerManager.PlayerCollsionMaskIgnorePlatform());
                    }
                }                           
            }
        }
    }
}