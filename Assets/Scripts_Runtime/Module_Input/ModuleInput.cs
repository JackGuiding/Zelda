using UnityEngine;

namespace Zelda {

    public class ModuleInput {

        Vector2 moveAxis; // 按键方向, 常用于移动
        public Vector3 moveCameraDir;
        public bool isAttack;
        public bool isJump;

        public bool isUIToggleBag;

        public Vector2 cameraRotationAxis;

        public ModuleInput() { }

        public void Process(Quaternion cameraRotation) {

            // ==== 角色移动 ====
            Vector2 moveAxis = Vector2.zero;
            if (Input.GetKey(KeyCode.W)) {
                moveAxis.y = 1;
            } else if (Input.GetKey(KeyCode.S)) {
                moveAxis.y = -1;
            }

            if (Input.GetKey(KeyCode.A)) {
                moveAxis.x = -1;
            } else if (Input.GetKey(KeyCode.D)) {
                moveAxis.x = 1;
            }
            this.moveAxis = moveAxis;

            // 四元数 * Vector3 = 旋转后的 Vector3
            // 相机面向
            moveCameraDir = cameraRotation * new Vector3(moveAxis.x, 0, moveAxis.y);

            // ==== 角色攻击 ====
            isAttack = Input.GetKeyDown(KeyCode.F);

            // ==== 角色跳跃 ====
            isJump = Input.GetKeyDown(KeyCode.Space);

            // ==== 相机旋转 ====
            const int mouseRightButton = 1;
            if (Input.GetMouseButton(mouseRightButton)) {
                // delta = 当前帧坐标 - 上一帧坐标
                cameraRotationAxis = Input.mousePositionDelta;
            }

            // ==== UI: Bag ====
            isUIToggleBag = Input.GetKeyDown(KeyCode.B);

        }

    }

}