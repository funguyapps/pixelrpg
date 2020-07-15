using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static PixelRPG.Game1;
using PixelRPG.Moves;
using Microsoft.Xna.Framework.Graphics;

namespace PixelRPG.Pixels
{
    [Serializable()]
    public abstract class IPixel
    {
        // Every Pixel needs to store:

        // its name
        public abstract string Name { get; }

        // whether or not it is active
        public abstract bool Active { get; set; }

        // level at which it will evolve
        public abstract int EvolveLevel { get; set; }

        // a bool to tell whether or not it should evolve
        public abstract bool Evolve { get; set; }

        // its next form
        public abstract IPixel NextForm { get; set; }

        // its current level
        public abstract int Level { get; set; }

        // its element type, stored in an enum in Game1.cs
        public abstract ElementType Element { get; }

        // its health points
        public abstract int MaxHP { get; set; }

        // its current health points
        public abstract int CurrentHP { get; set; }

        // its current amount of XP, used for determing when it should level up
        public abstract int CurrentXP { get; set; }

        // the amount of XP it needs to reach before it levels up
        public abstract int LevelUpValue { get; set; }

        // its base speed, used for determining order in which Pixels attack in fights
        public abstract int Speed { get; set; }

        // its current speed, different every battle
        public abstract int CurrentSpeed { get; set; }

        // its defense, used for determining how resistant it is to taking damage
        public abstract int Defense { get; set; }

        // its current defense, may be affected by buffing moves
        public abstract int CurrentDefense { get; set; }

        // its attack power, used for determining how much damages moves do
        public abstract int Attack { get; set; }

        // its current attack, may be affected by buffing moves
        public abstract int CurrentAttack { get; set; }

        // a unique ID
        public abstract int ID { get; set; }

        // how much XP you get for killing a wild one
        public abstract int XPValue { get; }

        // its position as a Vector2
        public abstract Vector2 Position { get; set; }

        public abstract bool LeveledUp { get; set; }

        // its moves
        public abstract IMove Move1 { get; set; }
        public abstract IMove Move2 { get; set; }
        public abstract IMove Move3 { get; set; }
        public abstract IMove Move4 { get; set; }

        // the texture representing what image to use
        public abstract string TextureStr { get; }
        public abstract Texture2D Texture { get; set; }

        public abstract int CaptureChance { get; }

        public void Init(Texture2D texture, Vector2 position, int Id, Player player, bool isEnemy)
        {
            Position = position;

            if (player.SelectedArmor != null && !isEnemy)
                CurrentHP = MaxHP + player.SelectedArmor.Boost;
            else
                CurrentHP = MaxHP;

            Texture = texture;
            ID = Id;
            ID = Id;
            if (player.SelectedSword != null && !isEnemy)
                CurrentAttack = this.Attack + player.SelectedSword.Boost;
            else
                CurrentAttack = this.Attack;

            if (player.SelectedShield != null && !isEnemy)
                CurrentDefense = this.Defense + player.SelectedShield.Boost;
            else
                CurrentDefense = this.Defense;

            if (player.SelectedShoes != null && !isEnemy)
                CurrentSpeed = this.Speed + player.SelectedShoes.Boost;
            else
                CurrentSpeed = this.Speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public void LevelUp()
        {
            Level++;
            if (Level >= EvolveLevel)
            {
                Evolve = true;
            }
            else
            {
                LeveledUp = true;
            }

            int hpUp = 0;
            int spdUp = 0;
            int dfsUp = 0;
            int atkUp = 0;

            if (Element == ElementType.Fire)
            {
                hpUp = 10;
                spdUp = 10;
                dfsUp = 8;
                atkUp = 12;
            }
            else if (Element == ElementType.Earth)
            {
                hpUp = 10;
                spdUp = 10;
                dfsUp = 12;
                atkUp = 8;
            }
           else if (Element == ElementType.Air)
            {
                hpUp = 8;
                spdUp = 12;
                dfsUp = 10;
                atkUp = 10;
            }
            else if (Element == ElementType.Water)
            {
                hpUp = 12;
                atkUp = 8;
                dfsUp = 10;
                atkUp = 10;
            }

            MaxHP += hpUp;
            CurrentHP += hpUp;
            Speed += spdUp;
            CurrentSpeed = Speed;
            Defense += dfsUp;
            CurrentDefense = Defense;
            Attack += atkUp;
            CurrentAttack = Attack;

            CurrentXP -= LevelUpValue;
            LevelUpValue = (int)(LevelUpValue * 1.5f);

            //MaxHP += 20;
            //CurrentHP += 20;

            //Speed += 5;
            //Defense += 15;
            //CurrentDefense = Defense;
            //Attack += 8;
            //CurrentAttack = Attack;

            //CurrentXP -= LevelUpValue;
            //LevelUpValue *= 5;
        }

        public void EnemyLevelUp(int numTimes)
        {
            for (int i = 0; i < numTimes; i++)
            {
                MaxHP += 20;
                CurrentHP = MaxHP;

                Speed += 5;
                CurrentSpeed = Speed;
                Defense += 15;
                CurrentDefense = Defense;
                Attack += 8;
                CurrentAttack = Attack;

                Level++;

                CurrentXP -= LevelUpValue;
                LevelUpValue *= 5;
            }
        }
    }

    //public interface IPixel
    //{
    //    // Every Pixel needs to store:

    //    // its name
    //    string Name { get; }

    //    // whether or not it is active
    //    bool Active { get; set; }

    //    // level at which it will evolve
    //    int EvolveLevel { get; set; }

    //    // a bool to tell whether or not it should evolve
    //    bool Evolve { get; set; }

    //    // its next form
    //    IPixel NextForm { get; set; }

    //    // its current level
    //    int Level { get; set; }

    //    // its element type, stored in an enum in Game1.cs
    //    ElementType Element { get; }

    //    // its health points
    //    int MaxHP { get; set; }

    //    // its current health points
    //    int CurrentHP { get; set; }

    //    // its current amount of XP, used for determing when it should level up
    //    int CurrentXP { get; set; }

    //    // the amount of XP it needs to reach before it levels up
    //    int LevelUpValue { get; set; }

    //    // its base speed, used for determining order in which Pixels attack in fights
    //    int Speed { get; set;  }

    //    // its current speed, different every battle
    //    int CurrentSpeed { get; set; }

    //    // its defense, used for determining how resistant it is to taking damage
    //    int Defense { get; set; }

    //    // its current defense, may be affected by buffing moves
    //    int CurrentDefense { get; set; }

    //    // its attack power, used for determining how much damages moves do
    //    int Attack { get; set; }

    //    // its current attack, may be affected by buffing moves
    //    int CurrentAttack { get; set; }

    //    // a unique ID
    //    int ID { get; set; }

    //    // how much XP you get for killing a wild one
    //    int XPValue { get; }

    //    // its position as a Vector2

    //    Vector2 Position { get; set; }

    //    bool LeveledUp { get; set; }

    //    // its moves
    //    IMove Move1 { get; set; }
    //    IMove Move2 { get; set; }
    //    IMove Move3 { get; set; }
    //    IMove Move4 { get; set; }

    //    // the texture representing what image to use
    //    string TextureStr { get; }
    //    Texture2D Texture { get; set; }

    //    void Init(Texture2D texture, Vector2 position, int Id);
    //    void Draw(SpriteBatch spriteBatch);
    //    void LevelUp();
    //    void EnemyLevelUp(int numTimes);
    //}
}
