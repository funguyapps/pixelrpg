using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using PixelRPG.Pixels;

namespace PixelRPG.Moves
{
    public class Earthquake : IMove
    {
        public override int NumTargets => 3;

        public override MoveTypes MoveType => MoveTypes.offensive;

        public override Stats AffectedStat => Stats.none;

        public override string TextureStr => "Graphics\\Moves\\earthquake";

        public override int CalculateDamage()
        {
            return AttackStat;
        }

        public override string ToString()
        {
            return "Earthquake"; //Damage: " + AttackStat;
        }

        public override string enemyToString()
        {
            return "Earthquake Damage: ";
        }

        public override string InfoDamage()
        {
            return this.Damage.ToString();
        }
    }
}
