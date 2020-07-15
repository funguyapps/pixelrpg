using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class PoisonSting : IMove
    {
        public override int NumTargets => 1;
        public override MoveTypes MoveType => MoveTypes.offensive;
        public override Stats AffectedStat => Stats.defense;
        public override string TextureStr { get => "Graphics\\Moves\\stat_decrease"; }

        public override int CalculateDamage()
        {
            return AttackStat / 4;
        }

        public override string ToString()
        {
            //return "Bite Damage: " + Damage;
            return "Poison Sting";
        }

        public override string enemyToString()
        {
            return "Poison Sting Damage: ";
        }

        public override string InfoDamage()
        {
            return "dfs down " + this.Damage.ToString();
        }
    }
}
