using Unity.Entities;
using Unity.Collections;


namespace ProjectMecha
{
    [UpdateInGroup(typeof(CalculatePosition))]
    [UpdateAfter(typeof(PlayerMoveInput))]
    [UpdateAfter(typeof(PlayerAimInput))]
    public class CalculateVelocitySystem : ComponentSystem
    {
        private struct Group
        {
            public int Length;
            public ComponentArray<Velocity> Velocity;
            public ComponentArray<Collidable> Collidable;
            public ComponentArray<Gravity> Gravity;
            [ReadOnly] public ComponentArray<Player> Player;
            [ReadOnly] public ComponentArray<PlayerMoveInput> PlayerMoveInput;
        }
        [Inject] private Group group;


        protected override void OnUpdate()
        {
            for (int i = 0; i < group.Length; i++)
            {
                group.Velocity[i].Value.x = group.PlayerMoveInput[i].Horizontal * group.Player[i].MoveVelocity;

                if (group.Gravity[i].Grounded)
                {
                    // TODO:~ Can we optimize this? Do not need to update mask every frame.
                    group.Collidable[i].ContactFilter2D.SetLayerMask(LayerManager.PlayerCollsionMask());

                    if (group.PlayerMoveInput[i].Jump)
                    {
                        group.Gravity[i].Grounded = false;
                        group.Velocity[i].Value.y = group.Player[i].JumpVelocity;
                    }

                    if (group.PlayerMoveInput[i].Down)
                    {
                        group.Gravity[i].Grounded = false;
                        group.Collidable[i].ContactFilter2D.SetLayerMask(LayerManager.PlayerCollsionMaskIgnorePlatform());
                    }
                }
            }
        }
    }
}