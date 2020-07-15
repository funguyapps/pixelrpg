using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Divebomb : IMove
    {
        public override int NumTargets => 1;
        public override MoveTypes MoveType => MoveTypes.offensive;
        public override Stats AffectedStat => Stats.none;
        public override string TextureStr { get => "Graphics\\Moves\\divebomb"; }

        public override int CalculateDamage()
        {
            return AttackStat / 2;
        }

        public override string ToString()
        {
            //return "Bite Damage: " + Damage;
            return "Divebomb";
        }

        public override string enemyToString()
        {
            return "Divebomb Damage: ";
        }

        public override string InfoDamage()
        {
            return this.Damage.ToString();
        }
    }
}
