using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Items
{
    [Serializable]
    class LeatherArmor : Item
    {
        public override string TextureStr => "Graphics\\Items\\leather_armor";

        [NonSerialized()]
        private Texture2D texture;
        public override Texture2D Texture { get => texture; set => texture = value; }

        public override Stats AffectedStat => Stats.health;

        public override int Boost => 1;

        public override string Name => "Leather Armor";

        public override string Description => "Poorly mainained basic leather armor";

        public override int Id { get; set; }

        public override int Price { get => 10; }
    }
}
