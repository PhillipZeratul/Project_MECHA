using Unity.Entities;


// TODO:~ Inheritence in ECS?
namespace ProjectMecha
{
    public class DestoryShotSystem : ComponentSystem
    {
        private struct Group
        {
            public int Length;
            public ComponentArray<Bullet> GunBase;
        }
        [Inject] private Group group;


        protected override void OnUpdate()
        {
            //throw new System.NotImplementedException();
        }
    }
}