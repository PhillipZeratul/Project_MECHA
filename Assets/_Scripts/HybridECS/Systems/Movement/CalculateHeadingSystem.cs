using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;


namespace ProjectMecha
{
    public class CalculateHeadingSystem : ComponentSystem
    {
        private struct MoveGroup
        {
            public int Length;
            public ComponentArray<Heading2D> Heading;
            public ComponentArray<PlayerMoveInput> PlayerMoveInput;
        }
        [Inject] private MoveGroup moveGroup;

        private struct AimGroup
        {
            public int Length;
            public ComponentArray<Aim> Aim;
            public ComponentArray<PlayerAimInput> PlayerAimInput;
        }
        [Inject] private AimGroup aimGroup;

        private float cutOffAngle = 60f; // Rotation from horizontal right x axis to cut off angle.


        protected override void OnUpdate()
        {
            if (moveGroup.Length != aimGroup.Length)
            {
                Debug.LogFormat("Length of moveGroup = {0}, Length of aimGroup = {1}, Not Equal, Abort!", moveGroup.Length, aimGroup.Length);
                return;
            }

            for (int i = 0; i < moveGroup.Length; i++)
            {
                if (math.abs(aimGroup.Aim[i].RotationZ) < cutOffAngle)
                    moveGroup.Heading[i].Value = Heading.Right;
                else if (math.abs(aimGroup.Aim[i].RotationZ) > 180f - cutOffAngle)
                    moveGroup.Heading[i].Value = Heading.Left;
                else
                    moveGroup.Heading[i].Value = Heading.Front;
            }
        }
    }
}