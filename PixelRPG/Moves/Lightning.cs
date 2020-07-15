using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Lightning : IMove
    {
        public override int NumTargets => 1;
        public override MoveTypes MoveType => MoveTypes.offensive;
        public override Stats AffectedStat => Stats.none;
        public override string TextureStr { get => "Graphics\\Moves\\lightning"; }

        public override int CalculateDamage()
        {
            float input = AttackStat;
            input *= 1.5f;
            return (int)input;
        }

        public override string ToString()
        {
            //return "Bite Damage: " + Damage;
            return "Lightning";
        }

        public override string enemyToString()
        {
            return "Lightning Damage: ";
        }

        public override string InfoDamage()
        {
            float input = AttackStat;
            input *= 1.5f;
            input = (int)input;
            return input.ToString();
        }
    }
}
