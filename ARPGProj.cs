using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ARPGLoot
{
    public class ARPGProj : GlobalProjectile
    {
        private string onHit;
        private int onHitChance = 0;

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public override void SetDefaults(Projectile projectile)
        {
            // Swinging the item
            if (ARPGPlayer.swingingCheck && ARPGPlayer.swingingItem != null)
            {
                this.onHit = ARPGPlayer.swingingItem.GetGlobalItem<ARPGItem>().onHit;
                this.onHitChance = ARPGPlayer.swingingItem.GetGlobalItem<ARPGItem>().onHitChance;
            }

            // Came from another projectile
            else if (Main.ProjectileUpdateLoopIndex >= 0)
            {
                Projectile proj = Main.projectile[Main.ProjectileUpdateLoopIndex];
                if (proj.active && proj.owner == Main.myPlayer)
                {
                    this.onHit = proj.GetGlobalProjectile<ARPGProj>().onHit;
                    this.onHitChance = proj.GetGlobalProjectile<ARPGProj>().onHitChance;
                }
            }
            else
            {
                this.onHit = "";
            }
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (onHit == "")
                return;

            Player player = Main.player[projectile.owner];

            int rando = Main.rand.Next(0, 100) + 1;

            //ARPGLoot.DBPrint("HIt NPC OnHit " + onHit, " OnHitChance " + onHitChance, "rando " + rando);

            if (rando <= onHitChance)
            {
                if (onHit.Equals("Fire"))
                    target.AddBuff(BuffID.OnFire, 5 * 60);
                if (onHit.Equals("Frostburn"))
                    target.AddBuff(BuffID.Frostburn, 5 * 60);
                if (onHit.Equals("Curse"))
                    target.AddBuff(BuffID.CursedInferno, 5 * 60);
                if (onHit.Equals("Leech") && target.type != 488 && player.statLife <= player.statLifeMax2)
                {
                    player.HealEffect(3, true);
                    player.statLife += 3;
                }
            }
            //onHit = "";
        }


        
        public override void ModifyHitPvp(Projectile projectile, Player target, ref int damage, ref bool crit)
        {
            Player player = Main.player[projectile.owner];

            if (Main.rand.Next(0, 100) + 1 <= onHitChance)
            {
                if (onHit.Equals("Fire"))
                    target.AddBuff(BuffID.OnFire, 5 * 60);
                if (onHit.Equals("Frostburn"))
                    target.AddBuff(BuffID.Frostburn, 5 * 60);
                if (onHit.Equals("Curse"))
                    target.AddBuff(BuffID.CursedInferno, 5 * 60);
                if (onHit.Equals("Leech") && player.statLife <= player.statLifeMax2)
                {
                    player.HealEffect(3, true);
                    player.statLife += 3;
                }
            }
            onHit = "";
        }
    }
}
