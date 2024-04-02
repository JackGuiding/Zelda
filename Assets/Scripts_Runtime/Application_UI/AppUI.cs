using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zelda {

    public class AppUI {

        Dictionary<string, GameObject> prefabDict;

        Canvas canvas;

        Panel_Login login;

        public Action Login_OnStartHandle;

        public AppUI() {
            prefabDict = new Dictionary<string, GameObject>();
        }

        public void Inject(Canvas canvas, Panel_Login loginPrefab) {

            this.canvas = canvas;

            prefabDict.Add(nameof(Panel_Login), loginPrefab.gameObject);
            // prefabDict.Add("Panel_Login", loginPrefab.gameObject);
        }

        public void Login_Open() {
            GameObject go = Open(nameof(Panel_Login));
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

        // 打开 UI
        GameObject Open(string uiName) {
            bool has = prefabDict.TryGetValue(uiName, out GameObject prefab);
            if (!has) {
                Debug.LogError($"UI: {uiName} not found.");
                return null;
            }
            GameObject go = GameObject.Instantiate(prefab, canvas.transform);
            return go;
        }

    }

}