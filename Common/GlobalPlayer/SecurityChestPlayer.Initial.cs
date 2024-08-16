using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static SecurityChest.SecurityChest;
using System.Reflection;

namespace SecurityChest.Common.GlobalPlayer
{
    partial class SecurityChestPlayer
    {
        private void InitSecurityChest()
        {

        }
        private void InitMagicStorage()
        {
            MAGIC_STORAGE_EXIST = ModLoader.HasMod("MagicStorage");
            if (MAGIC_STORAGE_EXIST)
            {
                Mod magicStorageMod = ModLoader.GetMod("MagicStorage");
                MAGIC_STORAGE_HEART_TYPE = magicStorageMod.Find<ModItem>("StorageHeart").Type;
                MAGIC_STORAGE_PLAYER = magicStorageMod.Find<ModPlayer>("StoragePlayer");
                PropertyInfo propertyInfo = MAGIC_STORAGE_PLAYER.GetType().GetProperty("LocalPlayer");
                MAGIC_STORAGE_PLAYER = (ModPlayer)propertyInfo.GetValue(MAGIC_STORAGE_PLAYER, null);


                MAGIC_STORAGE_Storage_Heart = magicStorageMod.Find<ModTileEntity>("TEStorageHeart");
                MAGIC_STORAGE_Get_Storage_Heart = MAGIC_STORAGE_PLAYER.GetType().GetMethod("GetStorageHeart");
                MAGIC_STORAGE_Close_Storage = MAGIC_STORAGE_PLAYER.GetType().GetMethod("CloseStorage");
                MAGIC_STORAGE_View_Storage = MAGIC_STORAGE_PLAYER.GetType().GetMethod("ViewingStorage");
            }
        }
    }
}
