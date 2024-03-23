namespace Zelda {

    public class GameContext {

        public int ownerRoleID;

        public ModuleAssets assets;
        public ModuleInput input;

        public RoleRepository roleRepository;

        public GameContext() {
            roleRepository = new RoleRepository();
        }

        public void Inject(ModuleInput input, ModuleAssets assets) {
            this.input = input;
            this.assets = assets;
        }
    }
}