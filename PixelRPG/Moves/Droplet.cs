using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Droplet : WindCutter
    {
        public override string TextureStr => "Graphics\\Moves\\droplet";

        public override string ToString()
        {
            //return "Bite Damage: " + Damage;
            return "Droplet";
        }

        public override string enemyToString()
        {
            return "Droplet Damage: ";
        }
    }
}
