using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Clamp : Bite
    {
        public override string TextureStr { get => "Graphics\\Moves\\clamp"; }

        public override string ToString()
        {
            //return "Bite Damage: " + Damage;
            return "Clamp";
        }

        public override string enemyToString()
        {
            return "Clamp Damage: ";
        }

        public override string InfoDamage()
        {
            return this.Damage.ToString();
        }
    }
}
