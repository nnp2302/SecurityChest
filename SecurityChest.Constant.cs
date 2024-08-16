using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace SecurityChest
{
    partial class SecurityChest
    {
        public static bool DEBUG_MODE = false;
        //
        //Magic storage constants.
        //
        public static bool MAGIC_STORAGE_EXIST = false;
        public static int MAGIC_STORAGE_HEART_TYPE;
        public static int MAGIC_STORAGE_CRAFT_TYPE;
        public static ModPlayer MAGIC_STORAGE_PLAYER;
        public static MethodInfo MAGIC_STORAGE_Close_Storage;
        public static MethodInfo MAGIC_STORAGE_View_Storage;
        public static MethodInfo MAGIC_STORAGE_Get_Storage_Heart;

        public static ModTileEntity MAGIC_STORAGE_Storage_Heart;
        //
    }
}
