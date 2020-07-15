using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Flame : Bite
    {
        public override string TextureStr { get => "Graphics\\Moves\\flame"; }

        public override string ToString()
        {
            //return "Bite Damage: " + Damage;
            return "Flame";
        }

        public override string enemyToString()
        {
            return "Flame Damage: ";
        }

        public override string InfoDamage()
        {
            return this.Damage.ToString();
        }
    }
}
