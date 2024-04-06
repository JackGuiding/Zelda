using UnityEngine;

namespace Zelda {

    public static class BagDomain {

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

    }
}