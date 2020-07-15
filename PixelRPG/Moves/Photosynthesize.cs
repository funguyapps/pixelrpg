using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Photosynthesize : IMove
    {
        public override int NumTargets => 1;

        public override MoveTypes MoveType => MoveTypes.defensive;

        public override Stats AffectedStat => Stats.attack;

        public override string TextureStr => "Graphics\\Moves\\stat_increase";

        public override int CalculateDamage()
        {
            return (int)(AttackStat * 1.25f);
        }

        public override string enemyToString()
        {
            //return "Enrage increased atk by: ";
            return "Photosynethesize";
        }

        public override string ToString()
        {
            return "Photosynthesize"; //Increase by: " + (AttackStat / 4).ToString();
        }

        public override string InfoDamage()
        {
            return "atk up " + ((int)(AttackStat * 1.25)).ToString();
        }
    }
}
