namespace Zelda {

    public class GameContext {

        public int ownerRoleID;

        public AppUI ui;
        public ModuleAssets assets;
        public ModuleInput input;
        public ModuleCamera moduleCamera;

        public RoleRepository roleRepository;
        public LootRepository lootRepository;

        public IDService idService;

        public GameContext() {
            roleRepository = new RoleRepository();
            lootRepository = new LootRepository();
            idService = new IDService();
        }

        public void Inject(AppUI ui, ModuleInput input, ModuleAssets assets, ModuleCamera moduleCamera) {
            this.ui = ui;
            this.input = input;
            this.assets = assets;
            this.moduleCamera = moduleCamera;
        }
    }
}