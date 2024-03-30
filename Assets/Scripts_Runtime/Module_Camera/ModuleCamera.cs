using UnityEngine;

namespace Zelda {

    public class ModuleCamera {

        public Camera camera;

        public ModuleCamera() { }

        public void Inject(Camera camera) {
            this.camera = camera;
        }

    }

}