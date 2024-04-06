using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zelda {

    public class AppUI {

        ModuleAssets assets;

        Canvas screenCanvas;
        Canvas worldCanvas;

        Panel_Login login; // Unique 唯一
        Panel_Bag bag;
        Dictionary<int /*EntityID*/, HUD_HpBar> hpBars;

        public Action Login_OnStartHandle;

        public Action<int> Bag_OnUseHandle;

        public AppUI() {
            hpBars = new Dictionary<int, HUD_HpBar>();
        }

        public void Inject(ModuleAssets assets, Canvas screenCanvas, Canvas worldCanvas) {
            this.assets = assets;
            this.screenCanvas = screenCanvas;
            this.worldCanvas = worldCanvas;
        }

        // - Login
        #region Panel_Login
        public void Login_Open() {
            GameObject go = Open(nameof(Panel_Login), screenCanvas);
            login = go.GetComponent<Panel_Login>();
            login.Ctor();

            login.OnStartHandle = () => {
                Login_OnStartHandle.Invoke();
            };
        }

        public void Login_Close() {
            GameObject.Destroy(login.gameObject);
            login = null;
        }
        #endregion

        // - Bag
        // 打开背包时, 需要生成空格子
        public void Bag_Open(int maxSlot) {
            if (bag == null) {
                GameObject go = Open(nameof(Panel_Bag), screenCanvas);
                Panel_Bag panel = go.GetComponent<Panel_Bag>();
                panel.Ctor();
                panel.OnUseHandle = (int id) => {
                    Bag_OnUseHandle.Invoke(id);
                };
                this.bag = panel;
            }

            bag.Init(maxSlot);
        }

        public void Bag_Add(int id, Sprite icon, int count) {
            bag?.Add(id, icon, count);
        }

        public void Bag_Close() {
            bag?.Close();
        }

        // - HpBar
        #region HUD_HpBar
        public void HpBar_Open(int id, float hp, float hpMax) {
            GameObject go = Open(nameof(HUD_HpBar), worldCanvas);
            HUD_HpBar hpBar = go.GetComponent<HUD_HpBar>();
            hpBar.Ctor();
            hpBar.SetHp(hp, hpMax);
            hpBars.Add(id, hpBar);
        }

        public void HpBar_UpdatePosition(int id, Vector3 position, Vector3 cameraForward) {
            hpBars.TryGetValue(id, out HUD_HpBar hpBar);
            hpBar.SetPosition(position, cameraForward);
        }
        #endregion

        // 打开 UI
        GameObject Open(string uiName, Canvas canvas) {
            bool has = assets.TryGetUIPrefab(uiName, out GameObject prefab);
            if (!has) {
                Debug.LogError($"UI: {uiName} not found.");
                return null;
            }
            GameObject go = GameObject.Instantiate(prefab, canvas.transform);
            return go;
        }

    }

}