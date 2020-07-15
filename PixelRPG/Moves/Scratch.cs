using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    class Scratch : Bite
    {
        public override string TextureStr => "Graphics\\Moves\\scratch";

        public override string ToString()
        {
            return "Scratch"; //Damage: " + Damage;
        }

        public override string enemyToString()
        {
            return "Scratch Damage: ";
        }
    }
}
