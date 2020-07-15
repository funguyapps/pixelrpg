using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Puff : IMove
    {
        public override int NumTargets => 3;

        public override MoveTypes MoveType => MoveTypes.offensive;

        public override Stats AffectedStat => Stats.speed;

        public override string TextureStr => "Graphics\\Moves\\stat_decrease";

        public override int CalculateDamage()
        {
            return (AttackStat / 2);
        }

        public override string enemyToString()
        {
            return "Puff decreased spd by: ";
        }

        public override string ToString()
        {
            return "Puff"; //decrease by: " + (AttackStat / 2).ToString();
        }

        public override string InfoDamage()
        {
            return "spd down " + (AttackStat / 2).ToString();
        }
    }
}
