using UnityEngine;

namespace Zelda {

    public class ModuleCamera {

        public Camera camera;

        public ModuleCamera() { }

        public void Inject(Camera camera) {
            this.camera = camera;
        }

        public void Follow(Vector3 targetPos, float height, float radius) {
            camera.transform.position = new Vector3(targetPos.x, targetPos.y + height, targetPos.z - radius);
        }

        public void Rotate(Vector2 axis, float dt) {

            // ==== 方案一: 四元数 ====
            // RotateByQuaternion(axis, dt);

            // ==== 方案二: 欧拉角 ====
            RotateByEuler(axis, dt);

        }

        void RotateByQuaternion(Vector2 axis, float dt) {

            float sensitivity = 60f * dt;

            // 旋转
            // 其中一个可以绕绝对的方向旋转，另一个可以绕相对的方向旋转
            Quaternion originRotation = camera.transform.rotation;

            // 绕X轴的旋转
            Quaternion xRotation = Quaternion.AngleAxis(-axis.y * sensitivity, Vector3.right);

            // 绕Y轴的旋转
            Quaternion yRotation = Quaternion.AngleAxis(axis.x * sensitivity, Vector3.up);

            // 原四元数 * 旋转增量四元数 = 旋转后的四元数
            originRotation = originRotation * xRotation;
            originRotation = originRotation * yRotation;

            camera.transform.rotation = originRotation;
        }

        void RotateByEuler(Vector2 axis, float dt) {
            float sensitivity = 60f * dt;
            Quaternion originRotation = camera.transform.rotation;
            Vector3 originEuler = originRotation.eulerAngles;
            originEuler += new Vector3(-axis.y * sensitivity, axis.x * sensitivity, 0);
            camera.transform.eulerAngles = originEuler;
        }

    }

}