using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Items
{
    [Serializable]
    public class IronSandals : Item
    {
        public override string TextureStr => "Graphics\\Items\\iron_sandals";

        [NonSerialized()]
        private Texture2D texture;
        public override Texture2D Texture { get => texture; set => texture = value; }

        public override Stats AffectedStat => Stats.speed;

        public override int Boost => 15;

        public override string Name => "Iron Sandals";

        public override string Description => "What they lack in comfort they make up for in durability.";

        public override int Id { get; set; }

        public override int Price { get => 1000; }
    }
}
