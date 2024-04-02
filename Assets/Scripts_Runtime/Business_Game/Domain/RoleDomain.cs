using System;
using UnityEngine;

namespace Zelda {

    public static class RoleDomain {

        public static RoleEntity Spawn(GameContext ctx, int typeID) {
            RoleEntity role = GameFactory.Role_Create(ctx.assets, typeID);
            role.OnCollisionEnterHandle = OnCollisionEnter;

            // UI
            ctx.ui.HpBar_Open(role.id, 8.1f, 10);

            ctx.roleRepository.Add(role);
            return role;
        }

        public static void UpdateHUD(GameContext ctx, RoleEntity role, Vector3 cameraForward) {
            ctx.ui.HpBar_UpdatePosition(role.id, role.transform.position + Vector3.up * 2.3f, cameraForward);
        }

        static void OnCollisionEnter(RoleEntity role, Collision other) {
            if (other.gameObject.CompareTag("Ground")) {
                role.SetGround(true);
            }
        }

    }
}