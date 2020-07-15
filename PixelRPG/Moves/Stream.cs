using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Stream : IMove
    {
        public override int NumTargets => 1;
        public override MoveTypes MoveType => MoveTypes.offensive;
        public override Stats AffectedStat => Stats.none;
        public override string TextureStr { get => "Graphics\\Moves\\stream"; }

        public override int CalculateDamage()
        {
            return AttackStat;
        }

        public override string ToString()
        {
            //return "Bite Damage: " + Damage;
            return "Stream";
        }

        public override string enemyToString()
        {
            return "Stream Damage: ";
        }

        public override string InfoDamage()
        {
            return this.Damage.ToString();
        }
    }
}
