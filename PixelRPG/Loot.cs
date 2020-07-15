using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PixelRPG.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG
{
    public class GetLoot
    {
        static List<Item> lowItems = new List<Item>
        {
            new Stick(),
            new WoodenSword(),
            new PotLid(),
            new LeatherArmor(),
            new LeatherSandals()
        };
        static List<Item> mediumItems = new List<Item>
        {
            new IronSword(),
            new IronShield(),
            new IronArmor(),
            new IronSandals()
        };
        static List<Item> highItems = new List<Item>
        {
            new GoldSword(),
            new GoldShield(),
            new GoldArmor(),
            new GoldSandals()
        };

        static Random rand = new Random();

        public static Loot Get(int xp, ContentManager Content)
        {
            foreach (Item item in lowItems)
            {
                item.Init(Content.Load<Texture2D>(item.TextureStr), rand.Next(0, 100));
            }
            foreach (Item item in mediumItems)
            {
                item.Init(Content.Load<Texture2D>(item.TextureStr), rand.Next(0, 100));
            }
            foreach (Item item in highItems)
            {
                item.Init(Content.Load<Texture2D>(item.TextureStr), rand.Next(0, 100));
            }
            Loot basic = new Loot();

            if (xp <= 20)
            {
                basic = new Loot(xp * 3, null);
            }
            else if (xp >= 21 && xp <= 60)
            {
                Item possible = null;

                if (rand.Next(0, 5) == 1)
                    possible = lowItems[rand.Next(0, lowItems.Count - 1)];

                basic = new Loot(xp * 4, possible);
            }
            else if (xp >= 61 && xp <= 500)
            {
                Item possible = null;
                if (rand.Next(0, 3) == 1)
                    possible = mediumItems[rand.Next(0, mediumItems.Count - 1)];

                basic = new Loot(xp * 5, possible);
            }
            else if (xp > 501)
            {
                Item possible = null;
                if (rand.Next(0, 4) == 1)
                    possible = highItems[rand.Next(0, highItems.Count - 1)];

                basic = new Loot(xp * 6, possible);
            }
            return basic;
        }
    }

    public class Loot
    {
        public int _gold;
        public Item _item;

        public Loot() { }

        public Loot(int gold, Item item)
        {
            _gold = gold;
            _item = item;
        }
    }
}
