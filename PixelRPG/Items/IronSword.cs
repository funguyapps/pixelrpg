using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Items
{
    [Serializable]
    public class IronSword : Item
    {
        public override string TextureStr => "Graphics\\Items\\iron_sword";

        [NonSerialized()]
        private Texture2D texture;
        public override Texture2D Texture { get => texture; set => texture = value; }

        public override Stats AffectedStat => Stats.attack;

        public override int Boost => 15;

        public override string Name => "Iron Sword";

        public override string Description => "An average iron sword; provides a decent boost.";

        public override int Id { get; set; }

        public override int Price { get => 1000; }
    }
}
