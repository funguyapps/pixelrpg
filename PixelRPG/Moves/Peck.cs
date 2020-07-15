using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Peck : WindCutter
    {
        public override string TextureStr { get => "Graphics\\Moves\\peck"; }

        public override string ToString()
        {
            //return "Bite Damage: " + Damage;
            return "Peck";
        }

        public override string enemyToString()
        {
            return "Peck Damage: ";
        }
    }
}
