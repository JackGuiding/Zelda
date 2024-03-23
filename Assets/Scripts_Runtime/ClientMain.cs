using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zelda {

    public class ClientMain : MonoBehaviour {

        ModuleInput input;

        [SerializeField] RoleEntity role;

        void Awake() {

            // ==== Phase: Instantiate ====
            input = new ModuleInput();

            // ==== Phase: Inject ====

            // ==== Phase: Init ====

            // ==== Phase: Enter Game ====

            Debug.Log("Hello");

            Application.targetFrameRate = 120;

        }

        float restDT = 0;
        void Update() {

            // FixedUpdate

            // fps = 120
            // dt = 1 / 120 = 0.0083

            float dt = Time.deltaTime;

            // ==== Phase: Process Input ====
            input.Process();

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
            role.Move(input.moveAxis, dt);
            role.Face(input.moveAxis, dt);
            // 通过射线检测地面
            RaycastHit[] hits = Physics.RaycastAll(role.transform.position + Vector3.up, Vector3.down, 1.05f);
            Debug.DrawRay(role.transform.position + Vector3.up, Vector3.down * 1.05f, Color.red);
            Debug.Log(role.isGrounded + " " + hits.Length);
            if (hits != null) {
                for (int i = 0; i < hits.Length; i += 1) {
                    var hit = hits[i];
                    if (hit.collider.CompareTag("Ground")) {
                        role.SetGround(true);
                        break;
                    }
                }
            }
            role.Jump(input.isJump);
            if (input.isAttack) {
                role.Anim_Attack();
            }

            // ==== Phase: Simulate ====
            Physics.Simulate(dt); // rb.position += rb.velocity * dt;

        }

    }

}
