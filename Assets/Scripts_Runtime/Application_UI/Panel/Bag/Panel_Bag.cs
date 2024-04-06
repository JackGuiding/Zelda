using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zelda {

    public class Panel_Bag : MonoBehaviour {

        // 背包内容
        [SerializeField] GridLayoutGroup group;
        [SerializeField] Panel_BagElement prefabElement;

        List<Panel_BagElement> elements;

        public void Ctor() {
            elements = new List<Panel_BagElement>();
        }

        public void Init(int maxSlot) {
            // 生成空的背包格子
            for (int i = 0; i < maxSlot; i += 1) {
                Panel_BagElement ele = GameObject.Instantiate(prefabElement, group.transform);
                ele.Init(-1, null, 0);
                elements.Add(ele);
            }
        }

        public void Close() {
            // foreach (KeyValuePair<int, Panel_BagElement> kv in elements) {
            //     GameObject.Destroy(kv.Value.gameObject);
            // }
            foreach (var ele in elements) {
                GameObject.Destroy(ele.gameObject);
            }
            GameObject.Destroy(gameObject);
        }

        public void Add(int id, Sprite icon, int count) {
            // 逻辑: 找到非-1的空格子, 设置内容
            for (int i = 0; i < elements.Count; i += 1) {
                Panel_BagElement ele = elements[i];
                if (ele.id == -1) {
                    ele.Init(id, icon, count);
                    break;
                }
            }
        }

        public void Remove(int id) {
            int index = elements.FindIndex(value => value.id == id);
            if (index != -1) {
                GameObject.Destroy(elements[index].gameObject);
                elements.RemoveAt(index);
            }
        }

    }

}