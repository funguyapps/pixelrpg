using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelRPG.Pixels;

namespace PixelRPG.Moves
{
    public class Slash : IMove
    {
        public override int NumTargets => 1;

        public override MoveTypes MoveType => MoveTypes.offensive;

        public override Stats AffectedStat => Stats.none;

        public override string TextureStr => "Graphics\\Moves\\slash";

        public override int CalculateDamage()
        {
            Random rand = new Random();
            return AttackStat + rand.Next(1, 11);
        }

        public override string ToString()
        {
            return "Slash"; //Damage: " + 
                //(AttackStat + 1).ToString() + " - " + (AttackStat + 10).ToString();
        }

        public override string enemyToString()
        {
            return "Slash Damage: ";
        }

        public override string InfoDamage()
        {
            return (AttackStat + 1).ToString() + "-" + (AttackStat + 10).ToString();
        }
    }
}
