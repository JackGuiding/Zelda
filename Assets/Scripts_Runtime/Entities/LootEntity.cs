using UnityEngine;

namespace Zelda {

    // 掉在地上的物品(不是Item)
    public class LootEntity : MonoBehaviour {

        public int id;

        // Item
        public int itemTypeID;
        public int itemCount;

        public void Ctor() {

        }

        public void SetPos(Vector3 pos) {
            transform.position = pos;
        }

    }

}