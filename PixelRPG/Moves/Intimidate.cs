using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    class Intimidate : IMove
    {
        public override int NumTargets => 3;

        public override MoveTypes MoveType => MoveTypes.offensive;

        public override Stats AffectedStat => Stats.attack;

        public override string TextureStr => "Graphics\\Moves\\stat_decrease";

        public override int CalculateDamage()
        {
            return (AttackStat / 2);
        }

        public override string enemyToString()
        {
            return "Intimidate decreased atk by: ";
        }

        public override string ToString()
        {
            return "Intimidate"; //decrease by: " + (AttackStat / 2).ToString();
        }

        public override string InfoDamage()
        {
            return "atk down " + (AttackStat / 2).ToString();
        }
    }
}
