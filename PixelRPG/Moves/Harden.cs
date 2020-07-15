using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    class Harden : IMove
    {
        public override int NumTargets => 1;

        public override MoveTypes MoveType => MoveTypes.defensive;

        public override Stats AffectedStat => Stats.defense;

        public override string TextureStr => "Graphics\\Moves\\stat_increase";

        public override int CalculateDamage()
        {
            return AttackStat;
        }

        public override string enemyToString()
        {
            //return "Harden increased defense by: ";
            return "Harden";
        }

        public override string ToString()
        {
            return "Harden";
        }

        public override string InfoDamage()
        {
            return "dfs up " + AttackStat.ToString();
        }
    }
}
