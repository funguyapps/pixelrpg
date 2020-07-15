using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    class VineLash : IMove
    {
        public override int NumTargets => 1;

        public override MoveTypes MoveType => MoveTypes.offensive;

        public override Stats AffectedStat => Stats.none;

        public override string TextureStr => "Graphics\\Moves\\vinelash";

        public override int CalculateDamage()
        {
            Random rand = new Random();

            float total = AttackStat * (1.0f + (rand.Next(25, 50) / 100.0f));

            return (int)total;
        }

        public override string ToString()
        {
            int min = (int)(AttackStat * 1.25f);
            int max = (int)(AttackStat * 1.5f);

            return "Vine Lash"; //Damage: " + min.ToString() + " - " + max.ToString();
        }

        public override string enemyToString()
        {
            return "Vine Lash Damage: ";
        }

        public override string InfoDamage()
        {
            int min = (int)(AttackStat * 1.25f);
            int max = (int)(AttackStat * 1.5f);

            return min.ToString() + "-" + max.ToString();
        }
    }
}
