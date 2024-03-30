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

        static void CheckGrounded(RoleEntity role) {
            RaycastHit[] hits = Physics.RaycastAll(role.transform.position + Vector3.up, Vector3.down, 1.05f);
            Debug.DrawRay(role.transform.position + Vector3.up, Vector3.down * 1.05f, Color.red);
            Debug.Log(role.isGrounded + " " + hits.Length);
            if (hits != null) {
                for (int i = 0; i < hits.Length; i += 1) {
                    var hit = hits[i];
                    if (hit.collider.CompareTag("Ground")) {
                        role.SetGround(true);
                        break;
                    }
                }
            }
        }

    }
}