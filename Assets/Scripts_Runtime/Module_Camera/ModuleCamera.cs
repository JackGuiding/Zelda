using UnityEngine;

namespace Zelda {

    public class ModuleCamera {

        public Camera camera;

        public ModuleCamera() { }

        public void Inject(Camera camera) {
            this.camera = camera;
        }

        // 跟随
        public void Follow(Vector3 targetPos, Vector2 followOffset, float radius) {
            camera.transform.position = new Vector3(targetPos.x + followOffset.x, targetPos.y + followOffset.y, targetPos.z - radius);
        }

        // 看向
        public void LookAt(Vector3 targetPos) {
            Vector3 dir = targetPos - camera.transform.position;
            camera.transform.forward = dir.normalized;
        }

        // 绕
        public void Round(Vector3 targetPos, Vector2 rotateAxis, Vector2 followOffset, float radius, float dt) {

            float sensitivity = 60f * dt;
            rotateAxis *= sensitivity;

            Vector3 targetToCameraDir = camera.transform.position - targetPos;
            targetToCameraDir.Normalize();

            Quaternion oldRot = Quaternion.Euler(targetToCameraDir);
            Vector3 oldEuler = camera.transform.eulerAngles;

            // 向量 * 四元数 = 旋转后的向量
            // 旋转后的向量 * 四元数 = 旋转后的向量

            // 四元数 * 四元数 = 四元数
            // 四元数 * 四元数 = 四元数
            // 四元数 * 向量 = 旋转后的向量

            // 绕 X 轴旋转
            Quaternion newRot = oldRot;
            float targetXEuler = oldEuler.x + rotateAxis.y;
            if (targetXEuler > 0 && targetXEuler < 90) {
                Quaternion xRot = Quaternion.AngleAxis(rotateAxis.y, camera.transform.right);
                targetToCameraDir = xRot * targetToCameraDir;
            }

            // 绕 Y 轴旋转
            Quaternion yRot = Quaternion.AngleAxis(rotateAxis.x, Vector3.up);
            targetToCameraDir = yRot * targetToCameraDir;

            Vector3 newDir = newRot * targetToCameraDir;
            newDir.Normalize();
            newDir *= radius;

            camera.transform.position = targetPos + newDir;
            camera.transform.forward = (targetPos - camera.transform.position).normalized;

        }

        public void OldRound(Vector3 targetPos, Vector2 rotateAxis, Vector2 followOffset, float radius, float dt) {

            float sensitivity = 60f * dt;
            rotateAxis *= sensitivity;

            Vector3 targetToCameraDir = camera.transform.position - targetPos;
            targetToCameraDir.Normalize();

            Quaternion oldRot = Quaternion.Euler(targetToCameraDir);
            Vector3 oldEuler = oldRot.eulerAngles;

            // 绕 X 轴旋转
            Quaternion newRot = oldRot;
            float targetXEuler = oldEuler.x + rotateAxis.y;
            if (targetXEuler > 0 && targetXEuler < 90) {
                Quaternion xRot = Quaternion.AngleAxis(rotateAxis.y, Vector3.right);
                newRot = xRot * newRot;
            }

            // 绕 Y 轴旋转
            Quaternion yRot = Quaternion.AngleAxis(rotateAxis.x, Vector3.up);
            newRot = yRot * newRot;

            Vector3 newDir = newRot * targetToCameraDir;
            newDir.Normalize();
            newDir *= radius;

            camera.transform.position = targetPos + newDir;
            camera.transform.forward = (targetPos - camera.transform.position).normalized;

        }

        public void Rotate(Vector2 axis, float dt) {

            // ==== 方案一: 四元数 ====
            RotateByQuaternion(axis, dt);

            // ==== 方案二: 欧拉角 ====
            // RotateByEuler(axis, dt);

        }

        void RotateByQuaternion(Vector2 axis, float dt) {

            float sensitivity = 60f * dt;

            // 旋转
            // 其中一个可以绕绝对的方向旋转，另一个可以绕相对的方向旋转
            Vector3 fwd = camera.transform.forward;

            // 绕X轴的旋转
            Quaternion xRotation = Quaternion.AngleAxis(-axis.y * sensitivity, Vector3.right);

            // 绕Y轴的旋转
            Quaternion yRotation = Quaternion.AngleAxis(axis.x * sensitivity, Vector3.up);

            // 原四元数 * 旋转增量四元数 = 旋转后的四元数
            fwd = xRotation * fwd;
            fwd = yRotation * fwd;

            camera.transform.forward = fwd;
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