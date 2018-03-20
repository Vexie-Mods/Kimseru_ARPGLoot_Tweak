using Microsoft.Xna.Framework;
using System.Text;
using Terraria;
using Terraria.ModLoader;

namespace ARPGLoot
{
    class ARPGLoot : Mod
    {
        public ARPGLoot()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadSounds = true
            };
        }

        // I like to make debug print functions
        public static void DBPrint(object obj1, params object[] obj2)
        {
            StringBuilder sb = new StringBuilder(obj1.ToString());

            for(int i = 0; i < obj2.Length; i++)
            {
                sb.Append(" | ").Append(obj2[i].ToString());
            }

            Main.NewText(sb.ToString(), Color.Yellow);
        }
    }
}
