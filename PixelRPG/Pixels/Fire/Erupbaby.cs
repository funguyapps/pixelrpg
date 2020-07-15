using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelRPG.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Pixels.Fire
{
    [Serializable]
    public class Erupbaby : IPixel
    {
        public override string Name => "Erupbaby";

        private bool active = true;
        public override bool Active { get => active; set => active = value; }

        private int evolveLevel = 13;
        public override int EvolveLevel { get => evolveLevel; set => evolveLevel = value; }

        private bool evolve = false;
        //private bool evolve = true;
        public override bool Evolve { get => evolve; set => evolve = value; }

        //private IPixel nextForm = new EarthDragonling();
        //private IPixel nextForm = EarthBaby;
        public override IPixel NextForm { get => new Eruption(); set { return; } }

        private int level = 1;
        public override int Level { get => level; set { level = value; } }

        public override ElementType Element { get { return ElementType.Fire; } }

        private int maxHP = 17;
        public override int MaxHP { get { return maxHP; } set { maxHP = value; } }
        private int currentHP;
        public override int CurrentHP { get { return currentHP; } set { currentHP = value; } }

        private int currentXP = 0;
        public override int CurrentXP { get { return currentXP; } set { currentXP = value; } }

        private int levelUpValue = 6;
        public override int LevelUpValue { get => levelUpValue; set => levelUpValue = value; }

        private int speed = 8;
        public override int Speed { get => speed; set => speed = value; }
        public override int CurrentSpeed { get; set; }

        private int defense = 11;
        public override int Defense { get => defense; set => defense = value; }
        public override int CurrentDefense { get; set; }

        private int attack = 17;
        public override int Attack { get => attack; set => attack = value; }
        public override int CurrentAttack { get; set; }

        public override int ID { get; set; }

        public override int XPValue { get => level * 4; }

        public override int CaptureChance { get => 2; }

        [NonSerialized()]
        private IMove move1 = new Magma();
        public override IMove Move1 { get { return move1; } set { move1 = value; } }

        [NonSerialized()]
        private IMove move2 = null;
        public override IMove Move2 { get { return move2; } set { move2 = value; } }

        [NonSerialized()]
        private IMove move3 = null;
        public override IMove Move3 { get { return move3; } set { move3 = value; } }

        [NonSerialized()]
        private IMove move4 = null;
        public override IMove Move4 { get { return move4; } set { move4 = value; } }

        [NonSerialized()]
        private Vector2 position;
        public override Vector2 Position { get => position; set { position = value; } }
        public override string TextureStr { get { return "Graphics\\Pixels\\Fire\\erupbaby"; } }

        [NonSerialized()]
        private Texture2D texture;
        public override Texture2D Texture { get => texture; set { texture = value; } }

        public override bool LeveledUp { get; set; }
    }
}
