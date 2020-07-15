using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Magma : Droplet
    {
        public override string TextureStr => "Graphics\\Moves\\magma";

        public override string ToString()
        {
            //return "Bite Damage: " + Damage;
            return "Magma";
        }

        public override string enemyToString()
        {
            return "Magma Damage: ";
        }
    }
}
