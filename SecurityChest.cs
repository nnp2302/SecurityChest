using Humanizer;
using SecurityChest.Common.GlobalTiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SecurityChest
{
    public partial class SecurityChest : Mod
    {
        public override void Load()
        {
            if (Main.netMode == NetmodeID.Server)
            {
                Console.WriteLine("Security Chest server loading...");
                if (!Directory.Exists(DATA_PATH))
                {
                    Directory.CreateDirectory(DATA_PATH);
                }
                Tiles.listChestOwner = LoadChestOwner();
                Console.WriteLine("Security Chest server is loaded.");
            }
            base.Load();
        }


    }
}
