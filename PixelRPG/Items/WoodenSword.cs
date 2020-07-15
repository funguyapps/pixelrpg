using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace PixelRPG.Items
{
    [Serializable]
    class WoodenSword : Item
    {
        public override string TextureStr => "Graphics\\Items\\wooden_sword";

        [NonSerialized()]
        private Texture2D texture;
        public override Texture2D Texture { get => texture; set => texture = value; }

        public override Stats AffectedStat => Stats.attack;

        public override int Boost => 2;

        public override string Name => "Wooden Sword";

        public override string Description => "A simple wooden sword; provides a negligible attack boost.";

        public override int Id { get; set; }

        public override int Price { get => 10; }
    }
}
