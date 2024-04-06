using System;
using System.Collections.Generic;

namespace Zelda {

    // 背包组件
    public class BagComponent {

        // null 表示空格子
        BagItemModel[] all;

        public BagComponent() { }

        public void Init(int maxSlot) {
            all = new BagItemModel[maxSlot];
        }

        // 添加(获得)物品
        public bool /*是否添加成功*/ Add(int typeID, int count, Func<BagItemModel> onAddItemToNewSlot) {

            // 是否已存在相同 typeID
            for (int i = 0; i < all.Length; i += 1) {
                BagItemModel old = all[i];
                if (old != null && old.typeID == typeID) {
                    // 叠加
                    int allowCount = old.countMax - old.count;
                    if (allowCount >= count) {
                        // max 50 - oldCount 48, count = 1
                        old.count += count;
                        return true;
                    } else {
                        // max 50 - oldCount 48, count = 3
                        old.count = old.countMax;
                        count -= allowCount;
                    }
                }
            }

            // 并没有叠加在相同的 TypeID 上
            if (count > 0) {
                int index = -1;
                for (int i = 0; i < all.Length; i += 1) {
                    BagItemModel old = all[i];
                    if (old == null) {
                        index = i;
                        break;
                    }
                }

                // 如果没有空格子
                if (index == -1) {
                    return false;
                }

                // 在空格子里添加新的物品, 并设置数量
                BagItemModel model = onAddItemToNewSlot.Invoke();
                all[index] = model;
                return true;

            } else {
                return true;
            }
        }

        // 查找物品

        // 移除物品

        // 遍历物品
        public void ForEach(Action<BagItemModel> callback) {
            for (int i = 0; i < all.Length; i += 1) {
                BagItemModel item = all[i];
                if (item != null) {
                    callback.Invoke(item);
                }
            }
        }

        public int GetMaxSlot() {
            return all.Length;
        }

        public int GetOccupiedSlot() {
            int count = 0;
            for (int i = 0; i < all.Length; i += 1) {
                if (all[i] != null) {
                    count += 1;
                }
            }
            return count;
        }

    }

}