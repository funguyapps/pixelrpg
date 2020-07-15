using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Spear : Bite
    {
        public override string TextureStr { get => "Graphics\\Moves\\spear"; }

        public override string ToString()
        {
            //return "Bite Damage: " + Damage;
            return "Spear";
        }

        public override string enemyToString()
        {
            return "Spear Damage: ";
        }
    }
}
