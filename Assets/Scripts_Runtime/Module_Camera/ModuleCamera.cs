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

    }

}