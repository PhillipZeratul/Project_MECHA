using UnityEngine;
using Unity.Entities;


public class PlayerInputSystem : ComponentSystem
{
    private struct Group
    {
        public PlayerInput playerInput;
    }

    protected override void OnUpdate()
    {
        foreach (var entity in GetEntities<Group>())
        {
            entity.playerInput.horizontal = Input.GetAxis("Horizontal");
        }
    }
}
