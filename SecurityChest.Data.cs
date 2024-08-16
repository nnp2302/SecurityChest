using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace SecurityChest
{
    partial class SecurityChest
    {
        private static string prefix = "SecurityChest_Config_";
        public static string DATA_PATH = Path.Combine(Main.SavePath, "ModConfigs");
        public static string CHEST_OWNER = prefix + "chest_owner.json";
        public static string APPROVAL_LIST = prefix + "approval_list.json";
    }
}
