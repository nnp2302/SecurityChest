using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;
using System.IO;

namespace SecurityChest.Common.GlobalTiles
{
    public class GlobalChestTiles : GlobalTile
    {

        public static readonly Dictionary<Point16, int> placedTileData = new Dictionary<Point16, int>();

        public override void RightClick(int i, int j, int type)
        {
            base.RightClick(i, j, type);
            //Chest
            if (type == TileID.Containers || type == TileID.Containers2)
            {
                // Example: Display a message when the chest is right-clicked
                Player player = Main.LocalPlayer;
                Main.NewText(player.name + " is open chest.", 255, 240, 20);

                // Additional logic or modifications can go here...
                // For example, checking who placed the chest or adding custom functionality.
                Mod magicStorageMod = ModLoader.GetMod("MagicStorage");
                if (magicStorageMod != null)
                {
                    Main.NewText("loaded");
                }
            }
        }

        public override void PlaceInWorld(int i, int j, int type, Item item)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                placedTileData[new Point16(i, j)] = Main.myPlayer;   
            }
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                Main.NewText("Client send");
                int player = Main.myPlayer;
                ModPacket packet = ModContent.GetInstance<SecurityChest>().GetPacket();
                packet.Write(i);
                packet.Write(j);
                packet.Write(player);
                packet.Send(ignoreClient:Main.myPlayer);
            }
            base.PlaceInWorld(i, j, type, item);
        }

        // Get the player ID who placed a specific tile
        public static int GetPlacingPlayerID(int i, int j)
        {
            Point16 point = new Point16(i, j);
            if (placedTileData.TryGetValue(point, out int playerID))
            {
                return playerID;
            }
            return -1; // Return -1 if no data is available
        }
    }

    public enum MessageType : byte
    {
        TileData
    }
}
