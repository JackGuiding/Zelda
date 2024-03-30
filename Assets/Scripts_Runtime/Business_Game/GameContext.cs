namespace Zelda {

    public class GameContext {

        public int ownerRoleID;

        public ModuleAssets assets;
        public ModuleInput input;
        public ModuleCamera moduleCamera;

        public RoleRepository roleRepository;

        public GameContext() {
            roleRepository = new RoleRepository();
        }

        public void Inject(ModuleInput input, ModuleAssets assets, ModuleCamera moduleCamera) {
            this.input = input;
            this.assets = assets;
            this.moduleCamera = moduleCamera;
        }
    }
}