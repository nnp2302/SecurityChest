using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SecurityChest
{
    partial class SecurityChest
    {
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                int i = reader.ReadInt32();
                int j = reader.ReadInt32();
                int placingPlayerID = reader.ReadInt32();
                ModPacket packet = GetPacket();
                packet.Write("helllo");
                packet.Send(ignoreClient:255);

                Logger.Info($"(Client) Received placement data: Tile at ({i}, {j}) placed by player ID: {placingPlayerID}");
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                string s = reader.ReadString();
                Main.NewText("R F S" + s);
            }
            base.HandlePacket(reader, whoAmI);
        }
    }
}
