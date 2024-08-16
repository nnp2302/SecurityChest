using SecurityChest.Common.GlobalPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using static SecurityChest.SecurityChest;

namespace SecurityChest.Common.GlobalTiles
{
    public partial class Tiles : GlobalTile
    {
        public static Dictionary<Point16, ulong> listChestOwner = [];
        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        {
            ulong steamID = 0;
            if (Main.tile[i, j].TileFrameX > 0)
                i--;
            if (Main.tile[i, j].TileFrameY > 0)
                j--;
            TileEntity te;
            if (TileEntity.ByPosition.TryGetValue(new Point16(i, j), out te))
            {
                Point16 position = new Point16(te.Position.X + 1, te.Position.Y);
                DebugLog.Raise(position.ToString());
                if (Tiles.listChestOwner.TryGetValue(position, out steamID))
                {
                    ulong steam = Main.LocalPlayer.GetModPlayer<SecurityChestPlayer>().GetSteamId();
                    if (steam == steamID)
                    {

                    }
                    else
                    {
                        Main.NewText("Can not destroy this Storage, you are not owner.", 255, 0, 0);
                        return false;
                    }
                }
            }


            return base.CanKillTile(i, j, type, ref blockDamaged);
        }
    }
}
