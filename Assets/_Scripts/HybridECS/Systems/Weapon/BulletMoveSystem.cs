using Unity.Entities;


namespace ProjectMecha
{
    public class BulletMoveSystem : ComponentSystem
    {
        private struct Group
        {
            public int Length;
            public ComponentArray<Bullet> Bullet;
            public ComponentArray<Velocity> Velocity;
        }
        [Inject] private Group group;


        protected override void OnUpdate()
        {
            for (int i = 0; i < group.Length; i++)
            {

            }
        }
    }
}