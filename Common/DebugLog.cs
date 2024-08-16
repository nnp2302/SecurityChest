using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace SecurityChest.Common
{
    public class DebugLog
    {
        public static void Raise(object message)
        {
            if (SecurityChest.DEBUG_MODE)
            {
                if (Main.netMode == NetmodeID.Server)
                {
                    Console.WriteLine(message.ToString());
                }
                else
                {
                    Main.NewText(message.ToString());
                }
            }
        }

    }
}
