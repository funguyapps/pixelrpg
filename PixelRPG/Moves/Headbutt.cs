using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using PixelRPG.Pixels;

namespace PixelRPG.Moves
{
    class Headbutt : IMove
    {
        public override int NumTargets => 1;

        public override MoveTypes MoveType => MoveTypes.offensive;

        public override Stats AffectedStat => Stats.none;

        public override string TextureStr => "Graphics\\Moves\\headbutt";

        public override int CalculateDamage()
        {
            Random rand = new Random();
            if (rand.Next(1, 11) > 5)
            {
                return AttackStat * 2;
            }
            else
            {
                return AttackStat - 5;
            }
        }

        public override string enemyToString()
        {
            return "Headbutt Damage: ";
        }

        public override string ToString()
        {
            return "Headbutt"; //Damage: " + (AttackStat * 2).ToString() + " or " + (AttackStat - 5).ToString();
        }

        public override string InfoDamage()
        {
            return (AttackStat * 2).ToString() + " or " + (AttackStat - 5).ToString();
        }
    }
}
