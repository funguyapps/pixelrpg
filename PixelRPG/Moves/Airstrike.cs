using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Airstrike : IMove
    {
        public override int NumTargets => 3;
        public override MoveTypes MoveType => MoveTypes.offensive;
        public override Stats AffectedStat => Stats.none;
        public override string TextureStr { get => "Graphics\\Moves\\airstrike"; }

        public override int CalculateDamage()
        {
            return AttackStat;
        }

        public override string ToString()
        {
            //return "Bite Damage: " + Damage;
            return "Airstrike";
        }

        public override string enemyToString()
        {
            return "Airstrike Damage: ";
        }

        public override string InfoDamage()
        {
            return this.Damage.ToString();
        }
    }
}
