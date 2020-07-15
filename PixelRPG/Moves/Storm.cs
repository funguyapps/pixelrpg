using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Storm : IMove
    {
        public override int NumTargets => 3;

        public override MoveTypes MoveType => MoveTypes.offensive;

        public override Stats AffectedStat => Stats.none;

        public override string TextureStr => "Graphics\\Moves\\storm";

        public override int CalculateDamage()
        {
            return AttackStat;
        }

        public override string ToString()
        {
            return "Storm"; //Damage: " + AttackStat;
        }

        public override string enemyToString()
        {
            return "Storm Damage: ";
        }

        public override string InfoDamage()
        {
            return this.Damage.ToString();
        }
    }
}
