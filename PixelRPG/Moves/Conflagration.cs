using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Conflagration : Earthquake
    {
        public override string TextureStr => "Graphics\\Moves\\conflagration";

        public override string ToString()
        {
            return "Conflagration"; //Damage: " + AttackStat;
        }

        public override string enemyToString()
        {
            return "Conflagration Damage: ";
        }

        public override string InfoDamage()
        {
            return this.Damage.ToString();
        }
    }
}
