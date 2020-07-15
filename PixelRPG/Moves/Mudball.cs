using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using PixelRPG.Pixels;

namespace PixelRPG.Moves
{
    public class Mudball : IMove
    {        
        public override int NumTargets => 1;

        public override MoveTypes MoveType => MoveTypes.offensive;

        public override Stats AffectedStat => Stats.none;

        public bool initted = false;

        public override string TextureStr => "Graphics\\Moves\\mudball";

        public override int CalculateDamage()
        {
            Random rand = new Random();

            float total = AttackStat * (1.0f + (rand.Next(1, 9) / 10.0f));

            return (int)total;
            
        }

        public override string ToString()
        {
            int min = (int)(AttackStat * 1.1f);
            int max = (int)(AttackStat * 1.9f);

            return "Mudball"; //Damage: " + min.ToString() + " - " + max.ToString();
        }

        public override string enemyToString()
        {
            return "Mudball Damage: ";
        }

        public override string InfoDamage()
        {
            int min = (int)(AttackStat * 1.1f);
            int max = (int)(AttackStat * 1.9f);

            return min.ToString() + "-" + max.ToString();
        }
    }
}
