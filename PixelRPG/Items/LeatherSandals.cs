using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Items
{
    [Serializable]
    public class LeatherSandals : Item
    {
        public override string TextureStr => "Graphics\\Items\\leather_sandals";

        [NonSerialized()]
        private Texture2D texture;
        public override Texture2D Texture { get => texture; set => texture = value; }

        public override Stats AffectedStat => Stats.speed;

        public override int Boost => 1;

        public override string Name => "Leather Sandals";

        public override string Description => "Leather sandals designed for durability but not speed";

        public override int Id { get; set; }

        public override int Price { get => 5; }
    }
}
