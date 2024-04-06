using UnityEngine;

namespace Zelda {

    public static class BagDomain {

        public static void Toggle(GameContext ctx, BagComponent bag) {
            var ui = ctx.ui;
            if (ui.Bag_IsOpened()) {
                ui.Bag_Close();
            } else {
                Open(ctx, bag);
            }
        }

        public static void Open(GameContext ctx, BagComponent bag) {

            var ui = ctx.ui;

            // 空格子
            ui.Bag_Open(bag.GetMaxSlot());

            // 每个格子上的物品
            bag.ForEach(item => {
                ui.Bag_Add(item.id, item.icon, item.count);
            });

        }

        public static void Update(GameContext ctx, BagComponent bag) {
            var ui = ctx.ui;
            if (ui.Bag_IsOpened()) {
                ui.Bag_Close();
                Open(ctx, bag);
            }
        }

        public static void OnOwnerUse(GameContext ctx, int id) {
            // 找到主角
            bool has = ctx.roleRepository.TryGet(ctx.ownerRoleID, out RoleEntity owner);
            if (!has) {
                Debug.LogError("找不到主角: " + ctx.ownerRoleID);
                return;
            }

            // 找到物品
            has = owner.bagCom.TryGet(id, out BagItemModel item);
            if (!has) {
                Debug.LogError("找不到物品: " + id);
                return;
            }

            // 使用物品
            if (item.isEatable) {
                RoleDomain.Attr_RestoreHp(ctx, owner, item.eatRestoreHp);
                if (item.isConsumable) {

                    item.count -= 1;
                    if (item.count <= 0) {
                        has = owner.bagCom.Remove(id);
                    }

                    // 刷新背包
                    Update(ctx, owner.bagCom);

                }
            }
        }

    }
}