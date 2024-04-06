using UnityEngine;

namespace Zelda {

    public static class GameFactory {

        public static RoleEntity Role_Create(ModuleAssets assets, IDService idService, int typeID) {
            bool has = assets.TryGetEntity("Entity_Role", out var go);
            if (!has) {
                Debug.LogError("Entity_Role not found");
                return null;
            }

            go = GameObject.Instantiate(go);
            RoleEntity role = go.GetComponent<RoleEntity>();
            role.id = idService.roleIDRecord++;
            role.Ctor();
            role.Init(16);
            return role;
        }

        public static LootEntity Loot_Create(ModuleAssets assets, IDService idService, int itemTypeID, int itemCount, Vector3 pos) {
            bool has = assets.TryGetEntity("Entity_Loot", out var go);
            if (!has) {
                Debug.LogError("Entity_Loot not found");
                return null;
            }

            go = GameObject.Instantiate(go);
            LootEntity loot = go.GetComponent<LootEntity>();
            loot.Ctor();
            loot.id = idService.lootIDRecord++;
            loot.itemTypeID = itemTypeID;
            loot.itemCount = itemCount;

            loot.SetPos(pos);

            return loot;

        }

    }
}