using UnityEngine;

namespace Zelda {

    public class ModuleInput {

        public Vector2 moveAxis; // 按键方向, 常用于移动
        public bool isAttack;
        public bool isJump;

        public ModuleInput() { }

        public void Process() {

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

            isAttack = Input.GetKeyDown(KeyCode.F);

            isJump = Input.GetKeyDown(KeyCode.Space);

        }

    }

}