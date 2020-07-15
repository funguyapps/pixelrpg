using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Items
{
    [Serializable]
    class PotLid : Item
    {
        public override string TextureStr => "Graphics\\Items\\pot_lid";

        [NonSerialized()]
        private Texture2D texture;
        public override Texture2D Texture { get => texture; set => texture = value; }

        public override Stats AffectedStat => Stats.defense;

        public override int Boost => 1;

        public override string Name => "Pot Lid";

        public override string Description => "Better suited to cooking than combat";

        public override int Id { get; set; }

        public override int Price { get => 8; }
    }
}
