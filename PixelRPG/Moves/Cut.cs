using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Cut : IMove
    {
        public override int NumTargets => 1;
        public override MoveTypes MoveType => MoveTypes.offensive;
        public override Stats AffectedStat => Stats.none;
        public override string TextureStr { get => "Graphics\\Moves\\cut"; }

        public override int CalculateDamage()
        {
            return AttackStat / 2;
        }

        public override string ToString()
        {
            //return "Bite Damage: " + Damage;
            return "Cut";
        }

        public override string enemyToString()
        {
            return "Cut Damage: ";
        }

        public override string InfoDamage()
        {
            return this.Damage.ToString();
        }
    }
}
