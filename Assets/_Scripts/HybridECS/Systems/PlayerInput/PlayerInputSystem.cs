using UnityEngine;
using Unity.Entities;
using Unity.Collections;


namespace ProjectMecha
{
    [UpdateInGroup(typeof(CalculatePosition))]
    public class PlayerInputSystem : ComponentSystem
    {
        private struct MoveGroup
        {
            public int Length;
            [ReadOnly] public ComponentArray<PlayerMoveInput> PlayerMoveInput;
        }

        private struct AimGroup
        {
            public int Length;
            [ReadOnly] public ComponentArray<PlayerAimInput> PlayerAimInput;
        }

        private struct FireGroup
        {
            public int Length;
            public ComponentArray<PlayerFireInput> PlayerFireInput;
        }

        [Inject] private MoveGroup moveGroup;
        [Inject] private AimGroup aimGroup;
        [Inject] private FireGroup fireGroup;


        protected override void OnUpdate()
        {
            for (int i = 0; i < moveGroup.Length; i++)
            {
                moveGroup.PlayerMoveInput[i].Horizontal = Input.GetAxis("Horizontal");
                moveGroup.PlayerMoveInput[i].Down = Input.GetAxis("Vertical") < -0.8;
                moveGroup.PlayerMoveInput[i].Jump = Input.GetButtonDown("Jump");
            }

            // TODO:~ Add Controller Aim Support
            for (int i = 0; i < aimGroup.Length; i++)
            {
                Vector3 point = aimGroup.PlayerAimInput[i].Camera.ScreenToWorldPoint(Input.mousePosition);

                aimGroup.PlayerAimInput[i].AimPosition.x = point.x;
                aimGroup.PlayerAimInput[i].AimPosition.y = point.y;
            }

            // TODO:~ Add Controller Fire Support, in Input Manager
            for (int i = 0; i < fireGroup.Length; i++)
            {
                var fireInput = fireGroup.PlayerFireInput[i];

                fireInput.IsFiringGun = CommonUtility.NearlyEqual(fireInput.GunCoolDown, 0f) && Input.GetButton("Gun");
                fireInput.IsFiringRocket = CommonUtility.NearlyEqual(fireInput.RocketCoolDown, 0f) && Input.GetButton("Rocket");
                fireInput.IsMelee = CommonUtility.NearlyEqual(fireInput.MeleeCoolDown, 0f) && Input.GetButton("Melee");
                fireInput.IsSheild = Input.GetButton("Shield");
            }
        }
    }
}