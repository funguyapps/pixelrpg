using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Deluge : Storm
    {
        public override string TextureStr => "Graphics\\Moves\\deluge";

        public override string ToString()
        {
            return "Deluge"; //Damage: " + AttackStat;
        }

        public override string enemyToString()
        {
            return "Deluge Damage: ";
        }

        public override string InfoDamage()
        {
            return this.Damage.ToString();
        }
    }
}
