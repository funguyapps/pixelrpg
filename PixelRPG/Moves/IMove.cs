using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PixelRPG.Pixels;
using Microsoft.Xna.Framework.Graphics;

namespace PixelRPG.Moves
{
    //public interface IMove
    public abstract class IMove
    {
        // Every Move needs to store:

        // its damage, used to determine how much damage it will do to its target
        // or how much it will boost a designated stat if this.MoveType is offensive
        public int Damage { get => CalculateDamage(); }

        // the attack stat of the Pixel using this move
        public int AttackStat { get => User.CurrentAttack; }

        // how many enemies it targets
        public virtual int NumTargets { get; }

        // only used if this.MoveType is defensive
        // which stat to be increased
        public virtual Stats AffectedStat { get; }

        // whether it is offensive or defensive
        public virtual MoveTypes MoveType { get; }

        // the Pixel using this move
        public IPixel User { get; set; }

        // the texture representing what image to use
        public virtual string TextureStr { get; }
        public Texture2D Texture { get; set; }

        // void Init(Texture2D texture, IPixel user);
        public void Init(Texture2D texture, IPixel user)
        {
            Texture = texture;
            User = user;
        }
        // int CalculateDamage();
        public abstract int CalculateDamage();
        // string enemyToString();
        public abstract string enemyToString();

        public abstract string InfoDamage();
    }
}
