using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zelda {

    public class ClientMain : MonoBehaviour {

        [SerializeField] Camera mainCamera;

        ModuleInput input;
        ModuleAssets assets;
        ModuleCamera moduleCamera;

        GameContext gameContext;

        void Awake() {

            // ==== Phase: Instantiate ====
            input = new ModuleInput();
            assets = new ModuleAssets();
            moduleCamera = new ModuleCamera();

            gameContext = new GameContext();

            // ==== Phase: Inject ====
            moduleCamera.Inject(mainCamera);
            gameContext.Inject(input, assets, moduleCamera);

            // ==== Phase: Init ====
            assets.Load();

            // ==== Phase: Enter Game ====
            BusinessGame.Enter(gameContext);

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
                while (restDT > 0) {
                    restDT -= fixedDT;
                    FixedTick(fixedDT);
                }
            } else {
                FixedTick(restDT);
                restDT = 0;
            }

            // ==== Phase: Draw ====

        }

        void FixedTick(float dt) {

            // ==== Phase: Logic ====
            BusinessGame.FixedTick(gameContext, dt);
            
            // ==== Phase: Simulate ====
            Physics.Simulate(dt); // rb.position += rb.velocity * dt;

        }

    }

}
