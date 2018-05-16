using Unity.Entities;
using Unity.Collections;


namespace ProjectMecha
{
    [UpdateInGroup(typeof(CalculatePosition))]
    [UpdateBefore(typeof(AimSystem))]
    public class UpdatePlayerAimSystem : ComponentSystem
    {
        private struct Group
        {
            public int Length;
            public ComponentArray<Aim> Aim;
            [ReadOnly] public ComponentArray<PlayerAimInput> PlayerAimInput;
        }
        [Inject] private Group group;


        protected override void OnUpdate()
        {
            for (int i = 0; i < group.Length; i++)
            {
                group.Aim[i].Position = group.PlayerAimInput[i].AimPosition;
            }
        }
    }
}