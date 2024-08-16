using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using SecurityChest.Common.GlobalPlayer;
using System.Runtime.InteropServices.JavaScript;
using Terraria.DataStructures;
using System.IO;
using SecurityChest.Common;
using SecurityChest.Common.GlobalTiles;
using static SecurityChest.SecurityChest;
namespace SecurityChest.Helper
{
    public class NetHelper
    {
        public static void PlaceChest_Send(short x, short y, ulong steamID = 0, int ignoreWho = -1)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                DebugLog.Raise("Client Read");
                steamID = Main.LocalPlayer.GetModPlayer<SecurityChestPlayer>().GetSteamId();
                ModPacket packet = ModContent.GetInstance<SecurityChest>().GetPacket();
                packet.Write((byte)MessageType.ChestPlace);
                packet.Write(x);
                packet.Write((short)(y - 1));
                packet.Write(steamID);
                packet.Send();
                return;
            }
            if (Main.netMode == NetmodeID.Server)
            {
                DebugLog.Raise("Server Read");
                ModPacket packet = ModContent.GetInstance<SecurityChest>().GetPacket();
                packet.Write((byte)MessageType.ChestPlace);
                packet.Write(x);
                packet.Write(y);
                packet.Write(steamID);
                packet.Send();
                return;
            }
        }

        public static void PlaceChest_Receive(BinaryReader reader, int whoAmI)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                Point16 dimension = new Point16(reader.ReadInt16(), reader.ReadInt16());
                ulong steamID = reader.ReadUInt64();
                Tiles.listChestOwner.Add(dimension, steamID);
            }
            if (Main.netMode == NetmodeID.Server)
            {
                Point16 dimension = new Point16(reader.ReadInt16(), reader.ReadInt16());
                ulong steamID = reader.ReadUInt64();
                SaveChestOwner(dimension, steamID);
                PlaceChest_Send(dimension.X, dimension.Y, steamID);
            }
        }

        public static void InitChestOwner_Send(int toWho = -1, int ignoreWho = -1)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = ModContent.GetInstance<SecurityChest>().GetPacket();
                packet.Write((byte)MessageType.InitChestOwner);
                packet.Send();
                return;
            }
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket packet = ModContent.GetInstance<SecurityChest>().GetPacket();
                var chestOwner = LoadChestOwner();
                packet.Write((byte)MessageType.InitChestOwner);
                packet.Write(chestOwner.Count);
                foreach (var item in chestOwner)
                {
                    packet.Write(item.Key.X);
                    packet.Write(item.Key.Y);
                    packet.Write(item.Value);
                }
                packet.Send(toWho, ignoreWho);
            }
        }
        public static void InitChestOwner_Receive(BinaryReader reader, int whoAmI)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                InitChestOwner_Send(toWho: whoAmI);
            }
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                int count = reader.ReadInt32();
                Dictionary<Point16, ulong> receivedDictionary = [];
                for (int i = 0; i < count; i++)
                {
                    short x = reader.ReadInt16();
                    short y = reader.ReadInt16();
                    ulong value = reader.ReadUInt64();

                    Point16 point = new Point16(x, y);
                    receivedDictionary[point] = value;
                }
                Tiles.listChestOwner = receivedDictionary;
            }

        }
    }
}
