using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PixelRPG.Pixels;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PixelRPG.Items;
using PixelRPG.Quests;

namespace PixelRPG
{
    [Serializable()]
    public class Player
    {
        #region Fields
        const string FileName = @"Save\SavedPlayer.bin";
        // stores the texture that is the player image
        public Animation RestingAnimation;
        public Animation RightAnimation;
        public Animation LeftAnimation;
        public Animation UpAnimation;
        public Animation DownAnimation;
        public Animation CurrentAnimation;

        // stores the speed at which the player can move around the map
        public float MoveSpeed = 3.0f;

        // stores the width and height of the image
        public int Width;
        public int Height;

        // stores the direction the player is moving
        public Directions Direction;

        // stores whether or not the player is in motion
        public bool Moving;

        // tells how many pixels can be carried in the active lineup
        public int MaxPixels = 12;

        // stores which pixels are captured but not in the active lineup
        public List<IPixel> Backup = new List<IPixel>();

        // all of the Pixels the player has
        public List<IPixel> Lineup = new List<IPixel>();

        // the list of pixels to be regenerated
        public List<IPixel> RegenList = new List<IPixel>();

        // all of the swords
        public List<Item> Swords = new List<Item>();
        // all of the shields
        public List<Item> Shields = new List<Item>();
        // all of the armor
        public List<Item> Armor = new List<Item>();
        // all of the shoes
        public List<Item> Shoes = new List<Item>();

        public Item SelectedSword { get; set; }
        public Item SelectedShield { get; set; }
        public Item SelectedArmor { get; set; }
        public Item SelectedShoes { get; set; }

        // all the quests the player has
        public List<IQuest> Quests = new List<IQuest>(3);
        // all the quests that the player has completed
        public List<IQuest> completedQuests = new List<IQuest>();

        public Towns CurrentTown;

        public int tileX, tileY;

        public int gold = 0;

        public bool showBattleTut = false;
        public bool showBuyTut = false;
        public bool showGuildTut = false;
        public bool showLineupTut = false;
        public bool showInventoryTut = false;
        public bool showPauseLineupTut = false;
        public bool showQuestsTut = false;
        public bool showSellTut = false;
        public bool showWarranty = true;

        public bool inTown = true;

        public List<SPoint> foundAreas = new List<SPoint>();
        // stores position of player
        [NonSerialized()]
        public Vector2 position;
        #endregion

        #region Methods
        public static Player Load()
        {
            Player TestPlayer = null;
            if (File.Exists(FileName))
            {
                Stream TestFileStream = File.OpenRead(FileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                if (TestFileStream.Length > 0) TestPlayer = (Player)deserializer.Deserialize(TestFileStream);
                TestFileStream.Close();
            }

            return TestPlayer;
        }

        public void Save(Player current)
        {
            Stream TestFileStream = File.Create(FileName);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(TestFileStream, current);
            TestFileStream.Close();
        }

        public void Init(Animation restingAnimation, Animation rightAnimation, Animation leftAnimation, Animation upAnimation, Animation downAnimation)
        {
            RestingAnimation = restingAnimation;
            RightAnimation = rightAnimation;
            LeftAnimation = leftAnimation;
            UpAnimation = upAnimation;
            DownAnimation = downAnimation;
            CurrentAnimation = restingAnimation;
            position = new Vector2(450, 207);
        }

        public void Update(GameTime gameTime)
        {
            if(Direction == Directions.up && Moving)
            {
                CurrentAnimation = UpAnimation;
            }
            else if(Direction == Directions.down && Moving)
            {
                CurrentAnimation = DownAnimation;
            }
            else if(Direction == Directions.right && Moving)
            {
                CurrentAnimation = RightAnimation;
            }
            else if(Direction == Directions.left && Moving)
            {
                CurrentAnimation = LeftAnimation;
            }
            else
            {
                CurrentAnimation = RestingAnimation;
            }

            CurrentAnimation.Position = position;
            CurrentAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentAnimation.Draw(spriteBatch, position);
        }
        #endregion
    }
}
