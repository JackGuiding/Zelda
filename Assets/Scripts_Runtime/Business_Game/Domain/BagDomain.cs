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

            ui.Bag_OnUseHandle = (int id) => {
                Debug.Log("点击了: " + id);
            };

        }

        public static void Update(GameContext ctx, BagComponent bag) {
            var ui = ctx.ui;
            if (ui.Bag_IsOpened()) {
                Debug.Log("刷新背包");
            }
        }

    }
}