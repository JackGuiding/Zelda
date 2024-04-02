using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Zelda {

    public class ModuleAssets {

        Dictionary<string, GameObject> entities;
        Dictionary<string, GameObject> uiPrefabs;

        public ModuleAssets() {
            entities = new Dictionary<string, GameObject>();
            uiPrefabs = new Dictionary<string, GameObject>();
        }

        public void Load() {
            {
                AssetLabelReference labelReference = new AssetLabelReference();
                labelReference.labelString = "Entity";
                var list = Addressables.LoadAssetsAsync<GameObject>(labelReference, null).WaitForCompletion();
                foreach (var go in list) {
                    entities.Add(go.name, go);
                }
            }

            {
                // 从`硬盘`加载到`内存`
                AssetLabelReference labelReference = new AssetLabelReference();
                labelReference.labelString = "UI";
                var list = Addressables.LoadAssetsAsync<GameObject>(labelReference, null).WaitForCompletion();
                foreach (var go in list) {
                    uiPrefabs.Add(go.name, go);
                }
            }
        }

        public bool TryGetEntity(string name, out GameObject entity) {
            return entities.TryGetValue(name, out entity);
        }

        public bool TryGetUIPrefab(string name, out GameObject prefab) {
            return uiPrefabs.TryGetValue(name, out prefab);
        }

    }
}