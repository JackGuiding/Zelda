using UnityEngine;

namespace Zelda {

    public static class BusinessGame {

        public static void Enter(GameContext ctx) {
            RoleEntity owner = RoleDomain.Spawn(ctx, 0);
            ctx.ownerRoleID = owner.id;
        }

        public static void FixedTick(GameContext ctx, float fixdt) {

            ModuleInput input = ctx.input;

            bool hasOwner = ctx.roleRepository.TryGet(ctx.ownerRoleID, out RoleEntity owner);

            owner.Move(input.moveCameraDir, fixdt);
            owner.Face(input.moveCameraDir, fixdt);

            // 通过射线检测地面
            // CheckGrounded();

            owner.Jump(input.isJump);
            if (input.isAttack) {
                owner.Anim_Attack();
            }

        }

        public static void LateTick(GameContext ctx, float dt) {

            // 摄像机跟随
            ModuleCamera moduleCamera = ctx.moduleCamera;
            bool hasOwner = ctx.roleRepository.TryGet(ctx.ownerRoleID, out RoleEntity role);
            moduleCamera.Follow(role.transform.position, 2, 5);

        }

    }
}