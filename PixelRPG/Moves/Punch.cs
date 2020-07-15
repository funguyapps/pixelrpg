using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Punch : IMove
    {
        public override int NumTargets => 1;
        public override MoveTypes MoveType => MoveTypes.offensive;
        public override Stats AffectedStat => Stats.attack;
        public override string TextureStr { get => "Graphics\\Moves\\punch"; }

        public override int CalculateDamage()
        {
            return AttackStat / 4;
        }

        public override string ToString()
        {
            //return "Bite Damage: " + Damage;
            return "Punch";
        }

        public override string enemyToString()
        {
            return "Punch Damage: ";
        }

        public override string InfoDamage()
        {
            return "atk down " + Damage;
        }
    }
}
