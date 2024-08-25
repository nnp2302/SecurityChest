using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using SecurityChest.Common.GlobalTiles;
using SecurityChest.Common;
using SecurityChest.Helper;

namespace SecurityChest
{
    partial class SecurityChest
    {
        internal enum MessageType : byte
        {
            ChestPlace,
            ChestDestroy,
            ChestOpen,
            RequestPlayerPlaceChest,
            RequestPlayerWhoApprovalToUse,
            SetPrivate,
            SetOnlyApproval,
            SetEveryOne,
            AddApprovalPerson,
            RemoveApprovalPerson,
            InitChestOwner
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType)reader.ReadByte();

            switch (msgType)
            {
                case MessageType.ChestPlace:
                    NetHelper.PlaceChest_Receive(reader, whoAmI);
                    break;
                case MessageType.InitChestOwner:
                    NetHelper.InitChestOwner_Receive(reader, whoAmI);
                    break;
                case MessageType.ChestDestroy:
                    break;
                case MessageType.ChestOpen:
                    break;
                case MessageType.RequestPlayerPlaceChest:
                    break;
                case MessageType.RequestPlayerWhoApprovalToUse:
                    break;
                case MessageType.SetPrivate:
                    break;
                case MessageType.SetOnlyApproval:
                    break;
                case MessageType.SetEveryOne:
                    break;
                case MessageType.AddApprovalPerson:
                    break;
                case MessageType.RemoveApprovalPerson:
                    break;
                default:
                    break;
            }
            //base.HandlePacket(reader, whoAmI);
        }
        public static void SaveChestOwner(Point16 dimension, ulong player)
        {
            try
            {
                string path = Path.Combine(DATA_PATH, CHEST_OWNER);
                Dictionary<Point16, ulong> data = LoadChestOwner();
                ulong temp;
                if (data.TryGetValue(dimension, out temp))
                {
                    data[dimension] = player;
                }
                else
                {
                    data.Add(dimension, player);
                }

                File.WriteAllText(path, JsonConvert.SerializeObject(data));
            }
            catch (Exception)
            {

            }

        }

        public static Dictionary<Point16, ulong> LoadChestOwner()
        {
            Dictionary<Point16, ulong> data = [];
            string path = Path.Combine(DATA_PATH, CHEST_OWNER);
            if (File.Exists(path))
            {
                var temp = JsonConvert.DeserializeObject<Dictionary<string, ulong>>(File.ReadAllText(path));
                foreach (var item in temp)
                {
                    string point = item.Key[1..^1].Replace(" ", "");
                    string[] array = point.Split(',');
                    short x = short.Parse(array[0]);
                    short y = short.Parse(array[1]);
                    data.Add(new(x, y), item.Value);

                }
            }
            return data;
        }
        public static ulong FindChestOwner(Point16 dimension)
        {
            Dictionary<Point16, ulong> data = LoadChestOwner();
            ulong steamID = 0;
            data.TryGetValue(dimension, out steamID);
            return steamID;
        }
    }
}
