using UnityEngine;

namespace Zelda {

    public static class GameFactory {

        public static RoleEntity Role_Create(ModuleAssets assets, int typeID) {
            bool has = assets.TryGetEntity("Entity_Role", out var go);
            if (!has) {
                Debug.LogError("Entity_Role not found");
                return null;
            }

            go = GameObject.Instantiate(go);
            RoleEntity role = go.GetComponent<RoleEntity>();
            role.Ctor();
            role.Init(16);
            return role;
        }

    }
}