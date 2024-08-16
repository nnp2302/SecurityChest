using SecurityChest.Common.GlobalTiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using static SecurityChest.SecurityChest;
namespace SecurityChest.Common.GlobalPlayer
{
    partial class SecurityChestPlayer
    {
        private enum CurrentChestIs : byte
        {
            None,
            NormalChest,
            StorageHeart,
            StorageCraft
        }
        private void PreOpenMagicStorage()
        {
            ulong steamID = 0;
            Point16 storage = (Point16)MAGIC_STORAGE_View_Storage.Invoke(MAGIC_STORAGE_PLAYER, null);
            if (storage.X < 0 || storage.Y < 0)
                return;
            Tile tile = Main.tile[storage.X, storage.Y];
            if (!tile.HasTile)
                return;
            ModTileEntity storageHeart = (ModTileEntity)MAGIC_STORAGE_Get_Storage_Heart.Invoke(MAGIC_STORAGE_PLAYER, null);
            if (storageHeart != null)
            {
                Point16 position = new Point16(storageHeart.Position.X + 1, storageHeart.Position.Y);
                DebugLog.Raise(position.ToString());
                if (Tiles.listChestOwner.TryGetValue(position, out steamID))
                {
                    if (GetSteamId() == steamID)
                    {

                    }
                    else
                    {
                        
                        Main.NewText("Can not access to this Storage, you are not owner or approval.", 255, 0, 0);
                        Main.LocalPlayer.tileEntityAnchor.Clear();
                        MAGIC_STORAGE_Close_Storage.Invoke(MAGIC_STORAGE_PLAYER, null);

                    }
                }        
            }

        }
        private void PreOpenChest()
        {
            var player = Main.LocalPlayer;
            short x = (short)player.chestX;
            short y = (short)player.chestY;
            Point16 dimension = new Point16(x, y);
            ulong steamID = 0;

            if (Tiles.listChestOwner.TryGetValue(dimension, out steamID))
            {
                if (GetSteamId() == steamID)
                {

                }
                else
                {
                    Main.NewText("Can not access to this Chest, you are not owner or approval.", 255, 0, 0);
                    player.tileEntityAnchor.Clear();
                    player.chest = -1;
                    player.chestX = -1;
                    player.chestY = -1;
                }
            }
        }
        private CurrentChestIs CheckIsOpen()
        {
            if (MAGIC_STORAGE_EXIST)
            {
                Point16 storage = (Point16)MAGIC_STORAGE_View_Storage.Invoke(MAGIC_STORAGE_PLAYER, null);
                //DebugLog.Raise("Exist: "+storage.ToString());
                if (storage != Point16.NegativeOne)
                {
                    return CurrentChestIs.StorageHeart;
                }
            }
            if (Main.LocalPlayer.chest != -1)
            {
                return CurrentChestIs.NormalChest;
            }

            return CurrentChestIs.None;
        }

    }
}
