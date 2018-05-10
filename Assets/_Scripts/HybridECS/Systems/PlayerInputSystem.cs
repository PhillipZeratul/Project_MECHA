using UnityEngine;
using Unity.Entities;
using Unity.Collections;


namespace ProjectMecha
{
    public class PlayerInputSystem : ComponentSystem
    {
        private struct Group
        {
            [ReadOnly] public PlayerInput PlayerInput;
        }

        protected override void OnUpdate()
        {
            foreach (var entity in GetEntities<Group>())
            {
                entity.PlayerInput.Horizontal = Input.GetAxis("Horizontal");
                entity.PlayerInput.Jump = Input.GetButtonDown("Jump");
            }
        }
    }
}