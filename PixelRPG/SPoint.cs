using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG
{
    [Serializable]
    public class SPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public SPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
