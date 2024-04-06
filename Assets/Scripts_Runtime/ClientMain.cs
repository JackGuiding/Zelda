using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zelda {

    public class ClientMain : MonoBehaviour {

        [SerializeField] Camera mainCamera;

        [SerializeField] Canvas screenCanvas;
        [SerializeField] Canvas worldCanvas;

        AppUI ui;
        ModuleInput input;
        ModuleAssets assets;
        ModuleCamera moduleCamera;

        GameContext gameContext;

        bool isTearDown; // 是否已销毁

        void Awake() {

            isTearDown = false;

            // ==== Phase: Instantiate ====
            ui = new AppUI();

            input = new ModuleInput();
            assets = new ModuleAssets();
            moduleCamera = new ModuleCamera();

            gameContext = new GameContext();

            // ==== Phase: Inject ====
            ui.Inject(assets, screenCanvas, worldCanvas);
            moduleCamera.Inject(mainCamera);
            gameContext.Inject(ui, input, assets, moduleCamera);

            // ==== Phase: Init ====
            ui.Login_OnStartHandle = () => {

                // 关闭 Login
                ui.Login_Close();

                // 进入游戏: 生成怪物、主角、场景
                BusinessGame.Enter(gameContext);

                ui.Bag_Open(100);
                for (int i = 0; i < 10; i += 1) {
                    ui.Bag_Add(i, null, i + 1);
                }

            };

            assets.Load();

            // ==== Phase: Enter Game ====
            ui.Login_Open();
            // BusinessGame.Enter(gameContext);

            Application.targetFrameRate = 120;

        }

        float restDT = 0;
        void Update() {

            // FixedUpdate

            // fps = 120
            // dt = 1 / 120 = 0.0083

            float dt = Time.deltaTime;

            // ==== Phase: Process Input ====
            input.Process(moduleCamera.camera.transform.rotation);

            // ==== Phase: Logic ====
            float fixedDT = Time.fixedDeltaTime; // 0.02
            restDT += dt; // 0.0083 (0.0000000001, 10)
            if (restDT >= fixedDT) {
                while (restDT >= fixedDT) {
                    restDT -= fixedDT;
                    FixedTick(fixedDT);
                }
            } else {
                FixedTick(restDT);
                restDT = 0;
            }

            // ==== Phase: Draw ====
            LateTick(dt);

        }

        void FixedTick(float dt) {

            // ==== Phase: Logic ====
            BusinessGame.FixedTick(gameContext, dt);

            // ==== Phase: Simulate ====
            Physics.Simulate(dt); // rb.position += rb.velocity * dt;

        }

        void LateTick(float dt) {

            // ==== Phase: Draw ====
            BusinessGame.LateTick(gameContext, dt);

        }

        // 当 安卓/iOS 应用程序退出时调用
        void OnApplicationQuit() {
            TearDown();
        }

        void OnDestroy() {
            TearDown();
        }

        // 非官方生命周期
        void TearDown() {

            if (isTearDown) {
                return;
            }
            isTearDown = true;

            assets.Unload();

        }

    }

}
