using UnityEngine;

namespace Zelda {

    // 物品
    public class BagItemModel {

        // ID
        public int id; // 1, 2
        public int typeID; // 100 木剑
        public string name;
        public string description;
        public Sprite icon;

        // 数量
        public int count;
        public int countMax;

        // CD
        // public float cd;
        // public float cdMax;

        // 特性(通用)
        public bool isConsumable; // 可消耗

        // 特性
        public bool isEatable; // 药 / 食物
        public int eatRestoreHp; // 恢复生命值

        // public bool isCastable; // 技能

        // public bool isEquipable; // 装备

    }

}