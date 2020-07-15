using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class WindCutter : IMove
    {
        Random rand = new Random();
        public override int NumTargets => 1;
        public override MoveTypes MoveType => MoveTypes.offensive;
        public override Stats AffectedStat => Stats.none;
        public override string TextureStr { get => "Graphics\\Moves\\wind_cutter"; }

        public override int CalculateDamage()
        {
            return AttackStat + rand.Next(1, 10);
        }

        public override string ToString()
        {
            //return "Bite Damage: " + Damage;
            return "Wind Cutter";
        }

        public override string enemyToString()
        {
            return "Wind Cutter Damage: ";
        }

        public override string InfoDamage()
        {
            return (AttackStat + 1).ToString() + "-" + (AttackStat + 10).ToString();
        }
    }
}
