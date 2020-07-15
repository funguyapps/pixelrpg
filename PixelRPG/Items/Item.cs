using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Items
{
    [Serializable]
    public abstract class Item
    { 
        public abstract string TextureStr { get; }
        public abstract Texture2D Texture { get; set; }

        public abstract Stats AffectedStat { get; }
        public abstract int Boost { get; }
 
        public abstract string Name { get; }
        public abstract string Description { get; }

        public abstract int Id { get; set; }

        public abstract int Price { get; }

        public void Init(Texture2D texture, int id)
        {
            Texture = texture;
            Id = id;
        }
    }
}
