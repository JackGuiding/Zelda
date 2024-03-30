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

            // 旋转角度测试
            // Vector3 right = Vector3.right;
            // Vector3 up = Vector3.up;
            // float ruAngle = Vector3.SignedAngle(right, up, Vector3.forward);
            // float urAngle = Vector3.SignedAngle(up, right, Vector3.forward);
            // Debug.Log($"ruAngle: {ruAngle}, urAngle: {urAngle}");

            {
                Vector3 d = new Vector3(100, 100, 100).normalized;
                Debug.Log($"A d: {d}");

                Quaternion q = Quaternion.Euler(40, 40, 0);
                Debug.Log($"A d1: {q * d}");

                Quaternion q1 = Quaternion.Euler(40, 0, 0);
                Debug.Log($"A d2: {q * q1 * d}");

                Quaternion q2 = Quaternion.Euler(0, 40, 0);
                Debug.Log($"A d3: {q * q1 * q2 * d}");

                d = q * q1 * q2 * d;
                Debug.Log($"A res: {d}");
            }

            {
                Vector3 d = new Vector3(100, 100, 100).normalized;
                Debug.Log($"B d: {d}");

                Vector3 d1 = Quaternion.Euler(40, 40, 0) * d;
                Debug.Log($"B d1: {d1}");

                Vector3 d2 = Quaternion.Euler(0, 40, 0) * d1;
                Debug.Log($"B d2: {d2}");

                d = Quaternion.Euler(40, 0, 0) * d2;
                Debug.Log($"res 2: {d}");
            }

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

    }

}
