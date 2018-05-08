using UnityEngine;
using Unity.Entities;


namespace ProjectMecha
{
    public class GravitySystem : ComponentSystem
    {
        private struct Group
        {
            public Speed Speed;
            public Gravity Gravity;
        }

        protected override void OnUpdate()
        {
            float deltaTime = Time.deltaTime;
            float deltaTimeSquared = deltaTime * deltaTime;

            foreach (var entity in GetEntities<Group>())
            {
                
            }
        }
    }
}