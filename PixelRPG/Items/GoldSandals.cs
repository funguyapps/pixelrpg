using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Items
{
    [Serializable]
    public class GoldSandals : Item
    {
        public override string TextureStr => "Graphics\\Items\\gold_sandals";

        [NonSerialized()]
        private Texture2D texture;
        public override Texture2D Texture { get => texture; set => texture = value; }

        public override Stats AffectedStat => Stats.speed;

        public override int Boost => 50;

        public override string Name => "Gold Sandals";

        public override string Description => "Step into the sandals of the heroes of old.";

        public override int Id { get; set; }

        public override int Price { get => 5000; }
    }
}
