using SecurityChest.Common.GlobalPlayer;
using SecurityChest.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SecurityChest.Common.GlobalTiles
{
    partial class Tiles
    {
        public override void PlaceInWorld(int i, int j, int type, Item item)
        {
            //Nếu đặt một rương hoặc magic storage (nếu có) 
            if (type == TileID.Containers
                || type == TileID.Containers2
                || (SecurityChest.MAGIC_STORAGE_EXIST
                    && item.ModItem.Type == SecurityChest.MAGIC_STORAGE_HEART_TYPE
                    )
                )
            {
                NetHelper.PlaceChest_Send((short)i, (short)j);
            }
            base.PlaceInWorld(i, j, type, item);
        }
    }
}
