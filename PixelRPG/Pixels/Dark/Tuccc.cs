using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelRPG.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Pixels.Dark
{
    [Serializable]
    public class Tuccc : IPixel
    {
        public override string Name => "Tuccc";

        private bool active = true;
        public override bool Active { get => active; set => active = value; }

        private int evolveLevel = 99999999;
        public override int EvolveLevel { get => evolveLevel; set => evolveLevel = value; }

        private bool evolve = false;
        public override bool Evolve { get => evolve; set => evolve = value; }

        public override IPixel NextForm { get => null; set { return; } }

        private int level = 10;
        public override int Level { get => level; set => level = value; }

        public override ElementType Element => ElementType.Dark;

        private int maxHP = 220;
        public override int MaxHP { get => maxHP; set => maxHP = value; }

        private int currentHP;
        public override int CurrentHP { get => currentHP; set => currentHP = value; }

        private int currentXP = 0;
        public override int CurrentXP { get => currentXP; set => currentXP = value; }

        private int speed = 215;
        public override int Speed { get => speed; set => speed = value; }
        public override int CurrentSpeed { get; set; }

        private int defense = 196;
        public override int Defense { get => defense; set => defense = value; }
        public override int CurrentDefense { get; set; }

        private int attack = 144;
        public override int Attack { get => attack; set => attack = value; }
        public override int CurrentAttack { get; set; }

        public override int ID { get; set; }

        public override int XPValue => level * 20;

        public override int CaptureChance { get => 999999999; }

        [NonSerialized()]
        private Vector2 position;
        public override Vector2 Position { get => position; set { position = value; } }
        //public override Vector2 Position { get; set; }

        [NonSerialized()]
        private IMove move1 = new Mudball();
        public override IMove Move1 { get => move1; set { move1 = value; } }

        [NonSerialized()]
        private IMove move2 = new Enrage(); // new Earthquake();
        public override IMove Move2 { get => move2; set { move2 = value; } }

        [NonSerialized()]
        private IMove move3 = new Bite();
        public override IMove Move3 { get => move3; set { move3 = value; } }

        [NonSerialized()]
        private IMove move4 = null;
        public override IMove Move4 { get => move4; set { move4 = value; } }


        public override string TextureStr => "Graphics\\Pixels\\Dark\\tuccc";

        [NonSerialized()]
        private Texture2D texture;
        public override Texture2D Texture { get => texture; set { texture = value; } }
        //public override Texture2D Texture { get; set; }

        private int levelUpValue = 500;
        public override int LevelUpValue { get => levelUpValue; set => levelUpValue = value; }
        public override bool LeveledUp { get; set; }
    }
}
