using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Items
{
    [Serializable]
    class Stick : Item
    {
        public override string TextureStr => "Graphics\\Items\\stick";

        [NonSerialized()]
        private Texture2D texture;
        public override Texture2D Texture { get => texture; set => texture = value; }

        public override Stats AffectedStat => Stats.attack;

        public override int Boost => 1;

        public override string Name => "Stick";

        public override string Description => "A tree branch that was just picked up off the ground";

        public override int Id { get; set; }

        public override int Price { get => 5; }
    }
}
