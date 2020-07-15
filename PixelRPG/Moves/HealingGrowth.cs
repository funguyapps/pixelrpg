using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class HealingGrowth : IMove
    {
        public override int NumTargets => 1;

        public override MoveTypes MoveType => MoveTypes.defensive;

        public override Stats AffectedStat => Stats.health;

        public override string TextureStr => "Graphics\\Moves\\stat_increase";

        public override int CalculateDamage()
        {
            return AttackStat / 4;
        }

        public override string enemyToString()
        {
            //return "Enrage increased atk by: ";
            return "Healing Growth";
        }

        public override string ToString()
        {
            return "Healing Growth"; //Increase by: " + (AttackStat / 4).ToString();
        }

        public override string InfoDamage()
        {
            return "hp up " + (AttackStat / 4).ToString();
        }
    }
}
