using System;
using UnityEngine;

namespace Zelda {

    // 类 = 蓝图 = 设计图纸
    // 水杯的图纸不能装水
    // 水杯可以装水
    // 水杯是通过设计图纸设计出来的, 术语称水杯为"实例", 过程(new)称为"实例化"
    public class RoleEntity : MonoBehaviour {

        public int id;

        public float hp;
        public float hpMax;

        [SerializeField] Rigidbody rb;
        [SerializeField] Transform bodyTF;
        [SerializeField] Animator anim;

        [SerializeField] public float speed;

        Vector3 oldForward;

        float time;
        Vector3 startForward;
        Vector3 endForward;
        float duration;

        public bool isGrounded;

        public Action<RoleEntity, Collision> OnCollisionEnterHandle;

        public void Ctor() { }

        public void Move(Vector3 moveAxis, float dt) {
            Move(moveAxis, speed, dt);

            // Animation
            if (moveAxis != Vector3.zero) {
                anim.SetFloat("F_MoveSpeed", rb.velocity.magnitude);
            } else {
                anim.SetFloat("F_MoveSpeed", 0);
            }
        }

        public void Jump(bool isJumpKeyDown) {
            if (isJumpKeyDown && isGrounded) {
                Vector3 velo = rb.velocity;
                velo.y = 5.5f;
                rb.velocity = velo;
                isGrounded = false;
            }
        }

        public void SetGround(bool isGrounded) {
            this.isGrounded = isGrounded;
        }

        public void Anim_Attack() {
            anim.SetTrigger("T_Attack");
        }

        public void Face(Vector3 moveAxis, float dt) {

            if (moveAxis == Vector3.zero) {
                return;
            }

            // 根据正面进行旋转
            // old forward: (x0, y0, z1)
            // new forward: (moveAxis.x, 0, moveAxis.y)
            Vector3 newForward = new Vector3(moveAxis.x, 0, moveAxis.z).normalized;
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

        public void Move(Vector3 moveAxis, float speed, float dt) {

            Vector3 velo = rb.velocity;
            float oldY = velo.y;

            Vector3 moveDir = new Vector3(moveAxis.x, 0, moveAxis.z);
            moveDir.Normalize();

            // velocity 同时表示速度量和方向
            velo = moveDir * speed;
            velo.y = oldY;
            rb.velocity = velo;

        }

        // ==== 只会触发一种, 要么硬要么软 ====
        // 硬碰撞
        // void OnCollisionEnter2D(Collision2D other) {} // 2D 版本

        void OnCollisionEnter(Collision other) {
            // Debug.Log("OnCollisionEnter");
            OnCollisionEnterHandle.Invoke(this, other);
        }

        void OnCollisionStay(Collision other) {
            // Debug.Log("OnCollisionStay");
        }

        void OnCollisionExit(Collision other) {
            // Debug.Log("OnCollisionExit");
        }

        // 软交叉
        // void OnTriggerEnter2D(Collider2D other) {} // 2D 版本

        void OnTriggerEnter(Collider other) {
            // Debug.Log("OnTriggerEnter");
        }

        void OnTriggerStay(Collider other) {
            // Debug.Log("OnTriggerStay");
        }

        void OnTriggerExit(Collider other) {
            // Debug.Log("OnTriggerExit");
        }

    }

}