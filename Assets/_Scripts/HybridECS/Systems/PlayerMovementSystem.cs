using UnityEngine;
using Unity.Entities;


public class PlayerMovementSystem : ComponentSystem
{
    private struct Group
    {
        public Transform transform;
        public PlayerInput playerInput;
        public Speed speed;
    }

    protected override void OnUpdate()
    {
        float deltaTime = Time.deltaTime;

        foreach (var entity in GetEntities<Group>())
        {
            Vector3 position = entity.transform.position;
            position.x += entity.speed.value * entity.playerInput.horizontal * deltaTime;
            entity.transform.position = position;
        }
    }
}
