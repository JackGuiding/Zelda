using System;
using UnityEngine;

namespace Zelda {

    public class RoleEntity : MonoBehaviour {

        [SerializeField] Rigidbody rb;
        [SerializeField] Transform bodyTF;

        [SerializeField] public float speed;

        Vector3 oldForward;

        float time;
        Vector3 startForward;
        Vector3 endForward;
        float duration;

        public void Ctor() { }

        public void Move(Vector2 moveAxis, float dt) {
            Move(moveAxis, speed, dt);
        }

        public void Face(Vector2 moveAxis, float dt) {

            if (moveAxis == Vector2.zero) {
                return;
            }

            // 根据正面进行旋转
            // old forward: (x0, y0, z1)
            // new forward: (moveAxis.x, 0, moveAxis.y)
            Vector3 newForward = new Vector3(moveAxis.x, 0, moveAxis.y).normalized;
            if (oldForward != newForward) {
                startForward = oldForward; // 缓动开始
                if (startForward == Vector3.zero) {
                    startForward = transform.forward;
                }
                endForward = newForward; // 缓动结束
                time = 0;
                duration = 0.25f;
                oldForward = newForward;
            }
            // transform.rotation = Quaternion.LookRotation(newForward);

            // 硬转
            // transform.forward = newForward;

            // 平滑转
            if (time <= duration) {
                time += dt;
                float t = time / duration;
                Quaternion startRot = Quaternion.LookRotation(startForward);
                Quaternion endRot = Quaternion.LookRotation(endForward);
                transform.rotation = Quaternion.Lerp(startRot, endRot, t);
            }

        }

        public void Move(Vector2 inputAxis, float speed, float dt) {

            Vector3 moveDir = new Vector3(inputAxis.x, 0, inputAxis.y);
            moveDir.Normalize();

            // velocity 同时表示速度量和方向
            rb.velocity = moveDir * speed;

        }

    }

}