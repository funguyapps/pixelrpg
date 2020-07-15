using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Items
{
    [Serializable]
    public class GoldShield : Item
    {
        public override string TextureStr => "Graphics\\Items\\gold_shield";

        [NonSerialized()]
        private Texture2D texture;
        public override Texture2D Texture { get => texture; set => texture = value; }

        public override Stats AffectedStat => Stats.defense;

        public override int Boost => 50;

        public override string Name => "Gold Shield";

        public override string Description => "An almost impenetrable shield.";

        public override int Id { get; set; }

        public override int Price { get => 5000; }
    }
}
