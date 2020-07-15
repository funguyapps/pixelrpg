using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Lava : Mudball
    {
        public override string TextureStr => "Graphics\\Moves\\lava";

        public override string ToString()
        {
            int min = (int)(AttackStat * 1.1f);
            int max = (int)(AttackStat * 1.9f);

            return "Lava"; //Damage: " + min.ToString() + " - " + max.ToString();
        }

        public override string enemyToString()
        {
            return "Lava Damage: ";
        }

        public override string InfoDamage()
        {
            int min = (int)(AttackStat * 1.1f);
            int max = (int)(AttackStat * 1.9f);

            return min.ToString() + "-" + max.ToString();
        }
    }
}
