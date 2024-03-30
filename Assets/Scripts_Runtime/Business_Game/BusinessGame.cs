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

            Camera cam = ctx.moduleCamera.camera;
            Quaternion quaternion = cam.transform.rotation;

            Quaternion q = Quaternion.Euler(0, 90, 0); // 绕 y 轴旋转 90 度
            Vector3 fwd2 = q * Vector3.forward; // (0, 0, 1) -> (1, 0, 0)
            Debug.Log($"fwd2: {fwd2} right: {Vector3.right} ");

            // 四元数 * Vector3 = 旋转后的 Vector3

            Vector3 fwd = Vector3.forward; // (0, 0, 1)
            fwd = quaternion * fwd;

            Vector3 cameraFwd = cam.transform.forward;
            // Debug.Log($"fwd: {fwd} cameraFwd: {cameraFwd}");

            Vector3 moveDir = new Vector3(input.moveAxis.x, 0, input.moveAxis.y);
            moveDir = quaternion * moveDir;
            owner.Move(moveDir, fixdt);
            owner.Face(moveDir, fixdt);

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