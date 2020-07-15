using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using PixelRPG.Pixels;

namespace PixelRPG.Moves
{
    class Enrage : IMove
    {
        public override int NumTargets => 3;

        public override MoveTypes MoveType => MoveTypes.defensive;

        public override Stats AffectedStat => Stats.attack;

        public override string TextureStr => "Graphics\\Moves\\stat_increase";

        public override int CalculateDamage()
        {
            return AttackStat / 4;
        }

        public override string enemyToString()
        {
            //return "Enrage increased atk by: ";
            return "Enrage";
        }

        public override string ToString()
        {
            return "Enrage"; //Increase by: " + (AttackStat / 4).ToString();
        }

        public override string InfoDamage()
        {
            return "atk up " + (AttackStat / 4).ToString();
        }
    }
}
