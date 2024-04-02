using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Zelda {

    public class ModuleAssets {

        Dictionary<string, GameObject> entities;
        Dictionary<string, GameObject> uiPrefabs;

        AsyncOperationHandle entityPtr;
        AsyncOperationHandle uiPrefabPtr;

        public ModuleAssets() {
            entities = new Dictionary<string, GameObject>();
            uiPrefabs = new Dictionary<string, GameObject>();
        }

        public void Load() {
            {
                AssetLabelReference labelReference = new AssetLabelReference();
                labelReference.labelString = "Entity";

                // AsyncOperationHandle<IList<GameObject>> ptr = Addressables.LoadAssetsAsync<GameObject>(labelReference, null);
                var ptr = Addressables.LoadAssetsAsync<GameObject>(labelReference, null);
                
                var list = ptr.WaitForCompletion();
                foreach (var go in list) {
                    entities.Add(go.name, go);
                }
                this.entityPtr = ptr;
            }

            {
                // 从`硬盘`加载到`内存`
                AssetLabelReference labelReference = new AssetLabelReference();
                labelReference.labelString = "UI";
                var ptr = Addressables.LoadAssetsAsync<GameObject>(labelReference, null);
                var list = ptr.WaitForCompletion();
                foreach (var go in list) {
                    uiPrefabs.Add(go.name, go);
                }
                this.uiPrefabPtr = ptr;
            }
        }

        public void Unload() {
            // 释放(非托管的)内存
            if (entityPtr.IsValid()) {
                Addressables.Release(entityPtr);
            }
            if (uiPrefabPtr.IsValid()) {
                Addressables.Release(uiPrefabPtr);
            }
            Debug.Log("ModuleAssets: Unloaded");
        }

        public bool TryGetEntity(string name, out GameObject entity) {
            return entities.TryGetValue(name, out entity);
        }

        public bool TryGetUIPrefab(string name, out GameObject prefab) {
            return uiPrefabs.TryGetValue(name, out prefab);
        }

    }
}