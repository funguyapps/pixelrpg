using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Drizzle : IMove
    {
        public override int NumTargets => 3;

        public override MoveTypes MoveType => MoveTypes.offensive;

        public override Stats AffectedStat => Stats.none;

        public override string TextureStr => "Graphics\\Moves\\drizzle";

        public override int CalculateDamage()
        {
            return AttackStat / 2;
        }

        public override string ToString()
        {
            return "Drizzle"; //Damage: " + AttackStat;
        }

        public override string enemyToString()
        {
            return "Drizzle Damage: ";
        }

        public override string InfoDamage()
        {
            return this.Damage.ToString();
        }
    }
}
