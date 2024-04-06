using UnityEngine;

namespace Zelda {

    public static class BusinessGame {

        public static void Enter(GameContext ctx) {

            RoleEntity owner = RoleDomain.Spawn(ctx, 0);
            ctx.ownerRoleID = owner.id;

            LootEntity loot1 = LootDomain.Spawn(ctx, 1, 10, new Vector3(0, 0, 5));
            LootEntity loot2 = LootDomain.Spawn(ctx, 2, 10, new Vector3(2, 0, -5));

        }

        public static void FixedTick(GameContext ctx, float fixdt) {

            ModuleInput input = ctx.input;

            bool hasOwner = ctx.roleRepository.TryGet(ctx.ownerRoleID, out RoleEntity owner);
            if (!hasOwner) {
                return;
            }

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

            /* 
                相机处理:
                1. 基础: 推拉, 摇, 移, 缩放
                2. 进阶: 跟随, 看向
                3. 再进阶: 绕(摇+移+跟+看), 希区柯克式运境, 效果(震屏)
            */

            bool hasOwner = ctx.roleRepository.TryGet(ctx.ownerRoleID, out RoleEntity owner);
            if (!hasOwner) {
                return;
            }

            ModuleCamera moduleCamera = ctx.moduleCamera;

            // 跟随
            // moduleCamera.Follow(role.transform.position, new Vector2(-3, 2), 5);

            // 看向
            // 注: 看向会影响`单纯旋转`, 所以`单纯旋转`必定是失效的
            // moduleCamera.LookAt(role.transform.position);

            // 单纯旋转
            // moduleCamera.Rotate(ctx.input.cameraRotationAxis, dt);

            // 绕
            // 注: 绕会影响`看向`和`跟随`
            moduleCamera.Round(owner.transform.position, ctx.input.cameraRotationAxis, new Vector2(0, 0), 15, dt);

            /*
                UI: Panel & HUD
            */
            RoleDomain.UpdateHUD(ctx, owner, moduleCamera.camera.transform.forward);

            ModuleInput input = ctx.input;
            if (input.isUIToggleBag) {
                BagDomain.Toggle(ctx, owner.bagCom);
            }

        }

    }
}