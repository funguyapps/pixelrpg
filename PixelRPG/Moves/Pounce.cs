using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    class Pounce : Bite
    {
        public override string TextureStr => "Graphics\\Moves\\pounce";

        public override string ToString()
        {
            return "Pounce"; //Damage: " + Damage;
        }

        public override string enemyToString()
        {
            return "Pounce Damage: ";
        }
    }
}
