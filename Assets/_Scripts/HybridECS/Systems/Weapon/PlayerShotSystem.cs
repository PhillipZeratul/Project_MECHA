using Unity.Entities;


namespace ProjectMecha
{
    public class PlayerShotSystem : ComponentSystem
    {
        private struct Group
        {
            public int Length;
            public ComponentArray<PlayerFireInput> PlayerFireInput;
        }

        protected override void OnUpdate()
        {
            //throw new System.NotImplementedException();
        }
    }
}