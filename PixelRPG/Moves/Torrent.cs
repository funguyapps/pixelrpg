using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Torrent : Mudball
    {
        public override string TextureStr => "Graphics\\Moves\\torrent";

        public override string ToString()
        {
            int min = (int)(AttackStat * 1.1f);
            int max = (int)(AttackStat * 1.9f);

            return "Torrent"; //Damage: " + min.ToString() + " - " + max.ToString();
        }

        public override string enemyToString()
        {
            return "Torrent Damage: ";
        }

        public override string InfoDamage()
        {
            int min = (int)(AttackStat * 1.1f);
            int max = (int)(AttackStat * 1.9f);

            return min.ToString() + "-" + max.ToString();
        }
    }
}
