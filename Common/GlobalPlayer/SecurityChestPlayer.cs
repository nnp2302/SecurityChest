using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static SecurityChest.SecurityChest;
using Terraria.DataStructures;
using SecurityChest.Common.GlobalTiles;
using System.Reflection;
using SecurityChest.Helper;

namespace SecurityChest.Common.GlobalPlayer
{
    public partial class SecurityChestPlayer : ModPlayer
    {
        public ulong GetSteamId()
        {
            return Steamworks.SteamUser.GetSteamID().m_SteamID;
        }
        public override void OnEnterWorld()
        {
            NetHelper.InitChestOwner_Send();
            InitMagicStorage();
            InitSecurityChest();

            base.OnEnterWorld();
        }
        public override void PreUpdate()
        {
            base.PreUpdate();
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                if (CheckIsOpen() == CurrentChestIs.NormalChest)
                {
                    PreOpenChest();
                }
                if (CheckIsOpen() == CurrentChestIs.StorageHeart)
                {
                    PreOpenMagicStorage();
                }
            }

        }
    }
}
