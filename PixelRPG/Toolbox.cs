using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PixelRPG.Items;
using PixelRPG.Pixels;
using PixelRPG.Pixels.Air;
using PixelRPG.Pixels.Earth;
using PixelRPG.Pixels.Water;
using PixelRPG.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG
{
    public class Toolbox
    {
        public List<Point> homeIslandBoundaries = new List<Point>
        {
            new Point(9,0),
            new Point(9,1),
            new Point(9,2),
            new Point(9,3),
            new Point(9,4),
            new Point(8,5),
            new Point(8,4),
            new Point(7,5),
            new Point(7,4),
            new Point(6,5),
            new Point(5,5),
            new Point(4,4),
            new Point(3,4),
            new Point(2,4),
            new Point(1,5),
            new Point(2,4),
            new Point(0,0), new Point(0,1), new Point(0,2), new Point(0,3), new Point(0,4),
            new Point(1,-1), new Point(2,-1), new Point(3,-1), new Point(4,-1), new Point(5,-1),
            new Point(6,-1), new Point(7,-1), new Point(8,-1), new Point(9,-1)
        };
        List<Point> plainAreas = new List<Point>
        {
            new Point(1,0),
            new Point(2,0),
            new Point(3,0),
            new Point(1,1),
            new Point(2,1),
            new Point(3,1),
            new Point(4,2),
            new Point(5,3),
            new Point(6,3),
            new Point(7,3),
            new Point(7,2),
            new Point(7,1),
            new Point(7,0)
        };
        List<Point> forestAreas = new List<Point>
        {
            new Point(1,2),
            new Point(2,2),
            new Point(3,2),
            new Point(1,3)
        };
        List<Point> beachAreas = new List<Point>
        {
            new Point(1,4),
            new Point(2,3),
            new Point(3,3),
            new Point(4,3),
            new Point(4,5),
            new Point(4,6),
            new Point(4,7),
            new Point(4,8),
            new Point(8,1),
            new Point(8,0),
            new Point(8,2),
            new Point(8,3),
            new Point(5,4),
            new Point(6,4)
        };
        List<Point> mountainAreas = new List<Point>
        {
            new Point(4,0),
            new Point(4,1),
            new Point(5,2),
            new Point(6,2),
            new Point(6,1),
            new Point(6,0)
        };
        List<Point> iceAreas = new List<Point>
        {
            new Point(5,0),
            new Point(5,1)
        };

        List<Item> buyItems = new List<Item>();
        List<Item> domusItems = new List<Item>
        {
            new LeatherArmor(),
            new LeatherSandals(),
            new PotLid(),
            new Stick(),
            new WoodenSword()
        };
        List<Item> oceanicItems = new List<Item>
        {
            new IronArmor(),
            new IronSandals(),
            new IronShield(),
            new IronSword()
        };
        List<IQuest> domusQuests = new List<IQuest>
        {
            new CaptureQuest("Chip Baby", 1, "Go out into the plains and catch your first pixel! A Chip Baby should do nicely.", Towns.Domus, 50),
            new KillQuest("Nimbaby", 3, "A group of Nimbabies have been wreaking havoc in the mountains to the east. Go check it out.", Towns.Domus, 100),
            new TravelQuest(Towns.Oceanic, 1, "It's time you saw more of the world. Go to Oceanic, far to the east.", Towns.Domus, 250)
        };
        List<IQuest> oceanicQuests = new List<IQuest>
        {
            new KillQuest("Narbaby", 2, "Go to the north until you hit the ocean, then go west. Defeat 2 Narbabies.", Towns.Oceanic, 300),
            new KillQuest("Salababy", 3, "Stay on the eastern coastline here and defeat 3 Salababies.", Towns.Oceanic, 500),
            new CaptureQuest("Fire Baby", 1, "Capture a Fire Baby on this coastline. Be warned, they are very rare.", Towns.Oceanic, 2000)
        };
        public List<IQuest> complete = new List<IQuest>();

        Random rand = new Random();

        LineupToolbox lntb; 

        Texture2D pauseScreen;
        Texture2D inventoryScreen;
        Texture2D lineupPauseScreen;
        SpriteFont nameDisplay;
        Texture2D leftArrow;
        Texture2D rightArrow;
        Texture2D shopScreenTex;
        Texture2D selectedItemMarker;
        Texture2D buysellScreen;
        Texture2D selectedBox;
        Texture2D sellOverlay;
        Texture2D hoverMoney;
        Texture2D x;
        Texture2D hoverBuy;
        Texture2D hoverSell;
        Texture2D hoverI;
        Texture2D hoverL;
        Texture2D shiftLeft;
        Texture2D shiftRight;
        Texture2D hoverUp;
        Texture2D hoverDown;
        Texture2D hoverQ;
        Texture2D questScreen;
        Texture2D guildScreen;
        Texture2D selectedQuest;
        Texture2D progressBarEmpty, progressBarFull;
        Texture2D prizePopup;
        SpriteFont goldDisplay;
        Texture2D hoverSave;
        Texture2D shadedX;
        Texture2D buyTut, sellTut, inventoryTut, pauseLineupTut, questsTut;
        Texture2D hoverTown;
        Texture2D map;
        Texture2D ocean;
        Texture2D here;

        SpriteBatch spriteBatch;

        public bool inPause = false;
        public bool inShop = false;
        public bool leftTown = true;
        public bool inGuild = false;
        public bool inMap = false;
        public bool inInventory = false;
        public bool inLineup = false;
        public bool inQuests = false;
        bool allowMove = true;
        bool allowShift = true;
        bool justOpen = true;
        bool inBuy = false;
        bool allowBuy = true;
        bool inSell = false;
        public bool allowClick = false;
        bool showPrize = false;
        bool wait = false;

        int startingFrame = 0;
        int swordIndex = 0;
        int shieldIndex = 0;
        int armorIndex = 0;
        int shoeIndex = 0;

        Item activeItem;

        Rectangle rightUpShift = new Rectangle(700, 180, 60, 100);
        Rectangle leftUpShift = new Rectangle(40, 180, 60, 100);
        Rectangle upRHalf1 = new Rectangle(170, 180, 50, 100);
        Rectangle upRHalf2 = new Rectangle(310, 180, 50, 100);
        Rectangle upRHalf3 = new Rectangle(490, 180, 50, 100);
        Rectangle upRHalf4 = new Rectangle(630, 180, 50, 100);
        Rectangle upLHalf1 = new Rectangle(120, 180, 50, 100);
        Rectangle upLHalf2 = new Rectangle(260, 180, 50, 100);
        Rectangle upLHalf3 = new Rectangle(440, 180, 50, 100);
        Rectangle upLHalf4 = new Rectangle(580, 180, 50, 100);

        Vector2 up1Vector = new Vector2(125, 185);
        Vector2 up2Vector = new Vector2(265, 185);
        Vector2 up3Vector = new Vector2(445, 185);
        Vector2 up4Vector = new Vector2(585, 185);

        Vector2 selectedBoxPos = new Vector2(1000, 1000);

        public Toolbox(SpriteBatch s, ContentManager Content, SpriteFont n, LineupToolbox l)
        {
            spriteBatch = s;
            nameDisplay = n;
            pauseScreen = Content.Load<Texture2D>("Graphics\\UI\\pause_screen_2");
            inventoryScreen = Content.Load<Texture2D>("Graphics\\UI\\inventory_screen");
            lineupPauseScreen = Content.Load<Texture2D>("Graphics\\UI\\lineup_pause");
            leftArrow = Content.Load<Texture2D>("Graphics\\UI\\left_hover_arrow");
            rightArrow = Content.Load <Texture2D>("Graphics\\UI\\right_hover_arrow");
            shopScreenTex = Content.Load<Texture2D>("Graphics\\UI\\shop_screen");
            selectedItemMarker = Content.Load<Texture2D>("Graphics\\UI\\selected_item");
            buysellScreen = Content.Load<Texture2D>("Graphics\\UI\\buy_sell_screen");
            selectedBox = Content.Load<Texture2D>("Graphics\\UI\\selected_item");
            sellOverlay = Content.Load<Texture2D>("Graphics\\UI\\sell_overlay");
            hoverMoney = Content.Load<Texture2D>("Graphics\\UI\\hover_money");
            x = Content.Load<Texture2D>("Graphics\\UI\\x");
            hoverBuy = Content.Load<Texture2D>("Graphics\\UI\\hover_buy");
            hoverSell = Content.Load<Texture2D>("Graphics\\UI\\hover_sell");
            hoverI = Content.Load<Texture2D>("Graphics\\UI\\hover_i");
            hoverL = Content.Load<Texture2D>("Graphics\\UI\\hover_l");
            shiftLeft = Content.Load<Texture2D>("Graphics\\UI\\left_shift_arrow");
            shiftRight = Content.Load<Texture2D>("Graphics\\UI\\right_shift_arrow");
            hoverUp = Content.Load<Texture2D>("Graphics\\UI\\up_white_hover_arrow");
            hoverDown = Content.Load<Texture2D>("Graphics\\UI\\down_white_hover_arrow");
            hoverQ = Content.Load<Texture2D>("Graphics\\UI\\hover_q");
            questScreen = Content.Load<Texture2D>("Graphics\\UI\\quest_screen");
            guildScreen = Content.Load<Texture2D>("Graphics\\UI\\guild_screen");
            selectedQuest = Content.Load<Texture2D>("Graphics\\UI\\selected_quest");
            progressBarEmpty = Content.Load<Texture2D>("Graphics\\UI\\progress_bar_empty");
            progressBarFull = Content.Load<Texture2D>("Graphics\\UI\\progress_bar_full");
            prizePopup = Content.Load<Texture2D>("Graphics\\UI\\prize_popup");
            goldDisplay = Content.Load<SpriteFont>("Graphics\\goldDisplay");
            hoverSave = Content.Load<Texture2D>("Graphics\\UI\\hover_save");
            shadedX = Content.Load<Texture2D>("Graphics\\UI\\shaded_x");
            buyTut = Content.Load<Texture2D>("Graphics\\UI\\Tutorials\\buy");
            sellTut = Content.Load<Texture2D>("Graphics\\UI\\Tutorials\\sell");
            inventoryTut = Content.Load<Texture2D>("Graphics\\UI\\Tutorials\\inventory");
            pauseLineupTut = Content.Load<Texture2D>("Graphics\\UI\\Tutorials\\pause_lineup");
            questsTut = Content.Load<Texture2D>("Graphics\\UI\\Tutorials\\quests");
            hoverTown = Content.Load<Texture2D>("Graphics\\UI\\hover_town");
            map = Content.Load<Texture2D>("Graphics\\Map\\mini\\map");
            ocean = Content.Load<Texture2D>("Graphics\\Map\\mini\\ocean");
            here = Content.Load<Texture2D>("Graphics\\Map\\mini\\here");

            lntb = l;
        }

        public void UpdateRegionType(int x, int y, CurrentTile currentTile)
        {
            if (currentTile.CurrentIsland == Islands.home)
            {
                Point point = new Point(x, y);
                if (plainAreas.Contains(point))
                {
                    currentTile.CurrentRegion = RegionType.plain;
                }
                else if (forestAreas.Contains(point))
                {
                    currentTile.CurrentRegion = RegionType.forest;
                }
                else if (beachAreas.Contains(point))
                {
                    currentTile.CurrentRegion = RegionType.beach;
                }
                else if (mountainAreas.Contains(point))
                {
                    currentTile.CurrentRegion = RegionType.mountain;
                }
                else if (iceAreas.Contains(point))
                {
                    currentTile.CurrentRegion = RegionType.ice;
                }
            }
        }

        public void UpdateSpawnRate(CurrentTile currentTile, int tileX, int tileY)
        {
            currentTile.SpawnRate = 1000 / (tileX + tileY);
        }

        public string GetRightStr(int tileX, int tileY)
        {
            return "Graphics\\Map\\" + (tileX + 1).ToString() + tileY.ToString();
        }

        public string GetLeftStr(int tileX, int tileY)
        {
            return "Graphics\\Map\\" + (tileX - 1).ToString() + tileY.ToString();
        }

        public string GetUpStr(int tileX, int tileY)
        {
            return "Graphics\\Map\\" + tileX.ToString() + (tileY + 1).ToString();
        }

        public string GetDownStr(int tileX, int tileY)
        {
            return "Graphics\\Map\\" + tileX.ToString() + (tileY - 1).ToString();
        }

        public void UpdatePeripheralStr(int tileX, int tileY, CurrentTile currentTile)
        {
            if (tileX == 1)
                currentTile.RightTextureStr = GetRightStr(tileX, tileY);
            else
            {
                currentTile.RightTextureStr = GetRightStr(tileX, tileY);
                currentTile.LeftTextureStr = GetLeftStr(tileX, tileY);
            }
            if (tileY == 0)
                currentTile.UpTextureStr = GetUpStr(tileX, tileY);
            else
            {
                currentTile.UpTextureStr = GetUpStr(tileX, tileY);
                currentTile.DownTextureStr = GetDownStr(tileX, tileY);
            }
        }

        public int InitNewPixels(List<IPixel> ls, int id, ContentManager Content, Player player, IPixel type)
        {
            var earthBaby01 = (IPixel)Activator.CreateInstance(type.GetType());
            earthBaby01.Init(Content.Load<Texture2D>(earthBaby01.TextureStr), new Vector2(100, 332), id, player, false);
            id++;
            ls.Add(earthBaby01);

            return id;
        }

        public int ReInitPixels(List<IPixel> ls, List<IPixel> backupls, int id, ContentManager Content, Player player)
        {
            for (int i = 0; i < ls.Count; i++)
            {
                IPixel temp = ls[i];
                temp.Init(Content.Load<Texture2D>(temp.TextureStr), new Vector2(100, 332), id, player, false);
                id++;

                IPixel moveObj = (IPixel)Activator.CreateInstance(temp.GetType());

                temp.Move1 = moveObj.Move1;
                temp.Move2 = moveObj.Move2;
                temp.Move3 = moveObj.Move3;
                temp.Move4 = moveObj.Move4;

                ls[i] = temp;
            }
            for (int i = 0; i < backupls.Count; i++)
            {
                IPixel temp = backupls[i];
                temp.Init(Content.Load<Texture2D>(temp.TextureStr), new Vector2(100, 332), id, player, false);
                id++;

                IPixel moveObj = (IPixel)Activator.CreateInstance(temp.GetType());

                temp.Move1 = moveObj.Move1;
                temp.Move2 = moveObj.Move2;
                temp.Move3 = moveObj.Move3;
                temp.Move4 = moveObj.Move4;

                backupls[i] = temp;
            }

            return id;
        }

        public void ShowMap(Islands island, Player player, ContentManager Content)
        {
            spriteBatch.Draw(map, new Vector2(291, 26), Color.White);
            if (island == Islands.home)
            {
                for (int i = 1; i < 9; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        SPoint point = new SPoint(i, j);
                        Point point2 = new Point(point.X, point.Y);
                        if (!homeIslandBoundaries.Contains(point2))
                        {
                            Texture2D tex = Content.Load<Texture2D>("Graphics\\Map\\mini\\unknown");
                            spriteBatch.Draw(tex, new Vector2(0 + 100 * (i - 1), 360 - 60 * j), Color.White);
                        }
                    }
                }
                foreach (var p in player.foundAreas)
                {
                    Texture2D tex2 = Content.Load<Texture2D>("Graphics\\Map\\mini\\" + p.X.ToString() + p.Y.ToString());
                    spriteBatch.Draw(tex2, new Vector2(0 + 100 * (p.X - 1), 360 - 60 * p.Y), Color.White);
                }
                spriteBatch.Draw(here, new Vector2(0 + 100 * (player.tileX - 1), 360 - 60 * player.tileY), Color.White);
            }
        }

        public bool ShowTutorial(Texture2D texture, MouseState currentMouseState)
        {
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            spriteBatch.Draw(shadedX, Vector2.Zero, Color.White);

            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                if (new Rectangle(0, 0, 60, 60).Contains(currentMouseState.Position))
                    return false;
            }
            else if (currentMouseState.LeftButton == ButtonState.Released)
            {
                if (new Rectangle(0, 0, 60, 60).Contains(currentMouseState.Position))
                    spriteBatch.Draw(x, Vector2.Zero, Color.White);
            }
            return true;
        }

        public void GuildScreen(MouseState currentMouseState, Player player)
        {
            spriteBatch.Draw(guildScreen, Vector2.Zero, Color.White);

            List<IQuest> townQuests = new List<IQuest>();
            if (player.CurrentTown == Towns.Domus)
            {
                townQuests = domusQuests;
            }
            else if (player.CurrentTown == Towns.Oceanic)
            {
                townQuests = oceanicQuests;
            }

            for (int i = 0; i < townQuests.Count; i++)
            {
                IQuest result = player.Quests.Find(p => p.Description == townQuests[i].Description);
                if (result != null)
                {
                    townQuests.RemoveAt(i);
                }
            }

            for (int i = 0; i < complete.Count; i++)
            {
                if (townQuests.Count > 1 && townQuests[0].Description == complete[0].Description)
                    townQuests.RemoveAt(0);


                if (!complete[0].goldAwarded)
                {
                    showPrize = true;
                    spriteBatch.Draw(prizePopup, Vector2.Zero, Color.White);

                    spriteBatch.DrawString(nameDisplay, "Quest Completed!", new Vector2(161, 141), Color.Black);
                    spriteBatch.DrawString(goldDisplay, complete[0].Reward.ToString(), new Vector2(237, 215), Color.Gold);
                }
                else if (complete[0].goldAwarded)
                {
                    complete.Add(complete[0]);
                    complete.RemoveAt(0);
                }
            }

            if (!showPrize)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i < townQuests.Count)
                    {
                        if (NumOfLines(townQuests[i].Description) == 1)
                        {
                            spriteBatch.DrawString(nameDisplay, townQuests[i].Description, new Vector2(121, 181 + (i * 100)), Color.White);
                        }
                        else if (NumOfLines(townQuests[i].Description) == 2)
                        {
                            spriteBatch.DrawString(nameDisplay, townQuests[i].Description.Substring(0, 63), new Vector2(121, 181 + (i * 100)), Color.White);
                            spriteBatch.DrawString(nameDisplay, townQuests[i].Description.Substring(63, townQuests[i].Description.Length - 63), new Vector2(121, 195 + (i * 100)), Color.White);
                        }
                        //for (int j = 1; j <= NumOfLines(townQuests[i].Description); j++)
                        //{
                        //    if (j == 1)
                        //    {
                        //        spriteBatch.DrawString(nameDisplay, townQuests[i].Description.Substring(0, 63), new Vector2(121, 181 + (i * 100)), Color.White);
                        //    }
                        //    else if (j == 2)
                        //    {
                        //        spriteBatch.DrawString(nameDisplay, townQuests[i].Description.Substring(63, townQuests[i].Description.Length - 63), new Vector2(121, 195 + (i * 100)), Color.White);
                        //    }
                        //}
                    }
                }
            }

            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                if (showPrize && new Rectangle(620, 140, 60, 60).Contains(currentMouseState.Position) && allowClick)
                {
                    allowClick = false;
                    showPrize = false;

                    for(int i = 0; i < player.completedQuests.Count; i++)
                    {
                        if (player.completedQuests[i].Description == complete[0].Description)
                        {
                            player.completedQuests[i].goldAwarded = true;
                        }
                    }

                    player.Quests.Remove(complete[0]);
                    complete.RemoveAt(0);
                }

                if (new Rectangle(740, 0, 60, 60).Contains(currentMouseState.Position))
                {
                    if (player.CurrentTown == Towns.Domus)
                    {
                        domusQuests = townQuests;
                    }

                    inGuild = false;
                }
                else if (new Rectangle(40, 180, 40, 40).Contains(currentMouseState.Position) && allowClick)
                {
                    allowClick = false;
                    if (player.Quests.Count + 1 < 4 && townQuests.Count > 0)
                    {
                        player.Quests.Add(townQuests[0]);
                        townQuests.RemoveAt(0);
                    }
                }
                else if (new Rectangle(40, 280, 40, 40).Contains(currentMouseState.Position) && allowClick)
                {
                    allowClick = false;
                    if (player.Quests.Count + 1 < 4 && townQuests.Count > 1)
                    {
                        player.Quests.Add(townQuests[1]);
                        townQuests.RemoveAt(1);
                    }
                }
                else if (new Rectangle(40, 380, 40, 40).Contains(currentMouseState.Position) && allowClick)
                {
                    allowClick = false;
                    if (player.Quests.Count + 1 < 4 && townQuests.Count > 2)
                    {
                        player.Quests.Add(townQuests[2]);
                        townQuests.RemoveAt(2);
                    }
                }
            }
            else if (currentMouseState.LeftButton == ButtonState.Released)
            {
                if (!allowClick) allowClick = true;

                if (showPrize && new Rectangle(620, 140, 60, 60).Contains(currentMouseState.Position))
                {
                    spriteBatch.Draw(x, new Vector2(620, 140), Color.White);
                }

                if (new Rectangle(740, 0, 60, 60).Contains(currentMouseState.Position))
                    spriteBatch.Draw(x, new Vector2(740, 0), Color.White);
                else if (new Rectangle(40, 180, 40, 40).Contains(currentMouseState.Position))
                    spriteBatch.Draw(selectedQuest, new Vector2(40, 180), Color.White);
                else if (new Rectangle(40, 280, 40, 40).Contains(currentMouseState.Position))
                    spriteBatch.Draw(selectedQuest, new Vector2(40, 280), Color.White);
                else if (new Rectangle(40, 380, 40, 40).Contains(currentMouseState.Position))
                    spriteBatch.Draw(selectedQuest, new Vector2(40, 380), Color.White);
            }
        }

        private int NumOfLines(string str_in)
        {
            int returned = 1;

            if (str_in.Count() > 63)
            {
                returned++;
            }

            return returned;
        }

        public bool PauseScreen(MouseState currentMouseState, Player player, CurrentTile currentTile, ContentManager Content)
        {
            bool intown = false;
            if (inInventory)
            {
                if (player.showInventoryTut)
                {
                    player.showInventoryTut = ShowTutorial(inventoryTut, currentMouseState);
                }
                else
                    InventoryScreen(currentMouseState, player);
            }
            else if (inLineup)
            {
                if (player.showPauseLineupTut)
                {
                    player.showPauseLineupTut = ShowTutorial(pauseLineupTut, currentMouseState);
                }
                else
                    PauseLineupScreen(currentMouseState, player);
            }
            else if (inQuests)
            {
                if (player.showQuestsTut)
                {
                    player.showQuestsTut = ShowTutorial(questsTut, currentMouseState);
                }
                else
                    QuestScreen(currentMouseState, player);
            }
            else
            {
                spriteBatch.Draw(pauseScreen, Vector2.Zero, Color.White);

                spriteBatch.DrawString(nameDisplay, "Completed Quests: " + player.completedQuests.Count.ToString() + "/6", new Vector2(279, 135), Color.White);

                if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (new Rectangle(740, 0, 60, 60).Contains(currentMouseState.Position))
                    {
                        inPause = false;
                    }

                    else if (new Rectangle(37, 180, 180, 180).Contains(currentMouseState.Position))
                    {
                        inInventory = true;
                    }

                    else if (new Rectangle(583, 180, 180, 180).Contains(currentMouseState.Position))
                    {
                        inLineup = true;
                    }

                    else if (new Rectangle(309, 181, 180, 180).Contains(currentMouseState.Position))
                    {
                        inQuests = true;
                    }
                    else if (new Rectangle(0, 0, 60, 60).Contains(currentMouseState.Position))
                    {
                        player.Save(player);
                    }
                    else if (new Rectangle(0, 420, 60, 60).Contains(currentMouseState.Position))
                    {
                        wait = true;
                    }
                }
                if (currentMouseState.LeftButton == ButtonState.Released)
                {
                    if (wait)
                    {
                        intown = true;
                        inPause = false;
                        if (player.CurrentTown == Towns.Domus)
                        {
                            currentTile.CurrentTexture = Content.Load<Texture2D>("Graphics\\Map\\10");
                            player.tileX = 1;
                            player.tileY = 0;
                        }
                        else if (player.CurrentTown == Towns.Oceanic)
                        {
                            currentTile.CurrentTexture = Content.Load<Texture2D>("Graphics\\Map\\80");
                            player.tileX = 8;
                            player.tileY = 0;
                        }
                        wait = false;
                    }
                    if (new Rectangle(37, 180, 180, 180).Contains(currentMouseState.Position))
                        spriteBatch.Draw(hoverI, new Vector2(37, 180), Color.White);
                    else if (new Rectangle(583, 180, 180, 180).Contains(currentMouseState.Position))
                        spriteBatch.Draw(hoverL, new Vector2(583, 180), Color.White);
                    else if (new Rectangle(740, 0, 60, 60).Contains(currentMouseState.Position))
                        spriteBatch.Draw(x, new Vector2(740, 0), Color.White);
                    else if (new Rectangle(309, 181, 180, 180).Contains(currentMouseState.Position))
                        spriteBatch.Draw(hoverQ, new Vector2(309, 181), Color.White);
                    else if (new Rectangle(0, 0, 60, 60).Contains(currentMouseState.Position))
                        spriteBatch.Draw(hoverSave, new Vector2(0, 0), Color.White);
                    else if (new Rectangle(0, 420, 60, 60).Contains(currentMouseState.Position))
                        spriteBatch.Draw(hoverTown, new Vector2(0, 420), Color.White);
                }
            }

            return intown;
        }

        private void QuestScreen(MouseState currentMouseState, Player player)
        {
            spriteBatch.Draw(questScreen, Vector2.Zero, Color.White);

            for (int i = 0; i < player.Quests.Count; i++)
            {
                IQuest q = player.Quests[i];
                spriteBatch.DrawString(nameDisplay, "Type: " + q.QuestType.ToString(), new Vector2(81, 141 + (i * 100)), Color.Black);
                if (q.QuestType == QuestTypes.kill)
                    spriteBatch.DrawString(nameDisplay, "Target: " + q.KillTarget, new Vector2(81, 155 + (i * 100)), Color.Black);
                else if (q.QuestType == QuestTypes.capture)
                    spriteBatch.DrawString(nameDisplay, "Target: " + q.CaptureTarget, new Vector2(81, 155 + (i * 100)), Color.Black);
                else if (q.QuestType == QuestTypes.travel)
                    spriteBatch.DrawString(nameDisplay, "Target: " + q.TravelTarget.ToString(), new Vector2(81, 155 + (i * 100)), Color.Black);

                spriteBatch.DrawString(nameDisplay, "Progress: ", new Vector2(81, 169 + (i * 100)), Color.Black);
                ProgressBar((float)q.Progress, (float)q.Goal, new Vector2(190, 173 + (i * 100)));
                spriteBatch.DrawString(nameDisplay, q.Progress.ToString() + "/" + q.Goal.ToString(), new Vector2(81, 183 + (i * 100)), Color.Black);

                if (q.Progress == q.Goal)
                    spriteBatch.DrawString(nameDisplay, "Completed!", new Vector2(343, 188 + (i * 100)), Color.LightGreen);
            }

            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                if (new Rectangle(740, 420, 60, 60).Contains(currentMouseState.Position))
                    inQuests = false;
                else if (new Rectangle(660, 140, 60, 60).Contains(currentMouseState.Position) && allowClick)
                {
                    allowClick = false;
                    if (player.Quests.Count > 0)
                    {
                        if (player.Quests[0].OriginTown == Towns.Domus)
                        {
                            domusQuests.Add(player.Quests[0]);
                            player.Quests.RemoveAt(0);
                        }
                    }
                }
                else if (new Rectangle(660, 240, 60, 60).Contains(currentMouseState.Position) && allowClick)
                {
                    allowClick = false;
                    if (player.Quests.Count > 1)
                    {
                        if (player.Quests[1].OriginTown == Towns.Domus)
                        {
                            domusQuests.Add(player.Quests[1]);
                            player.Quests.RemoveAt(1);
                        }
                    }
                }
                else if (new Rectangle(660, 340, 60, 60).Contains(currentMouseState.Position) && allowClick)
                {
                    allowClick = false;
                    if (player.Quests.Count > 2)
                    {
                        if (player.Quests[2].OriginTown == Towns.Domus)
                        {
                            domusQuests.Add(player.Quests[2]);
                            player.Quests.RemoveAt(2);
                        }
                    }
                }
            }
            else if (currentMouseState.LeftButton == ButtonState.Released)
            {
                if (!allowClick) allowClick = true;

                if (new Rectangle(740, 420, 60, 60).Contains(currentMouseState.Position))
                    spriteBatch.Draw(x, new Vector2(740, 420), Color.White);
                else if (new Rectangle(660, 140, 60, 60).Contains(currentMouseState.Position))
                    spriteBatch.Draw(x, new Vector2(660, 140), Color.White);
                else if (new Rectangle(660, 240, 60, 60).Contains(currentMouseState.Position))
                    spriteBatch.Draw(x, new Vector2(660, 240), Color.White);
                else if (new Rectangle(660, 340, 60, 60).Contains(currentMouseState.Position))
                    spriteBatch.Draw(x, new Vector2(660, 340), Color.White);
            }
        }

        private void InventoryScreen(MouseState currentMouseState, Player player)
        {
            spriteBatch.Draw(inventoryScreen, Vector2.Zero, Color.White);

            //spriteBatch.Draw(player.Swords[swordIndex].Texture, new Vector2(40, 220), Color.White);
            if (justOpen)
            {
                swordIndex = player.Swords.IndexOf(player.SelectedSword);
                if (swordIndex == -1 && player.Swords.Count > 0)
                {
                    swordIndex = 0;
                }
                shieldIndex = player.Shields.IndexOf(player.SelectedShield);
                if (shieldIndex == -1 && player.Shields.Count > 0)
                {
                    shieldIndex = 0;
                }
                armorIndex = player.Armor.IndexOf(player.SelectedArmor);
                if (armorIndex == -1 && player.Armor.Count > 0)
                {
                    armorIndex = 0;
                }
                justOpen = false;
                activeItem = null;
            }
            if (activeItem != null)
            {
                spriteBatch.DrawString(nameDisplay, activeItem.Description, new Vector2(140, 415), Color.White);
                spriteBatch.DrawString(nameDisplay, TitleCase(activeItem.AffectedStat.ToString()) + " increase by " + activeItem.Boost.ToString(),
                    new Vector2(140, 430), Color.LightGreen);
            }
            if (swordIndex <= player.Swords.Count - 1 && swordIndex >= 0)
            {
                DisplayItem(player.Swords[swordIndex], new Vector2(40, 220), swordIndex, player);
                if (!ReferenceEquals(player.SelectedSword, player.Swords[swordIndex]))
                {
                    player.SelectedSword = player.Swords[swordIndex];
                    for (int i = 0; i < player.Lineup.Count; i++)
                    {
                        player.Lineup[i].CurrentAttack = player.Lineup[i].Attack + player.SelectedSword.Boost;
                    }
                }
            }
            if (shieldIndex <= player.Shields.Count - 1 && shieldIndex >= 0)
            {
                DisplayItem(player.Shields[shieldIndex], new Vector2(240, 220), shieldIndex, player);
                if (!ReferenceEquals(player.SelectedShield, player.Shields[shieldIndex]))
                {
                    player.SelectedShield = player.Shields[shieldIndex];
                    for (int i = 0; i < player.Lineup.Count; i++)
                    {
                        player.Lineup[i].CurrentDefense = player.Lineup[i].Defense + player.SelectedShield.Boost;
                    }
                }
            }
            if (armorIndex <= player.Armor.Count - 1 && armorIndex >= 0)
            {
                DisplayItem(player.Armor[armorIndex], new Vector2(440, 220), armorIndex, player);
                if (!ReferenceEquals(player.SelectedArmor, player.Armor[armorIndex]))
                {
                    player.SelectedArmor = player.Armor[armorIndex];
                    for (int i = 0; i < player.Lineup.Count; i++)
                    {
                        player.Lineup[i].CurrentHP = player.Lineup[i].MaxHP + player.SelectedArmor.Boost;
                    }
                }
            }

            if (shoeIndex <= player.Shoes.Count - 1 && shoeIndex >= 0)
            {
                DisplayItem(player.Shoes[shoeIndex], new Vector2(640, 220), shoeIndex, player);
                if (!ReferenceEquals(player.SelectedShoes, player.Shoes[shoeIndex]))
                {
                    player.SelectedShoes = player.Shoes[shoeIndex];
                    for (int i = 0; i < player.Lineup.Count; i++)
                    {
                        player.Lineup[i].CurrentSpeed = player.Lineup[i].Speed + player.SelectedShoes.Boost;
                    }
                }
            }

            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                if (new Rectangle(40, 160, 120, 40).Contains(currentMouseState.Position) && allowShift)
                {
                    if (swordIndex != -1 && swordIndex < player.Swords.Count)
                    {
                        swordIndex = ShiftUp(player.Swords, swordIndex);
                        activeItem = player.Swords[swordIndex];
                    }
                }
                else if (new Rectangle(40, 360, 120, 40).Contains(currentMouseState.Position) && allowShift)
                {
                    if (swordIndex != -1 && swordIndex < player.Swords.Count)
                    {
                        swordIndex = ShiftDown(player.Swords, swordIndex);
                        activeItem = player.Swords[swordIndex];
                    }
                }
                else if (new Rectangle(240, 160, 120, 40).Contains(currentMouseState.Position) && allowShift)
                {
                    if (shieldIndex != -1 && shieldIndex < player.Shields.Count)
                    {
                        shieldIndex = ShiftUp(player.Shields, shieldIndex);
                        activeItem = player.Shields[shieldIndex];
                    }
                }
                else if (new Rectangle(240, 360, 120, 40).Contains(currentMouseState.Position) && allowShift)
                {
                    if (shieldIndex != -1 && shieldIndex < player.Shields.Count)
                    {
                        shieldIndex = ShiftDown(player.Shields, shieldIndex);
                        activeItem = player.Shields[shieldIndex];
                    }
                }
                else if (new Rectangle(440, 160, 120, 40).Contains(currentMouseState.Position) && allowShift)
                {
                    if (armorIndex != -1 && armorIndex < player.Armor.Count)
                    {
                        armorIndex = ShiftUp(player.Armor, armorIndex);
                        activeItem = player.Armor[armorIndex];
                    }
                }
                else if (new Rectangle(440, 360, 120, 40).Contains(currentMouseState.Position) && allowShift)
                {
                    if (armorIndex != -1 && armorIndex < player.Armor.Count)
                    {
                        armorIndex = ShiftDown(player.Armor, armorIndex);
                        activeItem = player.Armor[armorIndex];
                    }
                }
                else if (new Rectangle(640, 160, 120, 40).Contains(currentMouseState.Position) && allowShift)
                {
                    if (shoeIndex != -1 && shoeIndex < player.Shoes.Count)
                    {
                        shoeIndex = ShiftUp(player.Shoes, shoeIndex);
                        activeItem = player.Shoes[shoeIndex];
                    }
                }
                else if (new Rectangle(640, 360, 120, 40).Contains(currentMouseState.Position) && allowShift)
                {
                    if (shoeIndex != -1 && shoeIndex < player.Shoes.Count)
                    {
                        shoeIndex = ShiftDown(player.Shoes, shoeIndex);
                        activeItem = player.Shoes[shoeIndex];
                    }
                }
                else if (new Rectangle(740, 420, 60, 60).Contains(currentMouseState.Position))
                {
                    inInventory = false;
                    allowShift = true;
                    justOpen = true;
                }
            }
            if (currentMouseState.LeftButton == ButtonState.Released)
            {
                if (!allowShift)
                    allowShift = true;
                if (new Rectangle(40, 160, 120, 40).Contains(currentMouseState.Position) && allowShift)
                    spriteBatch.Draw(hoverUp, new Vector2(40, 160), Color.White);
                else if (new Rectangle(40, 360, 120, 40).Contains(currentMouseState.Position) && allowShift)
                    spriteBatch.Draw(hoverDown, new Vector2(40, 360), Color.White);
                else if (new Rectangle(240, 160, 120, 40).Contains(currentMouseState.Position) && allowShift)
                    spriteBatch.Draw(hoverUp, new Vector2(240, 160), Color.White);
                else if (new Rectangle(240, 360, 120, 40).Contains(currentMouseState.Position) && allowShift)
                    spriteBatch.Draw(hoverDown, new Vector2(240, 360), Color.White);
                else if (new Rectangle(440, 160, 120, 40).Contains(currentMouseState.Position) && allowShift)
                    spriteBatch.Draw(hoverUp, new Vector2(440, 160), Color.White);
                else if (new Rectangle(440, 360, 120, 40).Contains(currentMouseState.Position) && allowShift)
                    spriteBatch.Draw(hoverDown, new Vector2(440, 360), Color.White);
                else if (new Rectangle(640, 160, 120, 40).Contains(currentMouseState.Position) && allowShift)
                    spriteBatch.Draw(hoverUp, new Vector2(640, 160), Color.White);
                else if (new Rectangle(640, 360, 120, 40).Contains(currentMouseState.Position) && allowShift)
                    spriteBatch.Draw(hoverDown, new Vector2(640, 360), Color.White);
                else if (new Rectangle(740, 420, 60, 60).Contains(currentMouseState.Position))
                    spriteBatch.Draw(x, new Vector2(740, 420), Color.White);
            }
        }

        private void PauseLineupScreen(MouseState currentMouseState, Player player)
        {
            spriteBatch.Draw(lineupPauseScreen, Vector2.Zero, Color.White);

            if (startingFrame <= player.Lineup.Count - 1)
            {
                // draw the pixels to screen
                for (int i = startingFrame; i < startingFrame + 4; i++)
                {
                    if (i <= player.Lineup.Count - 1 && i >= 0)
                    {
                        var pixel = player.Lineup[i];

                        if (player.Lineup.IndexOf(pixel) == startingFrame)
                        {
                            spriteBatch.DrawString(nameDisplay, "Level " + (pixel.Level).ToString(),
                            new Vector2(up1Vector.X, up1Vector.Y - 25), Color.Black);
                            spriteBatch.Draw(pixel.Texture, up1Vector, Color.White);
                            spriteBatch.DrawString(nameDisplay, (lntb.ShortenName(pixel.Name, pixel.ID)),
                                new Vector2(up1Vector.X, up1Vector.Y + 95), Color.Black);
                        }
                        else if (player.Lineup.IndexOf(pixel) == startingFrame + 1)
                        {
                            spriteBatch.DrawString(nameDisplay, "Level " + (pixel.Level).ToString(),
                            new Vector2(up2Vector.X, up2Vector.Y - 25), Color.Black);
                            spriteBatch.Draw(pixel.Texture, up2Vector, Color.White);
                            spriteBatch.DrawString(nameDisplay, (lntb.ShortenName(pixel.Name, pixel.ID)),
                               new Vector2(up2Vector.X, up2Vector.Y + 95), Color.Black);
                        }
                        else if (player.Lineup.IndexOf(pixel) == startingFrame + 2)
                        {
                            spriteBatch.DrawString(nameDisplay, "Level " + (pixel.Level).ToString(),
                            new Vector2(up3Vector.X, up3Vector.Y - 25), Color.Black);
                            spriteBatch.Draw(pixel.Texture, up3Vector, Color.White);
                            spriteBatch.DrawString(nameDisplay, (lntb.ShortenName(pixel.Name, pixel.ID)),
                               new Vector2(up3Vector.X, up3Vector.Y + 95), Color.Black);
                        }
                        else if (player.Lineup.IndexOf(pixel) == startingFrame + 3)
                        {
                            spriteBatch.DrawString(nameDisplay, "Level " + (pixel.Level).ToString(),
                            new Vector2(up4Vector.X, up4Vector.Y - 25), Color.Black);
                            spriteBatch.Draw(pixel.Texture, up4Vector, Color.White);
                            spriteBatch.DrawString(nameDisplay, (lntb.ShortenName(pixel.Name, pixel.ID)),
                               new Vector2(up4Vector.X, up4Vector.Y + 95), Color.Black);
                        }
                    }
                }
            }

            if (currentMouseState.LeftButton == ButtonState.Released)
            {
                if (!allowMove)
                    allowMove = true;
                if (!allowShift)
                    allowShift = true;
                if (upLHalf1.Contains(currentMouseState.Position))
                    spriteBatch.Draw(leftArrow, new Vector2(120, 180), Color.White);
                else if (upRHalf1.Contains(currentMouseState.Position))
                    spriteBatch.Draw(rightArrow, new Vector2(170, 180), Color.White);
                else if (upLHalf2.Contains(currentMouseState.Position))
                    spriteBatch.Draw(leftArrow, new Vector2(260, 180), Color.White);
                else if (upRHalf2.Contains(currentMouseState.Position))
                    spriteBatch.Draw(rightArrow, new Vector2(310, 180), Color.White);
                else if (upLHalf3.Contains(currentMouseState.Position))
                    spriteBatch.Draw(leftArrow, new Vector2(440, 180), Color.White);
                else if (upRHalf3.Contains(currentMouseState.Position))
                    spriteBatch.Draw(rightArrow, new Vector2(490, 180), Color.White);
                else if (upLHalf4.Contains(currentMouseState.Position))
                    spriteBatch.Draw(leftArrow, new Vector2(580, 180), Color.White);
                else if (upRHalf4.Contains(currentMouseState.Position))
                    spriteBatch.Draw(rightArrow, new Vector2(630, 180), Color.White);
                else if (leftUpShift.Contains(currentMouseState.Position))
                    spriteBatch.Draw(shiftLeft, new Vector2(40, 180), Color.White);
                else if (rightUpShift.Contains(currentMouseState.Position))
                    spriteBatch.Draw(shiftRight, new Vector2(700, 180), Color.White);
                else if (new Rectangle(740, 420, 60, 60).Contains(currentMouseState.Position))
                    spriteBatch.Draw(x, new Vector2(740, 420), Color.White);

                if (new Rectangle(120, 180, 100, 100).Contains(currentMouseState.Position))
                {
                    if (startingFrame < player.Lineup.Count)
                        ShowStats(player.Lineup[startingFrame]);
                }
                else if (new Rectangle(260, 180, 100, 100).Contains(currentMouseState.Position))
                {
                    if (startingFrame + 1 < player.Lineup.Count)
                        ShowStats(player.Lineup[startingFrame + 1]);
                }
                else if (new Rectangle(440, 180, 100, 100).Contains(currentMouseState.Position))
                {
                    if (startingFrame + 2 < player.Lineup.Count)
                        ShowStats(player.Lineup[startingFrame + 2]);
                }
                else if (new Rectangle(580, 180, 100, 100).Contains(currentMouseState.Position))
                {
                    if (startingFrame + 3 < player.Lineup.Count)
                        ShowStats(player.Lineup[startingFrame + 3]);
                }
            }

            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                // check if lineup should be moved right or left
                if (rightUpShift.Contains(currentMouseState.Position) && (startingFrame + 4) <= player.Lineup.Count - 1 && (startingFrame + 4) >= 0 && allowShift)
                {
                    startingFrame += 4;
                    allowShift = false;
                }
                else if (leftUpShift.Contains(currentMouseState.Position) && (startingFrame - 4) <= player.Lineup.Count - 1 && (startingFrame - 4) >= 0 && allowShift)
                {
                    startingFrame -= 4;
                    allowShift = false;
                }
                // check if any lineup member should be moved right
                else if (upRHalf1.Contains(currentMouseState.Position) && allowMove)
                    MoveRight(player.Lineup, startingFrame, player.RegenList);
                else if (upRHalf2.Contains(currentMouseState.Position) && allowMove)
                    MoveRight(player.Lineup, startingFrame + 1, player.RegenList);
                else if (upRHalf3.Contains(currentMouseState.Position) && allowMove)
                    MoveRight(player.Lineup, startingFrame + 2, player.RegenList);
                else if (upRHalf4.Contains(currentMouseState.Position) && allowMove)
                    MoveRight(player.Lineup, startingFrame + 3, player.RegenList);
                // check if any lineup member should be moved left 
                else if (upLHalf1.Contains(currentMouseState.Position) && allowMove)
                    MoveLeft(player.Lineup, startingFrame, player.RegenList);
                else if (upLHalf2.Contains(currentMouseState.Position) && allowMove)
                    MoveLeft(player.Lineup, startingFrame + 1, player.RegenList);
                else if (upLHalf3.Contains(currentMouseState.Position) && allowMove)
                    MoveLeft(player.Lineup, startingFrame + 2, player.RegenList);
                else if (upLHalf4.Contains(currentMouseState.Position) && allowMove)
                    MoveLeft(player.Lineup, startingFrame + 3, player.RegenList);
                else if (new Rectangle(740, 420, 60, 60).Contains(currentMouseState.Position))
                {
                    inLineup = false;
                }
            }
        }

        private void ShowStats(IPixel pixel)
        {
            spriteBatch.DrawString(nameDisplay, "Stats for: " + pixel.Name + " " + pixel.ID.ToString(), new Vector2(287, 321), Color.White);
            spriteBatch.DrawString(nameDisplay, "HP - " + pixel.MaxHP.ToString(), new Vector2(350, 347), Color.LightGreen);
            spriteBatch.DrawString(nameDisplay, "SPD - " + pixel.Speed.ToString(), new Vector2(350, 367), Color.Yellow);
            spriteBatch.DrawString(nameDisplay, "DFS - " + pixel.Defense.ToString(), new Vector2(350, 387), Color.Blue);
            spriteBatch.DrawString(nameDisplay, "ATK - " + pixel.Attack.ToString(), new Vector2(350, 407), Color.Red);
        }

        public void ShopScreen(MouseState currentMouseState, Towns town, ContentManager Content, Player player)
        {
            if (inBuy)
            {
                if (player.showBuyTut)
                {
                    player.showBuyTut = ShowTutorial(buyTut, currentMouseState);
                }
                else
                    BuyScreen(currentMouseState, town, Content, player);
            }
            else if (inSell)
            {
                if (player.showSellTut)
                {
                    player.showSellTut = ShowTutorial(sellTut, currentMouseState);
                }
                else
                    SellScreen(currentMouseState, Content, player);
            }
            else
            {
                spriteBatch.Draw(shopScreenTex, Vector2.Zero, Color.White);

                if (currentMouseState.LeftButton == ButtonState.Released)
                {
                    if (!allowClick)
                        allowClick = true;
                    if (new Rectangle(740, 0, 60, 60).Contains(currentMouseState.Position))
                        spriteBatch.Draw(x, new Vector2(740, 0), Color.White);
                    if (new Rectangle(40, 200, 320, 180).Contains(currentMouseState.Position))
                        spriteBatch.Draw(hoverBuy, new Vector2(40, 200), Color.White);
                    else if (new Rectangle(380, 200, 380, 180).Contains(currentMouseState.Position))
                        spriteBatch.Draw(hoverSell, new Vector2(380, 200), Color.White);
                }
                if (currentMouseState.LeftButton == ButtonState.Pressed && allowClick)
                {
                    if (new Rectangle(40, 200, 320, 180).Contains(currentMouseState.Position))
                    {
                        inBuy = true;
                    }
                    else if (new Rectangle(380, 200, 380, 180).Contains(currentMouseState.Position))
                    {
                        inSell = true;
                    }
                    else if (new Rectangle(740, 0, 60, 60).Contains(currentMouseState.Position))
                    {
                        inShop = false;
                        allowClick = false;
                    }
                }
            }
        }

        private void SellScreen(MouseState currentMouseState, ContentManager Content, Player player)
        {
            spriteBatch.Draw(inventoryScreen, Vector2.Zero, Color.White);
            spriteBatch.Draw(sellOverlay, Vector2.Zero, Color.White);

            spriteBatch.DrawString(nameDisplay, "Current Balance", new Vector2(45, 25), Color.White);
            spriteBatch.DrawString(nameDisplay, player.gold.ToString(), new Vector2(70, 55), Color.Gold, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);

            if (activeItem != null)
            {
                spriteBatch.Draw(selectedBox, selectedBoxPos, Color.White);
                spriteBatch.DrawString(nameDisplay, activeItem.Description, new Vector2(100, 405), Color.White);

                spriteBatch.DrawString(nameDisplay, "Sell Price", new Vector2(150, 430), Color.White);
                spriteBatch.DrawString(nameDisplay, activeItem.Price.ToString(), new Vector2(305, 430), Color.Gold, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            }
            if (swordIndex <= player.Swords.Count - 1 && swordIndex >= 0)
            {
                DisplayItem(player.Swords[swordIndex], new Vector2(40, 220), swordIndex, player);
            }
            if (shieldIndex <= player.Shields.Count - 1 && shieldIndex >= 0)
            {
                DisplayItem(player.Shields[shieldIndex], new Vector2(240, 220), shieldIndex, player);
            }
            if (armorIndex <= player.Armor.Count - 1 && armorIndex >= 0)
            {
                DisplayItem(player.Armor[armorIndex], new Vector2(440, 220), armorIndex, player);
            }
            if (shoeIndex <= player.Shoes.Count - 1 && shoeIndex >= 0)
            {
                DisplayItem(player.Shoes[shoeIndex], new Vector2(640, 220), shoeIndex, player);
            }

            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                if (new Rectangle(40, 160, 120, 40).Contains(currentMouseState.Position) && allowShift)
                {
                    swordIndex = ShiftUp(player.Swords, swordIndex);
                }
                else if (new Rectangle(40, 360, 120, 40).Contains(currentMouseState.Position) && allowShift)
                {
                    swordIndex = ShiftDown(player.Swords, swordIndex);
                }
                else if (new Rectangle(20, 200, 160, 160).Contains(currentMouseState.Position))
                {
                    if (swordIndex != -1 && swordIndex < player.Swords.Count)
                    {
                        selectedBoxPos.X = 0; selectedBoxPos.Y = 180;
                        activeItem = player.Swords[swordIndex];
                    }
                }
                else if (new Rectangle(240, 160, 120, 40).Contains(currentMouseState.Position) && allowShift)
                {
                    shieldIndex = ShiftUp(player.Shields, shieldIndex);
                }
                else if (new Rectangle(240, 360, 120, 40).Contains(currentMouseState.Position) && allowShift)
                {
                    shieldIndex = ShiftDown(player.Shields, shieldIndex);
                }
                else if (new Rectangle(220, 200, 160, 160).Contains(currentMouseState.Position))
                {
                    if (shieldIndex != -1 && shieldIndex < player.Shields.Count)
                    {
                        selectedBoxPos.X = 200; selectedBoxPos.Y = 180;
                        activeItem = player.Shields[shieldIndex];
                    }
                }
                else if (new Rectangle(440, 160, 120, 40).Contains(currentMouseState.Position) && allowShift)
                {
                    armorIndex = ShiftUp(player.Armor, armorIndex);
                }
                else if (new Rectangle(440, 360, 120, 40).Contains(currentMouseState.Position) && allowShift)
                {
                    armorIndex = ShiftDown(player.Armor, armorIndex);
                }
                else if (new Rectangle(420, 200, 160, 160).Contains(currentMouseState.Position))
                {
                    if (armorIndex != -1 && armorIndex < player.Armor.Count)
                    {
                        selectedBoxPos.X = 400; selectedBoxPos.Y = 180;
                        activeItem = player.Armor[armorIndex];
                    }
                }
                else if (new Rectangle(640, 160, 120, 40).Contains(currentMouseState.Position) && allowShift)
                {
                    shoeIndex = ShiftUp(player.Shoes, shoeIndex);
                }
                else if (new Rectangle(640, 360, 120, 40).Contains(currentMouseState.Position) && allowShift)
                {
                    shoeIndex = ShiftDown(player.Shoes, shoeIndex);
                }
                else if (new Rectangle(620, 200, 160, 160).Contains(currentMouseState.Position))
                {
                    if (shoeIndex != -1 && shoeIndex < player.Shoes.Count)
                    {
                        selectedBoxPos.X = 600; selectedBoxPos.Y = 180;
                        activeItem = player.Shoes[shoeIndex];
                    }
                }
                else if (new Rectangle(700, 0, 100, 140).Contains(currentMouseState.Position) && allowBuy)
                {
                    if (activeItem != null)
                    {
                        allowBuy = false;
                        player.gold += activeItem.Price;
                        if (activeItem.AffectedStat == Stats.attack)
                        {
                            player.Swords.Remove(activeItem);
                            buyItems.Remove(activeItem);
                        }
                        else if (activeItem.AffectedStat == Stats.defense)
                        {
                            player.Shields.Remove(activeItem);
                            buyItems.Remove(activeItem);
                        }
                        else if (activeItem.AffectedStat == Stats.health)
                        {
                            player.Armor.Remove(activeItem);
                            buyItems.Remove(activeItem);
                        }
                        else if (activeItem.AffectedStat == Stats.speed)
                        {
                            player.Shoes.Remove(activeItem);
                            buyItems.Remove(activeItem);
                        }

                        activeItem = null;
                    }
                }
                else if (new Rectangle(0, 420, 60, 60).Contains(currentMouseState.Position))
                {
                    selectedBoxPos.X = 10000; selectedBoxPos.Y = 10000;
                    activeItem = null;
                    inSell = false;
                }
            }
            if (currentMouseState.LeftButton == ButtonState.Released)
            {
                if (!allowShift)
                    allowShift = true;
                if (!allowBuy)
                    allowBuy = true;
                if (new Rectangle(700, 0, 100, 140).Contains(currentMouseState.Position))
                    spriteBatch.Draw(hoverMoney, new Vector2(700, 0), Color.White);
                else if (new Rectangle(0, 420, 60, 60).Contains(currentMouseState.Position))
                    spriteBatch.Draw(x, new Vector2(0, 420), Color.White);
                if (new Rectangle(40, 160, 120, 40).Contains(currentMouseState.Position))
                    spriteBatch.Draw(hoverUp, new Vector2(40, 160), Color.White);
                else if (new Rectangle(40, 360, 120, 40).Contains(currentMouseState.Position))
                    spriteBatch.Draw(hoverDown, new Vector2(40, 360), Color.White);
                else if (new Rectangle(240, 160, 120, 40).Contains(currentMouseState.Position))
                    spriteBatch.Draw(hoverUp, new Vector2(240, 160), Color.White);
                else if (new Rectangle(240, 360, 120, 40).Contains(currentMouseState.Position))
                    spriteBatch.Draw(hoverDown, new Vector2(240, 360), Color.White);
                else if (new Rectangle(440, 160, 120, 40).Contains(currentMouseState.Position))
                    spriteBatch.Draw(hoverUp, new Vector2(440, 160), Color.White);
                else if (new Rectangle(440, 360, 120, 40).Contains(currentMouseState.Position))
                    spriteBatch.Draw(hoverDown, new Vector2(440, 360), Color.White);
                else if (new Rectangle(640, 160, 120, 40).Contains(currentMouseState.Position))
                    spriteBatch.Draw(hoverUp, new Vector2(640, 160), Color.White);
                else if (new Rectangle(640, 360, 120, 40).Contains(currentMouseState.Position))
                    spriteBatch.Draw(hoverDown, new Vector2(640, 360), Color.White);
            }
        }

        private void BuyScreen(MouseState currentMouseState, Towns town, ContentManager Content, Player player)
        {
            spriteBatch.Draw(buysellScreen, Vector2.Zero, Color.White);

            if (leftTown)
            {
                leftTown = false;
                activeItem = null;
                selectedBoxPos.X = 1000; selectedBoxPos.Y = 1000;
                buyItems.Clear();
                if (town == Towns.Domus)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Item item = domusItems[rand.Next(0, domusItems.Count - 1)];
                        item.Init(Content.Load<Texture2D>(item.TextureStr), i);
                        buyItems.Add(item);
                    }
                }
                else if (town == Towns.Oceanic)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Item item = oceanicItems[rand.Next(0, oceanicItems.Count - 1)];
                        item.Init(Content.Load<Texture2D>(item.TextureStr), i);
                        buyItems.Add(item);
                    }
                }
            }

            spriteBatch.DrawString(nameDisplay, "Current Balance", new Vector2(45, 25), Color.White);
            spriteBatch.DrawString(nameDisplay, player.gold.ToString(), new Vector2(70, 55), Color.Gold, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);

            for (int i = 0; i < buyItems.Count; i++)
            {
                spriteBatch.Draw(buyItems[i].Texture, new Vector2(140 + (200 * i), 200), Color.White);

                if (buyItems[i].AffectedStat == Stats.attack) spriteBatch.DrawString(nameDisplay, "atk +" + buyItems[i].Boost, new Vector2(140 + (200 * i), 182), Color.Black);
                else if (buyItems[i].AffectedStat == Stats.defense) spriteBatch.DrawString(nameDisplay, "dfs +" + buyItems[i].Boost, new Vector2(140 + (200 * i), 182), Color.Black);
                else if (buyItems[i].AffectedStat == Stats.health) spriteBatch.DrawString(nameDisplay, "hp +" + buyItems[i].Boost, new Vector2(140 + (200 * i), 182), Color.Black);
                else if (buyItems[i].AffectedStat == Stats.speed) spriteBatch.DrawString(nameDisplay, "spd +" + buyItems[i].Boost, new Vector2(140 + (200 * i), 182), Color.Black);

                spriteBatch.DrawString(nameDisplay, buyItems[i].Name, new Vector2(140 + (200 * i), 325), Color.Black);
            }

            if (activeItem != null)
            {
                spriteBatch.Draw(selectedBox, selectedBoxPos, Color.White);
                spriteBatch.DrawString(nameDisplay, activeItem.Description, new Vector2(120, 400), Color.White);
                spriteBatch.DrawString(nameDisplay, "Cost", new Vector2(215, 430), Color.White);
                spriteBatch.DrawString(nameDisplay, activeItem.Price.ToString(), new Vector2(305, 430), Color.Gold, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            }

            if (currentMouseState.LeftButton == ButtonState.Released)
            {
                if (!allowBuy)
                    allowBuy = true;

                if (new Rectangle(700, 0, 100, 140).Contains(currentMouseState.Position))
                    spriteBatch.Draw(hoverMoney, new Vector2(700, 0), Color.White);
                else if (new Rectangle(0, 420, 60, 60).Contains(currentMouseState.Position))
                    spriteBatch.Draw(x, new Vector2(0, 420), Color.White);
            }
            else if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                if (new Rectangle(120, 180, 160, 160).Contains(currentMouseState.Position))
                {
                    if (0 <= buyItems.Count - 1)
                    {
                        activeItem = buyItems[0];
                        selectedBoxPos.X = 100; selectedBoxPos.Y = 160;
                    }
                }
                else if (new Rectangle(320, 180, 160, 160).Contains(currentMouseState.Position))
                {
                    if (1 <= buyItems.Count - 1)
                    {
                        activeItem = buyItems[1];
                        selectedBoxPos.X = 300; selectedBoxPos.Y = 160;
                    }
                }
                else if (new Rectangle(520, 180, 160, 160).Contains(currentMouseState.Position))
                {
                    if (2 <= buyItems.Count - 1)
                    {
                        activeItem = buyItems[2];
                        selectedBoxPos.X = 500; selectedBoxPos.Y = 160;
                    }
                }
                else if (new Rectangle(0, 420, 60, 60).Contains(currentMouseState.Position))
                {
                    selectedBoxPos.X = 10000; selectedBoxPos.Y = 10000;
                    inBuy = false;
                }
                else if (new Rectangle(700, 0, 100, 140).Contains(currentMouseState.Position) && allowBuy)
                {
                    allowBuy = false;
                    if (player.gold >= activeItem.Price)
                    {
                        player.gold -= activeItem.Price;
                        if (activeItem.AffectedStat == Stats.attack)
                        {
                            if (player.Swords.Count > 0)
                                activeItem.Id = player.Swords[player.Swords.Count - 1].Id + 1;
                            player.Swords.Add(activeItem);
                            buyItems.Remove(activeItem);
                        }
                        else if (activeItem.AffectedStat == Stats.defense)
                        { 
                            if (player.Shields.Count > 0)
                                activeItem.Id = player.Shields[player.Shields.Count - 1].Id + 1;
                            player.Shields.Add(activeItem);
                            buyItems.Remove(activeItem);
                        }
                        else if (activeItem.AffectedStat == Stats.health)
                        {
                            if (player.Armor.Count > 0)
                                activeItem.Id = player.Armor[player.Armor.Count - 1].Id + 1;
                            player.Armor.Add(activeItem);
                            buyItems.Remove(activeItem);
                        }
                        else if (activeItem.AffectedStat == Stats.speed)
                        {
                            if (player.Shoes.Count > 0)
                                activeItem.Id = player.Shoes[player.Shoes.Count - 1].Id + 1;
                            player.Shoes.Add(activeItem);
                            buyItems.Remove(activeItem);
                        }

                        activeItem = null;
                    }
                }
            }
        }

        private void ProgressBar(float progress, float goal, Vector2 position)
        {
            float percent = (progress / goal) * 100;

            for (int i = 0; i < percent; i++)
            {
                spriteBatch.Draw(progressBarFull, new Vector2(position.X + (i * 4), position.Y), Color.White);
            }
            float temp = 100 - percent;
            for (int i = 0; i < (100 - percent); i++)
            {
                spriteBatch.Draw(progressBarEmpty, new Vector2((position.X + (percent * 4)) + (i * 4), position.Y), Color.White);
            }
        }

        private string TitleCase(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }

        private int ShiftUp(List<Item> ls, int index)
        {
            allowShift = false;
            if (index + 1 <= ls.Count - 1 && index + 1 >= 0)
            {
                index++;
            }
            return index;
        }

        private int ShiftDown(List<Item> ls, int index)
        {
            allowShift = false;
            if (index - 1 >= 0 && index - 1 <= ls.Count - 1)
            {
                index--;
            }
            return index;
        }

        private void DisplayItem(Item item, Vector2 place, int index, Player player)
        {
            try
            {
                spriteBatch.DrawString(nameDisplay, item.Name + " " + item.Id.ToString(), new Vector2(place.X - 20,   place.Y + 122), Color.Black);

                if (item.AffectedStat == Stats.attack) spriteBatch.DrawString(nameDisplay, "atk +" + item.Boost, new Vector2(place.X, place.Y - 18), Color.Black);
                else if (item.AffectedStat == Stats.defense) spriteBatch.DrawString(nameDisplay, "dfs +" + item.Boost, new Vector2(place.X, place.Y - 18), Color.Black);
                else if (item.AffectedStat == Stats.health) spriteBatch.DrawString(nameDisplay, "hp +" + item.Boost, new Vector2(place.X, place.Y - 18), Color.Black);
                else if (item.AffectedStat == Stats.speed) spriteBatch.DrawString(nameDisplay, "spd +" + item.Boost, new Vector2(place.X, place.Y - 18), Color.Black);

                //spriteBatch.DrawString(nameDisplay, index.ToString(), new Vector2(place.X, place.Y - 18), Color.Black);
                spriteBatch.Draw(item.Texture, place, Color.White);
            }
            catch(System.ArgumentNullException)
            {
                if (player.Swords.Contains(item))
                    player.Swords.RemoveAt(index);
            }
        }

        private void MoveRight(List<IPixel> ls, int index, List<IPixel> secondary = null)
        {
            allowMove = false;
            if (ls.Count - 1 >= index + 1)
            {
                IPixel pix = ls[index];
                ls.RemoveAt(index);
                ls.Insert(index + 1, pix);
            }
            if (secondary != null && secondary.Count - 1 >= index + 1)
            {
                IPixel pix = secondary[index];
                secondary.RemoveAt(index);
                secondary.Insert(index + 1, pix);
            }
        }

        private void MoveLeft(List<IPixel> ls, int index, List<IPixel> secondary = null)
        {
            allowMove = false;
            if (ls.Count - 1 >= index && index - 1 >= 0)
            {
                IPixel pix = ls[index];
                ls.RemoveAt(index);
                ls.Insert(index - 1, pix);
            }
            if (secondary != null && secondary.Count - 1 >= index && index - 1 >= 0)
            {
                IPixel pix = secondary[index];
                secondary.RemoveAt(index);
                secondary.Insert(index - 1, pix);
            }
        }
    }
}
    