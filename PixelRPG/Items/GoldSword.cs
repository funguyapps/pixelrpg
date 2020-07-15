using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Items
{
    [Serializable]
    public class GoldSword : Item
    {
        public override string TextureStr => "Graphics\\Items\\gold_sword";

        [NonSerialized()]
        private Texture2D texture;
        public override Texture2D Texture { get => texture; set => texture = value; }

        public override Stats AffectedStat => Stats.attack;

        public override int Boost => 50;

        public override string Name => "Gold Sword";

        public override string Description => "A godly, legendary blade.";

        public override int Id { get; set; }

        public override int Price { get => 5000; }
    }
}
