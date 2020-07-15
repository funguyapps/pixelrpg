using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Tornado : IMove
    {
        public override int NumTargets => 1;

        public override MoveTypes MoveType => MoveTypes.defensive;

        public override Stats AffectedStat => Stats.speed;

        public override string TextureStr => "Graphics\\Moves\\stat_increase";

        public override int CalculateDamage()
        {
            return AttackStat / 2;
        }

        public override string enemyToString()
        {
            //return "Enrage increased atk by: ";
            return "Tornado";
        }

        public override string ToString()
        {
            return "Tornado"; //Increase by: " + (AttackStat / 4).ToString();
        }

        public override string InfoDamage()
        {
            return "spd up  " + (AttackStat / 2).ToString();
        }
    }
}
