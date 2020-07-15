using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PixelRPG.Items;
using PixelRPG.Moves;
using PixelRPG.Pixels;
using PixelRPG.Pixels.Air;
using PixelRPG.Pixels.Earth;
using PixelRPG.Pixels.Fire;
using PixelRPG.Pixels.Water;
using PixelRPG.Quests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PixelRPG
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Keyboard states used to determine key presses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        // Mouse states used for menus and such
        MouseState currentMouseState;
        MouseState previousMouseState;

        // used for battle timing and such
        int elapsedTime;

        // My Fields
        Player player;
        Animation downAnimation = new Animation();
        Animation restingAnimation = new Animation();
        Animation leftAnimation = new Animation();
        Animation rightAnimation = new Animation();
        Animation upAnimation = new Animation();

        // the current enemy lineup
        //List<IPixel> enemyLineup = new List<IPixel>();
        List<object> enemyLineup = new List<object>();

        // a unique ID number assigned to each number as it is inited
        int IdNum = 0;

        // stores the order in which pixels attack
        List<IPixel> attackOrder = new List<IPixel>();

        IPixel starter;

        // bool inTown to check whether or not to draw the village interface
        public bool inLineup = false;
        bool showStart = true;
        bool wait = false;

        bool chooseStarterBool = false;
        bool showStory = false;

        bool newPixels = false;

        bool waitL = false;
        bool waitI = false;
        bool waitQ = false;
        bool waitP = false;

        // random number generator to get random encounters
        Random random = new Random();

        // UI
        Texture2D lineup_tray;
        SpriteFont nameDisplay;
        Texture2D move_tray;
        Texture2D battle;
        Texture2D pauseButton;
        Texture2D hoverLeave;
        Texture2D hoverGuild;
        Texture2D hoverLineup;
        Texture2D hoverShop;
        Texture2D hoverPause;
        Texture2D hoverSave;
        Texture2D battleTut, guildTut, lineupTut;
        Texture2D startScreen, hoverStart;
        Texture2D chooseStarter;
        Texture2D hoverFire, hoverEarth, hoverWater, hoverAir;
        Texture2D story, blackX;
        Texture2D warrantyScreen, hoverYes, hoverNo;

        CurrentTile currentTile = new CurrentTile();

        List<Point> boundaryList = new List<Point>();

        LineupToolbox lntb;
        BattleToolbox btb;
        Toolbox tb;

        public Game1()
        { 
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;

            // Set up the player and load all the necessary animations
            player = new Player();
            player = Player.Load();
            if (player != null && player.foundAreas == null)
            {
                player.foundAreas = new List<SPoint>();
            }
            if (player == null)
            {
                player = new Player();
                newPixels = true;
                player.tileX = 1;
                player.tileY = 0;
                player.CurrentTown = Towns.Domus;

                chooseStarterBool = true;
                showStory = true;

                player.showBattleTut = true;
                player.showBuyTut = true;
                player.showGuildTut = true;
                player.showLineupTut = true;
                player.showPauseLineupTut = true;
                player.showInventoryTut = true;
                player.showQuestsTut = true;
                player.showSellTut = true;
            }
            if (!player.foundAreas.Contains(new SPoint(player.tileX, player.tileY)))
                player.foundAreas.Add(new SPoint(player.tileX, player.tileY));

            // Load all the player's possible animations
            downAnimation.Init(Content.Load<Texture2D>("Graphics\\p_spritesheet_04"), player.position, 65, 124, 5, 60, Color.White, 1.0f, true);
            rightAnimation.Init(Content.Load<Texture2D>("Graphics\\p_r_spritesheet_00"), player.position, 73, 127, 7, 50, Color.White, 1.0f, true);
            restingAnimation.Init(Content.Load<Texture2D>("Graphics\\p_gif"), player.position, 66, 124, 1, 1, Color.White, 1.0f, true);
            leftAnimation.Init(Content.Load<Texture2D>("Graphics\\p_l_spritesheet_00"), player.position, 72, 127, 7, 50, Color.White, 1.0f, true);
            upAnimation.Init(Content.Load<Texture2D>("Graphics\\p_u_spritesheet_00"), player.position, 65, 124, 5, 60, Color.White, 1.0f, true);

            //BattleEnd += teamDead;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Init the player so that all the animations are correct
            player.Init(restingAnimation, rightAnimation, leftAnimation, upAnimation, downAnimation);

            if (!newPixels)
            {
                if (player.Swords.Count > 0)
                {
                    for (int i = 0; i < player.Swords.Count; i++)
                    {
                        player.Swords[i].Init(Content.Load<Texture2D>(player.Swords[i].TextureStr), i);
                    }
                }
                if (player.SelectedSword != null)
                {
                    player.SelectedSword.Init(Content.Load<Texture2D>(player.SelectedSword.TextureStr), player.SelectedSword.Id);
                }
                if (player.Shields.Count > 0)
                {
                    for (int i = 0; i < player.Shields.Count; i++)
                    {
                        player.Shields[i].Init(Content.Load<Texture2D>(player.Shields[i].TextureStr), i);
                    }
                }
                if (player.SelectedShield != null)
                {
                    player.SelectedShield.Init(Content.Load<Texture2D>(player.SelectedShield.TextureStr), player.SelectedShield.Id);
                }
                if (player.Armor.Count > 0)
                {
                    for (int i = 0; i < player.Armor.Count; i++)
                    {
                        player.Armor[i].Init(Content.Load<Texture2D>(player.Armor[i].TextureStr), i);
                    }
                }
                if (player.SelectedArmor != null)
                {
                    player.SelectedArmor.Init(Content.Load<Texture2D>(player.SelectedArmor.TextureStr), player.SelectedArmor.Id);
                }
                if (player.Shoes.Count > 0)
                {
                    for (int i = 0; i < player.Shoes.Count; i++)
                    {
                        player.Shoes[i].Init(Content.Load<Texture2D>(player.Shoes[i].TextureStr), i);
                    }
                }
                if (player.SelectedShoes != null)
                {
                    player.SelectedShoes.Init(Content.Load<Texture2D>(player.SelectedShoes.TextureStr), player.SelectedShoes.Id);
                }
            }
            //else
            //{ 
            //    Stick stick = new Stick();
            //    stick.Init(Content.Load<Texture2D>(stick.TextureStr), 0);
            //    player.Swords.Add(stick);
            //    player.SelectedSword = stick;

            //    PotLid potlid = new PotLid();
            //    potlid.Init(Content.Load<Texture2D>(potlid.TextureStr), 0);
            //    player.Shields.Add(potlid);
            //    player.SelectedShield = potlid;

            //    LeatherArmor leatherarmor = new LeatherArmor();
            //    leatherarmor.Init(Content.Load<Texture2D>(leatherarmor.TextureStr), 0);
            //    player.Armor.Add(leatherarmor);
            //    player.SelectedArmor = leatherarmor;

            //    LeatherSandals sandals = new LeatherSandals();
            //    sandals.Init(Content.Load<Texture2D>(sandals.TextureStr), 0);
            //    player.Shoes.Add(sandals);
            //    player.SelectedShoes = sandals;
            //}

            // UI Elements
            lineup_tray = Content.Load<Texture2D>("Graphics\\UI\\battle_lineup_tray_02");
            //nameDisplay = Content.Load<SpriteFont>("NameDisplay");
            nameDisplay = Content.Load<SpriteFont>("Graphics\\testpixel");
            move_tray = Content.Load<Texture2D>("Graphics\\UI\\battle_move_tray_02");
            battle = Content.Load<Texture2D>("Graphics\\UI\\battle_02");
            pauseButton = Content.Load<Texture2D>("Graphics\\UI\\pause_button");
            hoverLeave = Content.Load<Texture2D>("Graphics\\UI\\hover_leave");
            hoverGuild = Content.Load<Texture2D>("Graphics\\UI\\hover_guild");
            hoverLineup = Content.Load<Texture2D>("Graphics\\UI\\hover_lineup");
            hoverShop = Content.Load<Texture2D>("Graphics\\UI\\hover_shop");
            hoverPause = Content.Load<Texture2D>("Graphics\\UI\\hover_pause");
            hoverSave = Content.Load<Texture2D>("Graphics\\UI\\hover_save");
            startScreen = Content.Load<Texture2D>("Graphics\\UI\\start_screen");
            hoverStart = Content.Load<Texture2D>("Graphics\\UI\\hover_start");

            battleTut = Content.Load<Texture2D>("Graphics\\UI\\Tutorials\\battle");
            guildTut = Content.Load<Texture2D>("Graphics\\UI\\Tutorials\\guild");
            lineupTut = Content.Load<Texture2D>("Graphics\\UI\\Tutorials\\lineup");

            story = Content.Load<Texture2D>("Graphics\\UI\\story");
            blackX = Content.Load<Texture2D>("Graphics\\UI\\black_x");

            chooseStarter = Content.Load<Texture2D>("Graphics\\UI\\choose_starter");
            hoverFire = Content.Load<Texture2D>("Graphics\\UI\\hover_fire");
            hoverEarth = Content.Load<Texture2D>("Graphics\\UI\\hover_earth");
            hoverWater = Content.Load<Texture2D>("Graphics\\UI\\hover_water");
            hoverAir = Content.Load<Texture2D>("Graphics\\UI\\hover_air");

            warrantyScreen = Content.Load<Texture2D>("Graphics\\UI\\warranty_screen");
            hoverYes = Content.Load<Texture2D>("Graphics\\UI\\yes_hover");
            hoverNo = Content.Load<Texture2D>("Graphics\\UI\\no_hover");

            // Load the background
            currentTile.CurrentTextureStr = "Graphics\\Map\\" + player.tileX.ToString() + player.tileY.ToString();
            currentTile.CurrentTexture = Content.Load<Texture2D>(currentTile.CurrentTextureStr);

            // init all toolboxes
            lntb = new LineupToolbox(nameDisplay, inLineup, Content);
            btb = new BattleToolbox(ref elapsedTime, random, currentTile, ref player, spriteBatch, Content,
                lineup_tray, nameDisplay, move_tray, battle, player.tileX, player.tileY, ref player.RegenList);
            tb = new Toolbox(spriteBatch, Content, nameDisplay, lntb);

            // Set the current region; this is used to determine enemyLineup
            tb.UpdateRegionType(player.tileX, player.tileY, currentTile);

            if (!newPixels)
                IdNum = tb.ReInitPixels(player.Lineup, player.Backup, IdNum, Content, player);

            // Init all the moves for all the pixels
            for (int i = 0; i < player.Lineup.Count; i++)
            {
                IPixel pixel = player.Lineup[i];
                MovesInit(ref pixel);
            }

            for (int i = 0; i < player.Backup.Count; i++)
            {
                IPixel pixel = player.Lineup[i];
                MovesInit(ref pixel);
            }

            // make the player.RegenList
            if (player.RegenList.Count > 0)
                player.RegenList.Clear();
            foreach (IPixel pixel in player.Lineup)
            {
                player.RegenList.Add(pixel);
            }

            boundaryList = tb.homeIslandBoundaries;

            currentTile.RightTextureStr = tb.GetRightStr(player.tileX, player.tileY);
            currentTile.UpTextureStr = tb.GetUpStr(player.tileX, player.tileY);
            currentTile.DownTextureStr = tb.GetDownStr(player.tileX, player.tileY);
            currentTile.LeftTextureStr = tb.GetLeftStr(player.tileX, player.tileY);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (!this.IsActive)
            {
                return;
            }
            // get current state of keyboard and save this state to previous state
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            // get current state of mouse and save this state to previous state
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            //if (player.showWarranty)
            //{
            //    return;
            //}
            if (showStart)
            {
                return;
            }

            if (showStory)
            {
                return;
            }

            if (chooseStarterBool)
            {
                return;
            }

            // update the elapsedTime counter if during a battle or showing the battle results screen
            if (btb.inBattle || btb.playerWon || btb.battleEnd) btb.elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            else if (!player.inTown && !btb.inBattle)
            {
                if (!tb.inMap && currentMouseState.LeftButton == ButtonState.Pressed && new Rectangle(0, 0, 60, 60).Contains(currentMouseState.Position))
                {
                    tb.inPause = true;
                }
                // MAP LOGIC 
                if (!tb.inPause && currentKeyboardState.IsKeyDown(Keys.M))
                {
                    //tb.inMap = true;
                    wait = true;
                }
                if (!tb.inPause && wait && !tb.inMap && currentKeyboardState.IsKeyUp(Keys.M))
                {
                    tb.inMap = true;
                    wait = false;
                }
                if (!tb.inPause && wait && tb.inMap && currentKeyboardState.IsKeyUp(Keys.M))
                {
                    tb.inMap = false;
                    wait = false;
                }
                if (!tb.inPause && tb.inMap && currentKeyboardState.IsKeyDown(Keys.M))
                {
                    //tb.inMap = false;
                    wait = true;
                }

                // LINEUP LOGIC
                if (!tb.inPause && !tb.inMap && currentKeyboardState.IsKeyDown(Keys.L))
                {
                    waitL = true;
                    //tb.inPause = false;
                    //tb.inLineup = false;
                }
                if (!tb.inMap && tb.inLineup && currentKeyboardState.IsKeyDown(Keys.L))
                {
                    waitL = true;
                    //tb.inPause = false;
                    //tb.inLineup = false;
                }
                if (waitL && currentKeyboardState.IsKeyUp(Keys.L))
                {
                    waitL = false;
                    tb.inPause = !tb.inPause;
                    tb.inLineup = !tb.inLineup;
                }

                // INVENTORY LOGIC
                if (!tb.inPause && !tb.inMap && currentKeyboardState.IsKeyDown(Keys.I))
                {
                    waitI = true;
                }
                if (!tb.inMap && tb.inInventory && currentKeyboardState.IsKeyDown(Keys.I))
                {
                    waitI = true;
                }
                if (waitI && currentKeyboardState.IsKeyUp(Keys.I))
                {
                    waitI = false;
                    tb.inInventory = !tb.inInventory;
                    tb.inPause = !tb.inPause;
                }

                // QUEST LOGIC
                if (!tb.inPause && !tb.inMap && currentKeyboardState.IsKeyDown(Keys.Q))
                {
                    waitQ = true;
                }
                if (!tb.inMap && tb.inQuests && currentKeyboardState.IsKeyDown(Keys.Q))
                {
                    waitQ = true;
                }
                if (waitQ && currentKeyboardState.IsKeyUp(Keys.Q))
                {
                    waitQ = false;
                    tb.inQuests = !tb.inQuests;
                    tb.inPause = !tb.inPause;
                }

                // PAUSE LOGIC
                if (!tb.inPause && !tb.inMap && currentKeyboardState.IsKeyDown(Keys.P))
                {
                    waitP = true;
                }
                if (!tb.inMap && tb.inPause && currentKeyboardState.IsKeyDown(Keys.P))
                {
                    waitP = true;
                }
                if (waitP && currentKeyboardState.IsKeyUp(Keys.P))
                {
                    waitP = false;
                    tb.inPause = !tb.inPause;
                }
            }

            // if showing the In Town screen
            if (player.inTown)
            {
                // reset the player position so that when they leave town they are at the gates
                if (player.CurrentTown == Towns.Domus)
                {
                    player.position.X = 450; player.position.Y = 217;
                    player.tileX = 1; player.tileY = 0;

                    if (player.Quests.Count > 0)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (i < player.Quests.Count && player.Quests[i].TravelTarget != Towns.none)
                            {
                                if (Towns.Domus == player.Quests[i].TravelTarget && player.Quests[i].Progress + 1 <= player.Quests[i].Goal)
                                {
                                    player.Quests[i].Progress++;
                                }
                            }
                        }
                    }
                }
                else if (player.CurrentTown == Towns.Oceanic)
                {
                    player.position.X = 320 - (player.Width + 1);
                    player.tileX = 8; player.tileY = 0;

                    if (player.Quests.Count > 0)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (i < player.Quests.Count && player.Quests[i].TravelTarget != Towns.none)
                            {
                                if (Towns.Oceanic == player.Quests[i].TravelTarget && player.Quests[i].Progress + 1 <= player.Quests[i].Goal)
                                {
                                    player.Quests[i].Progress++;
                                }
                            }
                        }
                    }
                }

                // regen all pixels
                if (player.Lineup.Count < player.RegenList.Count)
                {
                    player.Lineup.Clear();
                    foreach (IPixel pixel in player.RegenList)
                    {
                        if (player.RegenList.IndexOf(pixel) == 0)
                        {
                            pixel.Position = new Vector2(100, 332);
                        }
                        else if (player.RegenList.IndexOf(pixel) == 1)
                        {
                            pixel.Position = new Vector2(290, 332);
                        }
                        else if (player.RegenList.IndexOf(pixel) == 2)
                        {
                            pixel.Position = new Vector2(480, 332);
                        }
                        else if (player.RegenList.IndexOf(pixel) == 3)
                        {
                            pixel.Position = new Vector2(880, 332);
                        }
                        else if (player.RegenList.IndexOf(pixel) >= 4)
                        {
                            pixel.Position = new Vector2((880 + player.RegenList.IndexOf(pixel)), 332);
                        }
                        player.Lineup.Add(pixel);
                    }
                }
                
                // heal all of the player's lineup
                foreach (IPixel pixel in player.Lineup)
                {
                    if (pixel.CurrentHP < pixel.MaxHP)
                    {
                        pixel.CurrentHP = pixel.MaxHP;
                        if (player.SelectedArmor != null) pixel.CurrentHP += player.SelectedArmor.Boost;
                    }   
                }

                // If the player pressed the LEAVE button
                if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (new Rectangle(16, 128, 369, 145).Contains(currentMouseState.Position) && player.inTown && !lntb.inLineup && !tb.inShop && !tb.inGuild)
                    {
                        player.inTown = false;
                        tb.leftTown = true;
                    }

                    if (new Rectangle(0, 0, 60, 60).Contains(currentMouseState.Position) && player.inTown && !lntb.inLineup && !tb.inShop && !tb.inGuild) player.Save(player);

                    if (new Rectangle(16, 304, 415, 143).Contains(currentMouseState.Position) && player.inTown && !lntb.inLineup && !tb.inShop && !tb.inGuild) lntb.inLineup = true;
                    if (new Rectangle(448, 304, 336, 144).Contains(currentMouseState.Position) && player.inTown && !tb.inShop && !lntb.inLineup && !tb.inGuild) tb.inShop = true;
                    else if (new Rectangle(416, 128, 368, 144).Contains(currentMouseState.Position) && player.inTown && !tb.inShop && !lntb.inLineup && !tb.inGuild)
                    {
                        for (int i = 0; i < player.Quests.Count; i++)
                        {
                            IQuest q = player.Quests[i];
                            if (q.Progress == q.Goal 
                                && (player.completedQuests.Count == 0 || 
                                (player.completedQuests.Where(quest => quest.Description == q.Description).Count() == 0)))
                            {
                                player.completedQuests.Add(q);
                            }
                        }

                        //tb.complete = player.completedQuests;
                        if (player.completedQuests.Count > 0)
                        {
                            foreach (var q in player.completedQuests)
                            {
                                tb.complete.Add(q);
                            }
                        }
                        tb.allowClick = false;
                        tb.inGuild = true;
                    }
                }
            }

            // checks if the player is touching Domus
            else if (new Rectangle(510, 207, 288, 239).Contains(player.position) && player.tileX == 1 && player.tileY == 0)
            {
                player.inTown = true;
                player.CurrentTown = Towns.Domus;
            }
            else if (new Rectangle(320, 180, 260, 200).Contains(player.position) && player.tileX == 8 && player.tileY == 0)
            {
                player.inTown = true;
                player.CurrentTown = Towns.Oceanic;
            }
            else if (new Rectangle(100, 0, 380, 160).Contains(player.position) && player.tileX == 8 && player.tileY == 3)
            {
                if (player.completedQuests.Count >= 6)
                {
                    btb.inBattle = true;
                    btb.wasBossBattle = true;
                    //bossBattle = true;
                    //spriteBatch.Begin();
                    //btb.BattleSequence(currentMouseState, true);
                    //spriteBatch.End();
                }
            }

            if (btb.inBattle)
            {
                btb.CheckClicks(currentMouseState);
            }

            // update the player's position and texture
            UpdatePlayer(gameTime);

            base.Update(gameTime);
        }

        private void UpdatePlayer(GameTime gameTime)
        {
            player.Update(gameTime);
            player.Moving = false;

            if (tb.inMap)
            {
                return;
            }
            if (btb.inBattle)
            {
                return;
            }

            // Use the Keyboard / Dpad
            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A))
            {
                player.position.X -= player.MoveSpeed;
                player.Moving = true;
                player.Direction = Directions.left;
                btb.GetEncounter();
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
            {
                player.position.X += player.MoveSpeed;
                player.Moving = true;
                player.Direction = Directions.right;
                btb.GetEncounter();
            }

            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W))
            {
                player.position.Y -= player.MoveSpeed;
                player.Moving = true;
                player.Direction = Directions.up;
                btb.GetEncounter();
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S))
            {
                player.position.Y += player.MoveSpeed;
                player.Moving = true;
                player.Direction = Directions.down;
                btb.GetEncounter();
            }

            if (player.position.X + player.CurrentAnimation.FrameWidth >= GraphicsDevice.Viewport.Width) 
            {
                if (!boundaryList.Contains(new Point(player.tileX + 1, player.tileY)))
                {
                    if (!(string.IsNullOrEmpty(currentTile.RightTextureStr)))
                    {
                        try
                        {
                            player.tileX++;
                            currentTile.CurrentTextureStr = currentTile.RightTextureStr;
                            currentTile.CurrentTexture = Content.Load<Texture2D>(currentTile.RightTextureStr);

                            if (!player.foundAreas.Contains(new SPoint(player.tileX, player.tileY)))
                            {
                                player.foundAreas.Add(new SPoint(player.tileX, player.tileY));
                            }

                            player.position.X = 1;

                            tb.UpdatePeripheralStr(player.tileX, player.tileY, currentTile);
                            tb.UpdateRegionType(player.tileX, player.tileY, currentTile);
                            tb.UpdateSpawnRate(currentTile, player.tileX, player.tileY);
                        }
                        catch(Exception)
                        {
                            player.tileX--;
                            currentTile.CurrentTexture = Content.Load<Texture2D>("Graphics\\Map\\" + player.tileX.ToString() + player.tileY.ToString());
                            tb.UpdatePeripheralStr(player.tileX, player.tileY, currentTile);
                            tb.UpdateRegionType(player.tileX, player.tileY, currentTile);
                            tb.UpdateSpawnRate(currentTile, player.tileX, player.tileY);
                        }
                    }
                }
            }
            if (player.position.X <= 0)
            {
                if (!boundaryList.Contains(new Point(player.tileX - 1, player.tileY)))
                {
                    if (!(string.IsNullOrEmpty(currentTile.LeftTextureStr)))
                    {
                        try
                        {
                            player.tileX--;
                            currentTile.CurrentTextureStr = currentTile.LeftTextureStr;
                            currentTile.CurrentTexture = Content.Load<Texture2D>(currentTile.CurrentTextureStr);

                            if (!player.foundAreas.Contains(new SPoint(player.tileX, player.tileY)))
                            {
                                player.foundAreas.Add(new SPoint(player.tileX, player.tileY));
                            }

                            player.position.X = GraphicsDevice.Viewport.Width - player.CurrentAnimation.FrameWidth - 1;

                            tb.UpdatePeripheralStr(player.tileX, player.tileY, currentTile);
                            tb.UpdateRegionType(player.tileX, player.tileY, currentTile);
                            tb.UpdateSpawnRate(currentTile, player.tileX, player.tileY);
                        }
                        catch (Exception)
                        {
                            player.tileX++;
                            currentTile.CurrentTexture = Content.Load<Texture2D>("Graphics\\Map\\" + player.tileX.ToString() + player.tileY.ToString());
                            tb.UpdatePeripheralStr(player.tileX, player.tileY, currentTile);
                            tb.UpdateRegionType(player.tileX, player.tileY, currentTile);
                            tb.UpdateSpawnRate(currentTile, player.tileX, player.tileY);
                        }
                    }
                }
            }
            if (player.position.Y <= 0)
            {
                if (!boundaryList.Contains(new Point(player.tileX, player.tileY + 1)))
                {
                    if (!string.IsNullOrEmpty(currentTile.UpTextureStr))
                    {
                        try
                        {
                            player.tileY++;
                            currentTile.CurrentTextureStr = currentTile.UpTextureStr;
                            currentTile.CurrentTexture = Content.Load<Texture2D>(currentTile.UpTextureStr);

                            if (!player.foundAreas.Contains(new SPoint(player.tileX, player.tileY)))
                            {
                                player.foundAreas.Add(new SPoint(player.tileX, player.tileY));
                            }

                            player.position.Y = GraphicsDevice.Viewport.Width - player.CurrentAnimation.FrameHeight - 1;

                            tb.UpdatePeripheralStr(player.tileX, player.tileY, currentTile);
                            tb.UpdateRegionType(player.tileX, player.tileY, currentTile);
                            tb.UpdateSpawnRate(currentTile, player.tileX, player.tileY);
                        }
                        catch (Exception)
                        {
                            player.tileY--;
                            currentTile.CurrentTexture = Content.Load<Texture2D>("Graphics\\Map\\" + player.tileX.ToString() + player.tileY.ToString());
                            tb.UpdatePeripheralStr(player.tileX, player.tileY, currentTile);
                            tb.UpdateRegionType(player.tileX, player.tileY, currentTile);
                            tb.UpdateSpawnRate(currentTile, player.tileX, player.tileY);
                        }
                    }
                } 
            }
            else if (player.position.Y + player.CurrentAnimation.FrameHeight >= GraphicsDevice.Viewport.Height)
            {
                if(!boundaryList.Contains(new Point(player.tileX, player.tileY - 1)))
                {
                    if (!string.IsNullOrEmpty(currentTile.DownTextureStr))
                    {
                        try
                        {
                            player.tileY--;
                            currentTile.CurrentTextureStr = currentTile.DownTextureStr;
                            currentTile.CurrentTexture = Content.Load<Texture2D>(currentTile.DownTextureStr);

                            if (!player.foundAreas.Contains(new SPoint(player.tileX, player.tileY)))
                            {
                                player.foundAreas.Add(new SPoint(player.tileX, player.tileY));
                            }

                            player.position.Y = 1;

                            tb.UpdatePeripheralStr(player.tileX, player.tileY, currentTile);
                            tb.UpdateRegionType(player.tileX, player.tileY, currentTile);
                            tb.UpdateSpawnRate(currentTile, player.tileX, player.tileY);
                        }
                        catch (Exception)
                        {
                            player.tileY++;
                            currentTile.CurrentTexture = Content.Load<Texture2D>("Graphics\\Map\\" + player.tileX.ToString() + player.tileY.ToString());
                            tb.UpdatePeripheralStr(player.tileX, player.tileY, currentTile);
                            tb.UpdateRegionType(player.tileX, player.tileY, currentTile);
                            tb.UpdateSpawnRate(currentTile, player.tileX, player.tileY);
                        }
                    }
                }
            }

            // Make sure that the player doesn't go out of bounds
            player.position.X = MathHelper.Clamp(player.position.X, 0, GraphicsDevice.Viewport.Width - player.CurrentAnimation.FrameWidth);
            player.position.Y = MathHelper.Clamp(player.position.Y, 0, GraphicsDevice.Viewport.Height - player.CurrentAnimation.FrameHeight);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            //if (player.showWarranty)
            //{
            //    spriteBatch.Draw(warrantyScreen, Vector2.Zero, Color.White);
            //    if (currentMouseState.LeftButton == ButtonState.Released)
            //    {
            //        if (wait)
            //        {
            //            wait = false;
            //            player.showWarranty = false;
            //            player.Save(player);
            //        }
            //        if (new Rectangle(20, 280, 300, 180).Contains(currentMouseState.Position))
            //            spriteBatch.Draw(hoverYes, new Vector2(20, 280), Color.White);
            //        if (new Rectangle(540, 280, 240, 180).Contains(currentMouseState.Position))
            //            spriteBatch.Draw(hoverNo, new Vector2(540, 280), Color.White);
            //    }
            //    else if (currentMouseState.LeftButton == ButtonState.Pressed)
            //    {
            //        if (new Rectangle(20, 280, 300, 180).Contains(currentMouseState.Position))
            //            wait = true;
            //        else if (new Rectangle(540, 280, 240, 180).Contains(currentMouseState.Position))
            //            Environment.Exit(0);
            //        //showStart = false;
            //    }
            //    spriteBatch.End();
            //    return;
            //}

            if (showStart)
            {
                spriteBatch.Draw(startScreen, Vector2.Zero, Color.White);

                if (currentMouseState.LeftButton == ButtonState.Released)
                {
                    if (wait)
                    {
                        wait = false;
                        showStart = false;
                    }
                    if (new Rectangle(460, 320, 320, 100).Contains(currentMouseState.Position))
                        spriteBatch.Draw(hoverStart, new Vector2(460, 320), Color.White);
                }
                else if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (new Rectangle(460, 320, 320, 100).Contains(currentMouseState.Position))
                        wait = true;
                        //showStart = false;
                }

                spriteBatch.End();
                return;
            }

            if (showStory)
            {
                spriteBatch.Draw(story, Vector2.Zero, Color.White);

                if (currentMouseState.LeftButton == ButtonState.Released)
                {
                    if (wait)
                    {
                        wait = false;
                        showStory = false;
                    }
                    if (new Rectangle(0, 0, 60, 60).Contains(currentMouseState.Position))
                        spriteBatch.Draw(blackX, new Vector2(0, 0), Color.White);
                }
                else if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (new Rectangle(0, 0, 60, 60).Contains(currentMouseState.Position))
                        wait = true;
                    //showStart = false;
                }

                spriteBatch.End();
                return;
            }

            if (chooseStarterBool)
            {
                spriteBatch.Draw(chooseStarter, Vector2.Zero, Color.White);

                if (currentMouseState.LeftButton == ButtonState.Released)
                {
                    if (wait)                    {
                        IdNum = tb.InitNewPixels(player.Lineup, IdNum, Content, player, starter);
                        foreach (var p in player.Lineup)
                        {
                            player.RegenList.Add(p);
;                       }

                        player.Lineup[0].Init((Content.Load<Texture2D>(player.Lineup[0].TextureStr)), new Vector2(100, 332), 0, player, false);
                        IPixel pix = player.Lineup[0];
                        MovesInit(ref pix);

                        player.Save(player);
                        wait = false;
                        chooseStarterBool = false;
                    }
                    if (new Rectangle(20, 160, 160, 160).Contains(currentMouseState.Position))
                        spriteBatch.Draw(hoverFire, new Vector2(20, 160), Color.White);
                    else if (new Rectangle(220, 160, 160, 160).Contains(currentMouseState.Position))
                        spriteBatch.Draw(hoverEarth, new Vector2(202, 160), Color.White);
                    else if (new Rectangle(420, 160, 160, 160).Contains(currentMouseState.Position))
                        spriteBatch.Draw(hoverWater, new Vector2(402, 160), Color.White);
                    else if (new Rectangle(620, 160, 160, 160).Contains(currentMouseState.Position))
                        spriteBatch.Draw(hoverAir, new Vector2(620, 160), Color.White);
                }
                else if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (new Rectangle(20, 160, 160, 160).Contains(currentMouseState.Position))
                    {
                        starter = new FireBaby();
                        wait = true;
                    }
                    else if (new Rectangle(220, 160, 160, 160).Contains(currentMouseState.Position))
                    {
                        starter = new EarthBaby();
                        wait = true;
                    }
                    else if (new Rectangle(420, 160, 160, 160).Contains(currentMouseState.Position))
                    {
                        starter = new WaterBaby();
                        wait = true;
                    }
                    else if (new Rectangle(620, 160, 160, 160).Contains(currentMouseState.Position))
                    {
                        starter = new AirBaby();
                        wait = true;
                    }
                }

                spriteBatch.End();
                return;
            }

            spriteBatch.Draw(currentTile.CurrentTexture, Vector2.Zero, Color.White);
            spriteBatch.Draw(pauseButton, Vector2.Zero, Color.White);
            spriteBatch.DrawString(nameDisplay, "Press 'M' to open/close map", new Vector2(61, 1), Color.White);

            if (!player.inTown && !tb.inPause && !btb.battleEnd)
            {
                if (currentMouseState.LeftButton == ButtonState.Released && new Rectangle(0, 0, 60, 60).Contains(currentMouseState.Position))
                    spriteBatch.Draw(hoverPause, Vector2.Zero, Color.White);
            }

            // this is to be called after a battle is over if the player won
            if (btb.battleEnd)
            {
                // keep track of how much time has passed so that it exits
                //btb.elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                player.inTown = btb.BattleResults(currentMouseState);
                if (player.CurrentTown == Towns.Domus && player.inTown)
                {
                    currentTile.CurrentTexture = Content.Load<Texture2D>("Graphics\\Map\\10");
                    player.tileX = 1;
                    player.tileY = 0;
                }
                else if (player.CurrentTown == Towns.Oceanic && player.inTown)
                {
                    currentTile.CurrentTexture = Content.Load<Texture2D>("Graphics\\Map\\80");
                    player.tileX = 8;
                    player.tileY = 0;
                }
            }
            else if (lntb.inLineup)
            {
                if (player.showLineupTut)
                {
                    player.showLineupTut = tb.ShowTutorial(lineupTut, currentMouseState);
                }
                else
                    lntb.LineupScreen(spriteBatch, currentMouseState, Content, player);
            }
            else if (tb.inShop)
            {
                tb.ShopScreen(currentMouseState, player.CurrentTown, Content, player);
            }
            else if (tb.inGuild)
            {
                if (player.showGuildTut)
                {
                    player.showGuildTut = tb.ShowTutorial(guildTut, currentMouseState);
                }
                else
                    tb.GuildScreen(currentMouseState, player);
            }
            else if (tb.inMap)
            {
                tb.ShowMap(Islands.home, player, Content);
            }
            // If we are in town, don't draw the player, just draw the village UI
            else if (player.inTown)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("Graphics\\UI\\village_ui"), Vector2.Zero, Color.White);
                if (currentMouseState.LeftButton == ButtonState.Released)
                {
                    if (new Rectangle(16, 128, 369, 145).Contains(currentMouseState.Position))
                        spriteBatch.Draw(hoverLeave, new Vector2(16, 128), Color.White);
                    else if (new Rectangle(416, 128, 368, 144).Contains(currentMouseState.Position))
                        spriteBatch.Draw(hoverGuild, new Vector2(416, 128), Color.White);
                    else if (new Rectangle(16, 304, 415, 143).Contains(currentMouseState.Position))
                        spriteBatch.Draw(hoverLineup, new Vector2(16, 304), Color.White);
                    else if (new Rectangle(448, 304, 336, 144).Contains(currentMouseState.Position))
                        spriteBatch.Draw(hoverShop, new Vector2(448, 304), Color.White);
                    else if (new Rectangle(0, 0, 60, 60).Contains(currentMouseState.Position))
                        spriteBatch.Draw(hoverSave, new Vector2(0, 0), Color.White);
                }
                elapsedTime = 0;
            }
            else if (tb.inPause)
            {
                player.inTown = tb.PauseScreen(currentMouseState, player, currentTile, Content);
            }
            // If we aren't in battle, just draw the player normally
            // NPCs and other things go here as well
            else if (!btb.inBattle)
            {
                player.Draw(spriteBatch);
            }
            // Call BattleSequence if inBattle is true
            else
            {
                // show the BATTLE graphic for 3 seconds when we encounter a battle
                //try
                //{
                    if (btb.showBattle)
                    {
                        spriteBatch.Draw(btb.battle, new Vector2(0, 0), Color.White);

                        if (btb.elapsedTime >= 3000) btb.showBattle = false;
                    }
                    else if (!btb.showBattle)
                    {
                        if (player.showBattleTut)
                        {
                            player.showBattleTut = tb.ShowTutorial(battleTut, currentMouseState);
                        }
                        else
                            btb.BattleSequence(currentMouseState);
                    }
                //}
                //catch (Exception e)
                //{
                //    btb.totalXP = 0;
                //    btb.inBattle = false;
                //    btb.battleEnd = false;
                //    btb.showBattle = true;

                //    btb.numEnemyDead.Clear();
                //    btb.enemyLineup.Clear();
                //    btb.numPlayerDead.Clear();
                //}
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Used to display staggered text on the screen; usually used within a for loop and arbitary types
        /// </summary>
        /// <param name="str_in">What string to be written</param>
        /// <param name="index">Used to determine by how much the text should be staggered</param>
        /// <param name="x">X-coordinate where first string should be drawn</param>
        /// <param name="y">Y-coordinate where first string should be drawn</param>
        /// <param name="xdif">How much to stagger the text left-right</param>
        /// <param name="ydif">How much to stagger the text up-down</param>
        private void DisplayText(string str_in, int index, int x, int y, int xdif, int ydif)
        {
            if (index == 0)
            {
                spriteBatch.DrawString(nameDisplay, str_in, new Vector2(x, y), Color.White);
            }
            else if (index == 1)
            {
                spriteBatch.DrawString(nameDisplay, str_in, new Vector2(x + xdif, y + ydif), Color.White);
            }
            else if (index == 2)
            {
                spriteBatch.DrawString(nameDisplay, str_in, new Vector2(x + (xdif * 2), y + (ydif * 2)), Color.White);
            }
            else if (index == 3)
            {
                spriteBatch.DrawString(nameDisplay, str_in, new Vector2(x + (xdif * 3), y + (ydif * 3)), Color.White);
            }
            else if (index == 4)
            {
                spriteBatch.DrawString(nameDisplay, str_in, new Vector2(x + (xdif * 4), y + (ydif * 4)), Color.White);
            }
            else if (index == 5)
            {
                spriteBatch.DrawString(nameDisplay, str_in, new Vector2(x + (xdif * 5), y + (ydif * 5)), Color.White);
            }
        }

        /// <summary>
        /// Initializes all the moves that are not null of the passed-in pixel
        /// </summary>
        /// <param name="pixel">Pixel whose moves will be initalized</param>
        private void MovesInit(ref IPixel pixel)
        {
            if (pixel.Move1 != null)
                pixel.Move1.Init(Content.Load<Texture2D>(pixel.Move1.TextureStr), pixel);
            if (pixel.Move2 != null)
                pixel.Move2.Init(Content.Load<Texture2D>(pixel.Move2.TextureStr), pixel);
            if (pixel.Move3 != null)
                pixel.Move3.Init(Content.Load<Texture2D>(pixel.Move3.TextureStr), pixel);
            if (pixel.Move4 != null)
                pixel.Move4.Init(Content.Load<Texture2D>(pixel.Move4.TextureStr), pixel);
        }
    }
}
