using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PixelRPG.Items;
using PixelRPG.Moves;
using PixelRPG.Pixels;
using PixelRPG.Pixels.Dark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG
{
    class BattleToolbox
    {
        /**** LISTS *****/
        public List<object> enemyLineup = new List<object>();
        // stores the order in which pixels attack
        List<IPixel> attackOrder = new List<IPixel>();
        List<IPixel> regenList;
        List<IPixel> evolveList = new List<IPixel>();
        public List<IPixel> numPlayerDead = new List<IPixel>();
        public List<IPixel> numEnemyDead = new List<IPixel>();

        /***** INTS *****/
        // a unique ID number assigned to each number as it is inited
        int IdNum = 0;
        int randTarget = 1;
        int enemyDamage = 0;
        public int totalXP;
        int avgPlayerLevel = 0;
        int tileX, tileY;
        public int elapsedTime;
        // this is used to determine the first pixel that should be drawn on the results screen
        int startingFrame = 0;
        // used to determine where in evolveList we are
        int evolveIndex = 0;
        // used to store the display value of the gold
        int goldInt = 0;
        // used to display and do damage to enemies
        int damageDisplay = 0;
        // used if attack hits all enemies
        int dmg1 = 0;
        int dmg2 = 0;
        int dmg3 = 0;

        /***** BOOLS *****/
        // used for determining if move1, 2, 3, or 4 were chosen
        bool move1Clicked = false;
        bool move2Clicked = false;
        bool move3Clicked = false;
        bool move4Clicked = false;
        // used for determining if capture 1, 2, or 3 were clicked
        bool capture1Clicked = false;
        bool capture2Clicked = false;
        bool capture3Clicked = false;
        // used for determining if enemy 1, 2, or 3 were targeted
        bool enemy1Targeted = false;
        bool enemy2Targeted = false;
        bool enemy3Targeted = false;
        // bool inBattle to check whether or not to draw Player and Pixels
        public bool inBattle = false;
        public bool battleEnd = false;
        // bool used to say whether or not enemy should choose a new move
        bool chooseMove = true;
        // used to check whether all enemies or all players are dead
        bool enemyDead = false;
        bool playerDead = false;
        // used for displaying graphics after battle
        public bool playerWon = false;
        // used to see if player is allowed to try to capture an enemy
        bool allowCapture = true;
        // used to determine whether or not to show the battle graphic
        public bool showBattle = true;
        // used to display results
        bool allowShift = true;
        // used to see if the loot screen should be shown
        bool showLoot = false;
        bool oneTimeOnly = true;
        bool oneTimeOnly2 = true;
        bool allowClick;
        public bool wasBossBattle = false;

        /***** RECTANGLES *****/
        // rects used to determine if user clicked a move
        Rectangle move1Rect = new Rectangle(110, 320, 280, 50);
        Rectangle move2Rect = new Rectangle(410, 320, 280, 50);
        Rectangle move3Rect = new Rectangle(110, 380, 280, 50);
        Rectangle move4Rect = new Rectangle(410, 380, 280, 50);
        // rects used to determine if user wants to attempt to capture a monster
        Rectangle capture1Rect = new Rectangle(50, 70, 36, 52);
        Rectangle capture2Rect = new Rectangle(240, 70, 36, 52);
        Rectangle capture3Rect = new Rectangle(430, 70, 36, 52);
        // rects used to determine if user clicked a target
        Rectangle enemy1Rect = new Rectangle(100, 50, 120, 120);
        Rectangle enemy2Rect = new Rectangle(290, 50, 120, 120);
        Rectangle enemy3Rect = new Rectangle(480, 50, 120, 120);

        /***** IPIXEL, IMOVE, PLAYER *****/
        // the following variables are used in battles, usually by the enemy AI
        IPixel p = null;
        IMove m = null;
        Player player;

        /***** UI ELEMENTS *****/
        Texture2D lineup_tray;
        SpriteFont nameDisplay;
        Texture2D move_tray;
        public Texture2D battle;
        CurrentTile currentTile;
        SpriteBatch spriteBatch;
        ContentManager Content;
        Texture2D results_screen;
        Texture2D evolveScreen;
        Texture2D lootScreen;
        SpriteFont goldDisplay;
        Texture2D battleBackground;
        Texture2D hpBarGood, hpBarMedium, hpBarBad, hpBarEmpty;
        Texture2D hoverMove;
        Texture2D hoverInfo;
        Texture2D x;
        Texture2D hoverContinue;
        Texture2D hoverDamage;
        Texture2D hoverPass;
        Texture2D passIcon;
        Texture2D end;
        Texture2D blackX;

        /***** MISCELLANEOUS *****/
        event EventHandler<EventArgs> BattleEnd;
        Random random;
        string captureDisplayStr = "";
        Loot results;

        public BattleToolbox(ref int e, Random r, CurrentTile c, ref Player p, SpriteBatch s, ContentManager cn,
            Texture2D l_t, SpriteFont n, Texture2D m_t, Texture2D b, int tX, int tY, ref List<IPixel> rL)
        {
            elapsedTime = e;
            random = r;
            currentTile = c;
            player = p;
            spriteBatch = s;
            Content = cn;
            lineup_tray = l_t;
            nameDisplay = n;
            move_tray = m_t;
            battle = b;
            tileX = tX;
            tileY = tY;
            BattleEnd += TeamDead;
            regenList = rL;

            results_screen = Content.Load<Texture2D>("Graphics\\UI\\results_screen");
            evolveScreen = Content.Load<Texture2D>("Graphics\\UI\\evolve_screen");
            lootScreen = Content.Load<Texture2D>("Graphics\\UI\\loot_screen");
            goldDisplay = Content.Load<SpriteFont>("Graphics\\goldDisplay");
            hpBarGood = Content.Load<Texture2D>("Graphics\\UI\\hp_bar_good");
            hpBarMedium = Content.Load<Texture2D>("Graphics\\UI\\hp_bar_medium");
            hpBarBad = Content.Load<Texture2D>("Graphics\\UI\\hp_bar_bad");
            hpBarEmpty = Content.Load<Texture2D>("Graphics\\UI\\hp_bar_empty");
            hoverMove = Content.Load<Texture2D>("Graphics\\UI\\hover_move");
            hoverInfo = Content.Load<Texture2D>("Graphics\\UI\\hover_info");
            x = Content.Load<Texture2D>("Graphics\\UI\\x");
            hoverContinue = Content.Load<Texture2D>("Graphics\\UI\\hover_continue");
            hoverDamage = Content.Load<Texture2D>("Graphics\\UI\\hover_damage");
            hoverPass = Content.Load<Texture2D>("Graphics\\UI\\hover_pass");
            passIcon = Content.Load<Texture2D>("Graphics\\UI\\pass_icon");
            end = Content.Load<Texture2D>("Graphics\\UI\\end");
            blackX = Content.Load<Texture2D>("Graphics\\UI\\black_x");
        }

        public void BattleSequence(MouseState currentMouseState, bool bossBattle = false)
        {
            /* ***** INITALIZE ***** */
            // if the enemyLineup is empty, we need to populate it
            if (enemyLineup.Count == 0)
            {
                elapsedTime = 0;
                // get the enemies for this region from Regions.cs
                // TODO: make this a for loop later and make it get a random number of enemies

                //wasBossBattle = bossBattle;

                if (wasBossBattle)
                {
                    battleBackground = Content.Load<Texture2D>("Graphics\\UI\\battlebackgrounds\\boss");
                }
                else if (currentTile.CurrentRegion == RegionType.plain)
                {
                    battleBackground = Content.Load<Texture2D>("Graphics\\UI\\battlebackgrounds\\plain1");
                }
                else if (currentTile.CurrentRegion == RegionType.forest)
                {
                    battleBackground = Content.Load<Texture2D>("Graphics\\UI\\battlebackgrounds\\forest1");
                }
                else if (currentTile.CurrentRegion == RegionType.beach)
                {
                    battleBackground = Content.Load<Texture2D>("Graphics\\UI\\battlebackgrounds\\beach1");
                }
                else if (currentTile.CurrentRegion == RegionType.ice)
                {
                    battleBackground = Content.Load<Texture2D>("Graphics\\UI\\battlebackgrounds\\ice1");
                }
                else if (currentTile.CurrentRegion == RegionType.mountain)
                {
                    battleBackground = Content.Load<Texture2D>("Graphics\\UI\\battlebackgrounds\\mountain1");
                }
                else if (currentTile.CurrentRegion == RegionType.volcano)
                {
                    battleBackground = Content.Load<Texture2D>("Graphics\\UI\\battlebackgrounds\\volcano1");
                }

                if (!wasBossBattle)
                {
                    int num = 1;

                    if (currentTile.SpawnRate <= 800)
                    {
                        if (player.tileX + player.tileY >= 4)
                            num = random.Next(1, ((player.tileX + player.tileY) / 2));
                    }


                    for (int i = 0; i < num; i++)
                    {
                        Rarity enemyRarity = Rarity.common;
                        if (random.Next(0, 4) == 1)
                            enemyRarity = Rarity.rare;
                        if (random.Next(0, 99) == 7)
                            enemyRarity = Rarity.legendary;

                        IPixel pix = Regions.GetEnemies(currentTile.CurrentIsland, currentTile.CurrentRegion, enemyRarity, player.tileX, player.tileY);
                        if (pix != null)
                            enemyLineup.Add(pix);
                        else
                            inBattle = false;
                    }
                }
                else
                {
                    var ty = new Tytus();
                    enemyLineup.Add(ty);
                    var tuccc = new Tuccc();
                    enemyLineup.Add(tuccc);
                    var seanusy = new Seanusy();
                    enemyLineup.Add(seanusy);
                    var jerome = new Jerome();
                    enemyLineup.Add(jerome);
                    var yourmaker = new TheMaker();
                    enemyLineup.Add(yourmaker);
                }

                // this gets the average level of the player's lineup so that the enemies are a fair fight
                avgPlayerLevel = 0;
                foreach (IPixel pixel in player.Lineup)
                {
                    avgPlayerLevel += pixel.Level;
                }

                avgPlayerLevel = avgPlayerLevel / player.Lineup.Count;

                foreach (IPixel pixel in enemyLineup)
                {
                    pixel.CurrentAttack = pixel.Attack;
                    pixel.CurrentDefense = pixel.Defense;
                    pixel.CurrentSpeed = pixel.Speed;
                }

                // set up the attack order here so that it only runs once 
                GetAttackOrder();

                for (int i = 0; i < player.Lineup.Count; i++)
                {
                    IPixel pixel = player.Lineup[i];
                    MovesInit(ref pixel);
                    if (i < 3)
                    {
                        pixel.Position = new Vector2((100 + 190 * i), 332);
                    }
                    else if (i == 3)
                    {
                        pixel.Position = new Vector2(1080, 332);
                    }
                    else
                    {
                        pixel.Position = new Vector2((880 + 190 * i - 3), 332);
                    }
                }
            }

            /* ***** UPDATE & DRAW ***** */
            // make sure the background and tray are drawn first
            //spriteBatch.Draw(currentTile.CurrentTexture, Vector2.Zero, Color.Black);
            spriteBatch.Draw(battleBackground, Vector2.Zero, Color.White);
            spriteBatch.Draw(lineup_tray, new Vector2(50, 300), Color.White);

            // write the current attackOrder to the screen
            for (int i = 0; i < attackOrder.Count; i++)
            {
                IPixel pixel = attackOrder[i];

                bool inPlayer = false;
                if (player.Lineup.Contains(pixel)) inPlayer = true;

                DisplayText((pixel.Name + " " + pixel.ID.ToString()), i, 600, 20, 0, 20, inPlayer);
            }

            IEnumerable<IPixel> result = player.Lineup.Where(pixel => pixel.CurrentHP == 0);
            if (result.Count() != 0) playerDead = true;

            // iterates over enemyLineup and inits the pixels if they aren't already
            Vector2 currentPos = Vector2.Zero;

            for (int i = 0; i < enemyLineup.Count; i++)
            {
                // this is used in this loop to easily refer to the pixel in question
                IPixel pixel = (IPixel)enemyLineup[i];
                // checks if the pixel has been inited or not 
                if (pixel.Texture == null || (pixel.Position.X == 0 && pixel.Position.Y == 0))
                {
                    // sets the pixels' positions based on their index
                    if (i < 3)
                    {
                        currentPos = new Vector2((100 + 190 * i), 50);
                    }
                    else if (i == 3)
                    {
                        currentPos = new Vector2(1080, 50);
                    }
                    else
                    {
                        currentPos = new Vector2((880 + 190 * i - 3), 50);
                    }

                    // init the pixel, increment the IdNum, and init the pixel's moves
                    pixel.Init(Content.Load<Texture2D>(pixel.TextureStr), currentPos, IdNum, player, true);
                    pixel.EnemyLevelUp(avgPlayerLevel - pixel.Level);

                    IdNum++;
                    MovesInit(ref pixel);

                    totalXP += pixel.XPValue;
                }

                if (pixel.CurrentHP > 0)
                {
                    // draw the pixel along with their name below and their HP above
                    pixel.Draw(spriteBatch);
                    // capture button
                    spriteBatch.Draw(Content.Load<Texture2D>("Graphics\\UI\\capture_icon"), new Vector2(pixel.Position.X - 50, pixel.Position.Y + 20), Color.White);
                    // pixel's name
                    spriteBatch.DrawString(nameDisplay, (pixel.Name + " " + pixel.ID.ToString()),
                        new Vector2(pixel.Position.X, pixel.Position.Y + 92), Color.White);
                    // pixel's HP
                    HpBar(pixel.CurrentHP, pixel.MaxHP, new Vector2(pixel.Position.X - 3, pixel.Position.Y - 18));
                    spriteBatch.DrawString(nameDisplay, (pixel.CurrentHP + "/" + pixel.MaxHP),
                        new Vector2(pixel.Position.X, pixel.Position.Y - 18), Color.White);
                }
            }

            for (int j = 0; j < enemyLineup.Count; j++)
            {
                IPixel p = (IPixel)enemyLineup[j];

                if (p.CurrentHP <= 0 && !numEnemyDead.Contains(p)) numEnemyDead.Add(p);
            }

            if (numEnemyDead.Count > 0)
            {
                enemyDead = true;

                foreach (var enemy in numEnemyDead)
                    spriteBatch.DrawString(nameDisplay, enemy.Name + " " + enemy.ID.ToString() + " has fainted.",
                        new Vector2(75, 200 + 15 * numEnemyDead.IndexOf(enemy)), Color.White);

                if (elapsedTime > 6000)
                {
                    enemyDead = false;
                    elapsedTime = 0;
                    oneTimeOnly2 = true;
                    numEnemyDead.Clear();
                }
                else if (elapsedTime > 4000 && oneTimeOnly2)
                {
                    oneTimeOnly2 = false;

                    foreach (IPixel pixel in numEnemyDead)
                    {
                        if (player.Quests.Count > 0)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (i < player.Quests.Count && player.Quests[i].KillTarget != null)
                                {
                                    if (pixel.Name == player.Quests[i].KillTarget && player.Quests[i].Progress + 1 <= player.Quests[i].Goal)
                                    {
                                        player.Quests[i].Progress++;
                                    }
                                }
                            }
                        }

                        Vector2 pos = pixel.Position;
                        int placeAtkOrder = attackOrder.IndexOf(pixel);
                        attackOrder.Remove(pixel);
                        enemyLineup.Remove(pixel);

                        if (enemyLineup.Count > 2)
                        {
                            IPixel p = (IPixel)enemyLineup[2];
                            p.Position = pos;
                            attackOrder.Insert(placeAtkOrder, p);
                            //GetAttackOrder();
                        }
                    }

                    if (enemyLineup.Count <= 0)
                    {
                        enemyLineup.Clear();
                        playerWon = true;
                        BattleEnd?.Invoke(this, new EventArgs());
                        return;
                    }
                }
            }

            for (int j = 0; j < player.Lineup.Count; j++)
            {
                IPixel p = player.Lineup[j];

                if (p.CurrentHP <= 0 && !numPlayerDead.Contains(p)) numPlayerDead.Add(p);
            }

            if (numPlayerDead.Count > 0)
            {
                playerDead = true;

                foreach (var enemy in numPlayerDead)
                    spriteBatch.DrawString(nameDisplay, enemy.Name + " " + enemy.ID.ToString() + " has fainted.",
                        new Vector2(75, 200 + 15 * numPlayerDead.IndexOf(enemy)), Color.White);

                if (elapsedTime > 6000)
                {
                    playerDead = false;
                    elapsedTime = 0;

                    numPlayerDead.Clear();
                    oneTimeOnly = true;
                }
                else if (elapsedTime > 4000 && oneTimeOnly)
                {
                    oneTimeOnly = false;

                    foreach (IPixel pixel in numPlayerDead)
                    {
                        Vector2 pos = pixel.Position;
                        int placeAtkOrder = attackOrder.IndexOf(pixel);
                        attackOrder.Remove(pixel);
                        player.Lineup.Remove(pixel);

                        if (player.Lineup.Count > 2)
                        {
                            player.Lineup[2].Position = pos;
                            attackOrder.Insert(placeAtkOrder, player.Lineup[2]);
                            //GetAttackOrder();
                        }
                    }

                    if (player.Lineup.Count <= 0)
                    {
                        playerWon = false;
                        totalXP = 0;
                        BattleEnd?.Invoke(this, new EventArgs());
                        return;
                    }
                }
            }

            // iterates over player's lineup and draws each because they are inited above
            for (int i = 0; i < player.Lineup.Count; i++)
            {
                IPixel pixel = player.Lineup[i];
          
                if (!(pixel.CurrentHP <= 0))
                {
                    // draw the pixel along with their name below and their HP above
                    pixel.Draw(spriteBatch);
                    // pixel's name
                    spriteBatch.DrawString(nameDisplay, (pixel.Name + " " + pixel.ID.ToString()),
                        new Vector2(pixel.Position.X, pixel.Position.Y + 92), Color.White);
                    // pixel's HP
                    HpBar(pixel.CurrentHP, pixel.MaxHP, new Vector2(pixel.Position.X, pixel.Position.Y - 18));
                    spriteBatch.DrawString(nameDisplay, (pixel.CurrentHP + "/" + pixel.MaxHP),
                        new Vector2(pixel.Position.X + 2, pixel.Position.Y - 18), Color.White);
                }
            }

            if (attackOrder.Count > 0 && player.Lineup.Contains(attackOrder[0]) && !playerDead && !enemyDead && attackOrder[0].CurrentHP > 0)
            {
                // draw the lineup_tray back over the pixels to make room for the moves
                spriteBatch.Draw(move_tray, new Vector2(50, 300), Color.White);
                spriteBatch.Draw(passIcon, new Vector2(732, 431), Color.White);
                // write the currently attacking pixel's name above the move_tray
                spriteBatch.DrawString(nameDisplay, attackOrder[0].Name + " " + attackOrder[0].ID.ToString() + "'s move",
                    new Vector2(200, 275), Color.White);

                if (currentMouseState.LeftButton == ButtonState.Released)
                {
                    if (!allowClick) allowClick = true;
                    if (new Rectangle(374, 321, 15, 14).Contains(currentMouseState.Position) && !move1Clicked && !move2Clicked && !move3Clicked && !move4Clicked)
                    {
                        if (attackOrder[0].Move1 != null)
                            DisplayInfo(374, 321, attackOrder[0].Move1);
                    }
                    else if (new Rectangle(674, 321, 15, 14).Contains(currentMouseState.Position) && !move1Clicked && !move2Clicked && !move3Clicked && !move4Clicked)
                    {
                        if (attackOrder[0].Move2 != null)
                            DisplayInfo(674, 321, attackOrder[0].Move2);
                    }
                    else if (new Rectangle(374, 381, 15, 14).Contains(currentMouseState.Position) && !move1Clicked && !move2Clicked && !move3Clicked && !move4Clicked)
                    {
                        if (attackOrder[0].Move3 != null)
                            DisplayInfo(374, 381, attackOrder[0].Move3);
                    }
                    else if (new Rectangle(674, 381, 15, 14).Contains(currentMouseState.Position) && !move1Clicked && !move2Clicked && !move3Clicked && !move4Clicked)
                    {
                        if (attackOrder[0].Move4 != null)
                            DisplayInfo(674, 381, attackOrder[0].Move4);
                    }
                    else if (move1Rect.Contains(currentMouseState.Position) && !move1Clicked && !move2Clicked && !move3Clicked && !move4Clicked)
                        spriteBatch.Draw(hoverMove, new Vector2(move1Rect.X, move1Rect.Y), Color.White);
                    else if (move2Rect.Contains(currentMouseState.Position) && !move1Clicked && !move2Clicked && !move3Clicked && !move4Clicked)
                        spriteBatch.Draw(hoverMove, new Vector2(move2Rect.X, move2Rect.Y), Color.White);
                    else if (move3Rect.Contains(currentMouseState.Position) && !move1Clicked && !move2Clicked && !move3Clicked && !move4Clicked)
                        spriteBatch.Draw(hoverMove, new Vector2(move3Rect.X, move3Rect.Y), Color.White);
                    else if (move4Rect.Contains(currentMouseState.Position) && !move1Clicked && !move2Clicked && !move3Clicked && !move4Clicked)
                        spriteBatch.Draw(hoverMove, new Vector2(move4Rect.X, move4Rect.Y), Color.White);
                    else if (new Rectangle(732, 431, 68, 49).Contains(currentMouseState.Position) && !move1Clicked && !move2Clicked && !move3Clicked && !move4Clicked)
                        spriteBatch.Draw(hoverPass, new Vector2(732, 431), Color.White);
                }
                // write move1's name and damage on first box
                if (attackOrder[0].Move1 != null)
                {
                    DisplayMove(move1Rect, attackOrder[0].Move1.ToString());
                    //spriteBatch.DrawString(nameDisplay, attackOrder[0].Move1.ToString(),
                        //new Vector2(150, 335), Color.White);
                }
                // write move2's name and damage on second box
                if (attackOrder[0].Move2 != null)
                {
                    DisplayMove(move2Rect, attackOrder[0].Move2.ToString());
                    //spriteBatch.DrawString(nameDisplay, attackOrder[0].Move2.ToString(),
                        //new Vector2(421, 335), Color.White);
                }
                // write move3's name and damage on third box
                if (attackOrder[0].Move3 != null)
                {
                    DisplayMove(move3Rect, attackOrder[0].Move3.ToString());
                    //spriteBatch.DrawString(nameDisplay, attackOrder[0].Move3.ToString(),
                      //  new Vector2(150, 400), Color.White);
                }
                if (attackOrder[0].Move4 != null)
                {
                    DisplayMove(move4Rect, attackOrder[0].Move4.ToString());
                    //spriteBatch.DrawString(nameDisplay, attackOrder[0].Move4.ToString(),
                      //  new Vector2(421, 400), Color.White);
                }

                // set to which move the player will select
                IMove pm = null;
                // set to which pixel the player will target
                IPixel pp = null;

                if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (new Rectangle(732, 431, 68, 49).Contains(currentMouseState.Position) && 
                        !(capture1Clicked || capture2Clicked || capture3Clicked || move1Clicked || move2Clicked || move3Clicked || move4Clicked) && allowClick)
                    {
                        allowClick = false;
                        attackOrder.Add(attackOrder[0]);
                        attackOrder.RemoveAt(0);
                        elapsedTime = 0;
                    }
                }

                if (!wasBossBattle)
                {
                    if (capture1Clicked && enemyLineup.Count >= 1)
                    {
                        CheckCapture(enemyLineup.IndexOf(GetEnemyTargeted(100, 50)));
                    }
                    else if (capture2Clicked && enemyLineup.Count >= 1)
                    {
                        CheckCapture(enemyLineup.IndexOf(GetEnemyTargeted(290, 50)));
                    }
                    else if (capture3Clicked && enemyLineup.Count >= 1)
                    {
                        CheckCapture(enemyLineup.IndexOf(GetEnemyTargeted(480, 50)));
                    }
                }
                else if (wasBossBattle)
                {
                    if (capture1Clicked) capture1Clicked = false;
                    if (capture2Clicked) capture2Clicked = false;
                    if (capture3Clicked) capture3Clicked = false;
                }
                if (move1Clicked && allowCapture == true)
                {
                    if (attackOrder[0].Move1 != null)
                        pm = attackOrder[0].Move1;
                    else move1Clicked = false;
                    //if (pm.NumTargets == 3) move1Clicked = false;
                }
                if (move2Clicked && allowCapture == true)
                {
                    if (attackOrder[0].Move2 != null)
                        pm = attackOrder[0].Move2;
                    else move2Clicked = false;
                    //if (pm.NumTargets == 3) move2Clicked = false;
                }
                if (move3Clicked && allowCapture == true)
                {
                    if (attackOrder[0].Move3 != null)
                        pm = attackOrder[0].Move3;
                    else move3Clicked = false;
                    //if (pm.NumTargets == 3) move3Clicked = false;
                }
                if (move4Clicked && allowCapture == true)
                {
                    if (attackOrder[0].Move4 != null)
                        pm = attackOrder[0].Move4;
                    else move4Clicked = false;
                    //if (pm.NumTargets == 3) move4Clicked = false;
                }

                // if the player clicked any move
                if (pm != null && pm.NumTargets == 1 && pm.MoveType == MoveTypes.offensive && pm.AffectedStat == Stats.none)
                {
                    // prompt them to pick a target
                    spriteBatch.DrawString(nameDisplay, "Choose a target.", new Vector2(40, 275), Color.Blue);

                    // check which enemy is targeted and set it to pp
                    if (enemy1Targeted && GetEnemyTargeted(100, 50) != null) pp = GetEnemyTargeted(100, 50);
                    else if (enemy2Targeted && GetEnemyTargeted(290, 50) != null) pp = GetEnemyTargeted(290, 50);
                    else if (enemy3Targeted && GetEnemyTargeted(480, 50) != null) pp = GetEnemyTargeted(480, 50);

                    // once the player chooses a target
                    if (enemy1Targeted || enemy2Targeted || enemy3Targeted)
                    {
                        if (damageDisplay == 0) damageDisplay = DoDamage(pm.Damage, attackOrder[0].Element, pp, false);
                        // draw the graphic for the move
                        spriteBatch.Draw(pm.Texture, pp.Position, Color.White);
                        spriteBatch.DrawString(goldDisplay, "-" + damageDisplay.ToString(), new Vector2(pp.Position.X - 75, pp.Position.Y + 100), Color.Red);
                    }

                    // after a second, to let the graphic stay on the screen for long enough to see
                    if (elapsedTime >= 4000)
                    {
                        // make sure this is only called if an enemy is targeted
                        if (enemy1Targeted || enemy2Targeted || enemy3Targeted)
                        {
                            //DoDamage(pm.Damage, attackOrder[0].Element, pp, true);
                            pp.CurrentHP -= damageDisplay;
                            damageDisplay = 0;
                            // reset all of these
                            enemy1Targeted = false; enemy2Targeted = false; enemy3Targeted = false;
                            move1Clicked = false; move2Clicked = false; move3Clicked = false; move4Clicked = false;
                            // remove attacker from front and add it to the back
                            attackOrder.Add(attackOrder[0]);
                            attackOrder.RemoveAt(0);
                        }
                        // reset the elapsedTime
                        elapsedTime = 0;
                    }
                }
                else if (pm != null && pm.NumTargets == 1 && pm.MoveType == MoveTypes.offensive && pm.AffectedStat != Stats.none)
                {
                    // prompt them to pick a target
                    spriteBatch.DrawString(nameDisplay, "Choose a target.", new Vector2(40, 275), Color.Blue);

                    // check which enemy is targeted and set it to pp
                    if (enemy1Targeted && GetEnemyTargeted(100, 50) != null) pp = GetEnemyTargeted(100, 50);
                    else if (enemy2Targeted && GetEnemyTargeted(290, 50) != null) pp = GetEnemyTargeted(290, 50);
                    else if (enemy3Targeted && GetEnemyTargeted(480, 50) != null) pp = GetEnemyTargeted(480, 50);

                    // once the player chooses a target
                    if (enemy1Targeted || enemy2Targeted || enemy3Targeted)
                    {
                        // draw the graphic for the move
                        spriteBatch.Draw(pm.Texture, pp.Position, Color.White);
                        spriteBatch.DrawString(goldDisplay, "-" + pm.Damage.ToString(), new Vector2(pp.Position.X - 75, pp.Position.Y + 100), Color.Red);
                        spriteBatch.DrawString(nameDisplay, pm.AffectedStat.ToString(), new Vector2(pp.Position.X - 75, pp.Position.Y + 175), Color.Red);
                    }

                    // after a second, to let the graphic stay on the screen for long enough to see
                    if (elapsedTime >= 4000)
                    {
                        // make sure this is only called if an enemy is targeted
                        if (enemy1Targeted || enemy2Targeted || enemy3Targeted)
                        {
                            if (pm.AffectedStat == Stats.attack)
                            {
                                pp.CurrentAttack -= pm.Damage;
                                pp.CurrentAttack = MathHelper.Clamp(pp.CurrentAttack, 1, 99999999);
                            }
                            else if (pm.AffectedStat == Stats.defense)
                            {
                                pp.CurrentDefense -= pm.Damage;
                                pp.CurrentDefense = MathHelper.Clamp(pp.CurrentDefense, 1, 999999999);
                            }
                            else if (pm.AffectedStat == Stats.speed)
                            {
                                pp.CurrentSpeed -= pm.Damage;
                                pp.CurrentSpeed = MathHelper.Clamp(pp.CurrentSpeed, 1, 999999999);
                            }

                            // reset all of these
                            enemy1Targeted = false; enemy2Targeted = false; enemy3Targeted = false;
                            move1Clicked = false; move2Clicked = false; move3Clicked = false; move4Clicked = false;
                            // remove attacker from front and add it to the back
                            attackOrder.Add(attackOrder[0]);
                            attackOrder.RemoveAt(0);
                        }
                        // reset the elapsedTime
                        elapsedTime = 0;
                    }
                }
                // if the move targets all the enemies
                else if (pm != null && pm.NumTargets == 3 && pm.MoveType == MoveTypes.offensive && pm.AffectedStat == Stats.none)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (i < enemyLineup.Count)
                        {
                            IPixel pixel = (IPixel)enemyLineup[i];

                            if (i == 0)
                            {
                                if (dmg1 == 0) dmg1 = DoDamage(pm.Damage, attackOrder[0].Element, pixel, false);
                                spriteBatch.DrawString(goldDisplay, "-" + dmg1.ToString(), new Vector2(pixel.Position.X - 75, pixel.Position.Y + 100), Color.Red);
                            }
                            else if (i == 1)
                            {
                                if (dmg2 == 0) dmg2 = DoDamage(pm.Damage, attackOrder[0].Element, pixel, false);
                                spriteBatch.DrawString(goldDisplay, "-" + dmg2.ToString(), new Vector2(pixel.Position.X - 75, pixel.Position.Y + 100), Color.Red);
                            }
                            else if (i == 2)
                            {
                                if (dmg3 == 0) dmg3 = DoDamage(pm.Damage, attackOrder[0].Element, pixel, false);
                                spriteBatch.DrawString(goldDisplay, "-" + dmg3.ToString(), new Vector2(pixel.Position.X - 75, pixel.Position.Y + 100), Color.Red);
                            }

                            spriteBatch.Draw(pm.Texture, pixel.Position, Color.White);
                        }
                    }

                    if (elapsedTime >= 4000)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (i < enemyLineup.Count)
                            {
                                IPixel pixel = (IPixel)enemyLineup[i];

                                if (i == 0)
                                {
                                    pixel.CurrentHP -= dmg1;
                                    dmg1 = 0;
                                }
                                else if (i == 1)
                                {
                                    pixel.CurrentHP -= dmg2;
                                    dmg2 = 0;
                                }
                                else if (i == 2)
                                {
                                    pixel.CurrentHP -= dmg3;
                                    dmg3 = 0;
                                }
                            }
                        }

                        // reset all of these
                        enemy1Targeted = false; enemy2Targeted = false; enemy3Targeted = false;
                        move1Clicked = false; move2Clicked = false; move3Clicked = false; move4Clicked = false;
                        pm = null; pp = null;
                        // remove attacker from front and add it to the back
                        attackOrder.Add(attackOrder[0]);
                        attackOrder.RemoveAt(0);

                        elapsedTime = 0;
                    }
                }
                else if (pm != null && pm.NumTargets == 3 && pm.MoveType == MoveTypes.offensive && pm.AffectedStat != Stats.none)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (i < enemyLineup.Count)
                        {
                            IPixel pixel = (IPixel)enemyLineup[i];

                            spriteBatch.DrawString(goldDisplay, "-" + pm.Damage.ToString(), new Vector2(pixel.Position.X - 75, pixel.Position.Y + 100), Color.Red);
                            spriteBatch.DrawString(nameDisplay, pm.AffectedStat.ToString(), new Vector2(pixel.Position.X - 75, pixel.Position.Y + 175), Color.Red);

                            spriteBatch.Draw(pm.Texture, pixel.Position, Color.White);
                        }
                    }

                    if (elapsedTime >= 4000)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (i < enemyLineup.Count)
                            {
                                IPixel pixel = (IPixel)enemyLineup[i];

                                if (pm.AffectedStat == Stats.attack)
                                {
                                    pixel.CurrentAttack -= pm.Damage;
                                    pixel.CurrentAttack = MathHelper.Clamp(pixel.CurrentAttack, 1, 99999999);
                                }
                                else if (pm.AffectedStat == Stats.defense)
                                {
                                    pixel.CurrentDefense -= pm.Damage;
                                    pixel.CurrentDefense = MathHelper.Clamp(pixel.CurrentDefense, 1, 999999999);
                                }
                                else if (pm.AffectedStat == Stats.speed)
                                {
                                    pixel.CurrentSpeed -= pm.Damage;
                                    pixel.CurrentSpeed = MathHelper.Clamp(pixel.CurrentSpeed, 1, 999999999);
                                }

                                //if (pm.AffectedStat == Stats.attack) pixel.CurrentAttack -= pm.Damage;
                                //else if (pm.AffectedStat == Stats.defense) pixel.CurrentDefense -= pm.Damage;
                                //else if (pm.AffectedStat == Stats.speed) pixel.CurrentSpeed -= pm.Damage;
                            }
                        }

                        // reset all of these
                        enemy1Targeted = false; enemy2Targeted = false; enemy3Targeted = false;
                        move1Clicked = false; move2Clicked = false; move3Clicked = false; move4Clicked = false;
                        pm = null; pp = null;
                        // remove attacker from front and add it to the back
                        attackOrder.Add(attackOrder[0]);
                        attackOrder.RemoveAt(0);

                        elapsedTime = 0;
                    }
                }
                else if (pm != null && pm.MoveType == MoveTypes.defensive)
                {
                    if (pm.NumTargets == 1)
                    {
                        spriteBatch.DrawString(goldDisplay, "+" + pm.Damage,
                            new Vector2(attackOrder[0].Position.X - 75, attackOrder[0].Position.Y - 100), Color.LightGreen);
                        spriteBatch.DrawString(nameDisplay, pm.AffectedStat.ToString(), new Vector2(attackOrder[0].Position.X - 75, attackOrder[0].Position.Y - 110), Color.LightGreen);

                        spriteBatch.Draw(lineup_tray, new Vector2(50, 300), Color.White);

                        for (int i = 0; i < player.Lineup.Count; i++)
                        {
                            IPixel pixel = player.Lineup[i];

                            if (!(pixel.CurrentHP <= 0))
                            {
                                // draw the pixel along with their name below and their HP above
                                pixel.Draw(spriteBatch);
                                // pixel's name
                                spriteBatch.DrawString(nameDisplay, (pixel.Name + " " + pixel.ID.ToString()),
                                    new Vector2(pixel.Position.X, pixel.Position.Y + 92), Color.White);
                                // pixel's HP
                                HpBar(pixel.CurrentHP, pixel.MaxHP, new Vector2(pixel.Position.X, pixel.Position.Y - 18));
                                spriteBatch.DrawString(nameDisplay, (pixel.CurrentHP + "/" + pixel.MaxHP),
                                    new Vector2(pixel.Position.X + 2, pixel.Position.Y - 18), Color.White);
                            }
                        }

                        // draw the move's graphic
                        spriteBatch.Draw(pm.Texture, attackOrder[0].Position, Color.White);

                        if (elapsedTime >= 4000)
                        {
                            if (pm.AffectedStat == Stats.attack) attackOrder[0].CurrentAttack += pm.Damage;
                            else if (pm.AffectedStat == Stats.defense) attackOrder[0].CurrentDefense += pm.Damage;
                            else if (pm.AffectedStat == Stats.speed) attackOrder[0].CurrentSpeed += pm.Damage;
                            else if (pm.AffectedStat == Stats.health) attackOrder[0].CurrentHP += pm.Damage;

                            // reset all of these
                            enemy1Targeted = false; enemy2Targeted = false; enemy3Targeted = false;
                            move1Clicked = false; move2Clicked = false; move3Clicked = false; move4Clicked = false;
                            pm = null; pp = null;

                            elapsedTime = 0;

                            attackOrder.Add(attackOrder[0]);
                            attackOrder.RemoveAt(0);
                        }
                    }
                    else if (pm.NumTargets == 3)
                    {
                        int increase = pm.Damage;

                        spriteBatch.Draw(lineup_tray, new Vector2(50, 300), Color.White);

                        for (int i = 0; i < player.Lineup.Count; i++)
                        {
                            IPixel pixel = player.Lineup[i];

                            if (!(pixel.CurrentHP <= 0))
                            {
                                // draw the pixel along with their name below and their HP above
                                pixel.Draw(spriteBatch);
                                // pixel's name
                                spriteBatch.DrawString(nameDisplay, (pixel.Name + " " + pixel.ID.ToString()),
                                    new Vector2(pixel.Position.X, pixel.Position.Y + 92), Color.White);
                                // pixel's HP
                                HpBar(pixel.CurrentHP, pixel.MaxHP, new Vector2(pixel.Position.X, pixel.Position.Y - 18));
                                spriteBatch.DrawString(nameDisplay, (pixel.CurrentHP + "/" + pixel.MaxHP),
                                    new Vector2(pixel.Position.X + 2, pixel.Position.Y - 18), Color.White);
                                spriteBatch.Draw(pm.Texture, pixel.Position, Color.White);
                            }
                        }

                        spriteBatch.DrawString(goldDisplay, "+" + pm.Damage,
                           new Vector2(attackOrder[0].Position.X - 75, attackOrder[0].Position.Y - 100), Color.LightGreen);
                        spriteBatch.DrawString(nameDisplay, pm.AffectedStat.ToString(), new Vector2(attackOrder[0].Position.X - 75, attackOrder[0].Position.Y - 110), Color.LightGreen);

                        if (elapsedTime >= 4000)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (i < player.Lineup.Count)
                                {
                                    if (pm.AffectedStat == Stats.attack) player.Lineup[i].CurrentAttack += increase;
                                    else if (pm.AffectedStat == Stats.defense) player.Lineup[i].CurrentDefense += increase;
                                    else if (pm.AffectedStat == Stats.health) player.Lineup[i].CurrentHP += increase;
                                    else if (pm.AffectedStat == Stats.speed)
                                    {
                                        player.Lineup[i].CurrentSpeed += increase;
                                        //GetAttackOrder();
                                    }
                                    else if (pm.AffectedStat == Stats.health) player.Lineup[i].CurrentHP += pm.Damage;
                                }
                            }

                            // reset all of these
                            enemy1Targeted = false; enemy2Targeted = false; enemy3Targeted = false;
                            move1Clicked = false; move2Clicked = false; move3Clicked = false; move4Clicked = false;
                            pm = null; pp = null;
                            // remove attacker from front and add it to the back
                            attackOrder.Add(attackOrder[0]);
                            attackOrder.RemoveAt(0);

                            elapsedTime = 0;
                        }
                    }
                }
            }
            // this is called if the first pixel is not in the player's lineup
            else if (attackOrder.Count > 0 && !playerDead && !enemyDead && !(capture1Clicked || capture2Clicked || capture3Clicked) && attackOrder[0].CurrentHP > 0)
            {
                // chooseMove is used to make sure the enemy AI only chooses a move once
                if (chooseMove)
                {
                    // make sure the AI cannot choose a move that is null
                    int movesAvailable = 0;
                    if (attackOrder[0].Move1 != null) movesAvailable++;
                    if (attackOrder[0].Move2 != null) movesAvailable++;
                    if (attackOrder[0].Move3 != null) movesAvailable++;
                    if (attackOrder[0].Move4 != null) movesAvailable++;

                    // choose a random num between 1 and however many valid moves there are 
                    int randMove = random.Next(1, movesAvailable + 1);

                    // make sure the AI cannot choose a target out of the bounds of the player's lineup
                    int targetsAvailable = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        if (j < player.Lineup.Count) targetsAvailable++;
                    }

                    // choose a random number between one and the max number of available targets
                    randTarget = random.Next(1, targetsAvailable);

                    // this is the attacker
                    p = attackOrder[0];

                    if (randMove == 1) m = p.Move1;
                    else if (randMove == 2) m = p.Move2;
                    else if (randMove == 3) m = p.Move3;
                    else if (randMove == 4) m = p.Move4;

                    // set this to false so the AI won't choose another move again
                    chooseMove = false;

                    if (m.NumTargets == 1 && player.Lineup.Count > randTarget - 1 && m.MoveType == MoveTypes.offensive && enemyDamage == 0)
                    {
                        enemyDamage = DoDamage(m.Damage, attackOrder[0].Element, player.Lineup[randTarget - 1], false);
                    }
                    else if (m.NumTargets == 3 && m.MoveType == MoveTypes.offensive && dmg1 == 0 && dmg2 == 0 && dmg3 == 0)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (i < player.Lineup.Count)
                            {
                                IPixel pixel = player.Lineup[i];

                                if (i == 0)
                                    dmg1 = DoDamage(m.Damage, attackOrder[0].Element, pixel, false);
                                else if (i == 1)
                                    dmg2 = DoDamage(m.Damage, attackOrder[0].Element, pixel, false);
                                else if (i == 2)
                                    dmg3 = DoDamage(m.Damage, attackOrder[0].Element, pixel, false);
                            }
                        }
                    }
                }
                //string display = attackOrder[0].Name + " " + attackOrder[0].ID.ToString() + " used " + m.ToString();
                //spriteBatch.DrawString(nameDisplay, display,
                    //new Vector2(400 - ((display.Length * 8) / 2), attackOrder[0].Position.Y + 150), Color.Red);

                if (m.NumTargets == 1 && m.MoveType == MoveTypes.offensive && m.AffectedStat == Stats.none)
                {
                    // write it to the screen
                    if (player.Lineup.Count > randTarget - 1 && player.Lineup[randTarget - 1].CurrentHP > 0)
                    {
                        spriteBatch.DrawString(goldDisplay, "-" + enemyDamage,
                            new Vector2(player.Lineup[randTarget - 1].Position.X - 75, player.Lineup[randTarget - 1].Position.Y - 100), Color.Red);
                        spriteBatch.DrawString(nameDisplay, m.ToString(), new Vector2(player.Lineup[randTarget - 1].Position.X, player.Lineup[randTarget - 1].Position.Y - 110), Color.Red);
                    }

                    // draw the move's graphic
                    spriteBatch.Draw(m.Texture, player.Lineup[randTarget - 1].Position, Color.White);

                    if (elapsedTime >= 4000)
                    {
                        player.Lineup[randTarget - 1].CurrentHP -= enemyDamage;

                        enemyDamage = 0;
                        elapsedTime = 0;

                        chooseMove = true;
                        attackOrder.Add(attackOrder[0]);
                        attackOrder.RemoveAt(0);
                    }
                }
                else if (m.NumTargets == 1 && m.MoveType == MoveTypes.offensive && m.AffectedStat != Stats.none)
                {

                    // draw the graphic for the move
                    spriteBatch.Draw(player.Lineup[randTarget - 1].Texture, player.Lineup[randTarget - 1].Position, Color.White);
                    spriteBatch.DrawString(goldDisplay, "-" + m.Damage.ToString(), new Vector2(player.Lineup[randTarget - 1].Position.X - 75, player.Lineup[randTarget - 1].Position.Y - 100), Color.Red);
                    spriteBatch.DrawString(nameDisplay, m.AffectedStat.ToString(), new Vector2(player.Lineup[randTarget - 1].Position.X - 75, player.Lineup[randTarget - 1].Position.Y - 175), Color.Red);

                    // after a second, to let the graphic stay on the screen for long enough to see
                    if (elapsedTime >= 4000)
                    {
                        if (m.AffectedStat == Stats.attack)
                        {
                            player.Lineup[randTarget - 1].CurrentAttack -= m.Damage;
                            player.Lineup[randTarget - 1].CurrentAttack = MathHelper.Clamp(player.Lineup[randTarget - 1].CurrentAttack, 1, 99999999);
                        }
                        else if (m.AffectedStat == Stats.defense)
                        {
                            player.Lineup[randTarget - 1].CurrentDefense -= m.Damage;
                            player.Lineup[randTarget - 1].CurrentDefense = MathHelper.Clamp(player.Lineup[randTarget - 1].CurrentDefense, 1, 999999999);
                        }
                        else if (m.AffectedStat == Stats.speed)
                        {
                            player.Lineup[randTarget - 1].CurrentSpeed -= m.Damage;
                            player.Lineup[randTarget - 1].CurrentSpeed = MathHelper.Clamp(player.Lineup[randTarget - 1].CurrentSpeed, 1, 999999999);
                        }
                        //if (m.AffectedStat == Stats.attack) player.Lineup[randTarget - 1].CurrentAttack -= m.Damage;
                        //else if (m.AffectedStat == Stats.defense) player.Lineup[randTarget - 1].CurrentDefense -= m.Damage;
                        //else if (m.AffectedStat == Stats.speed) player.Lineup[randTarget - 1].CurrentSpeed -= m.Damage;

                        chooseMove = true;
                        enemyDamage = 0;
                        // remove attacker from front and add it to the back
                        attackOrder.Add(attackOrder[0]);
                        attackOrder.RemoveAt(0);
                        // reset the elapsedTime
                        elapsedTime = 0;
                    }
                }
                else if (m.NumTargets == 1 && m.MoveType == MoveTypes.defensive)
                {
                    spriteBatch.DrawString(goldDisplay, "+" + m.Damage,
                        new Vector2(attackOrder[0].Position.X - 75, attackOrder[0].Position.Y + 100), Color.LightGreen);
                    spriteBatch.DrawString(nameDisplay, m.AffectedStat.ToString(), new Vector2(attackOrder[0].Position.X - 75, attackOrder[0].Position.Y + 175), Color.LightGreen);

                    // draw the move's graphic
                    spriteBatch.Draw(m.Texture, attackOrder[0].Position, Color.White);

                    if (elapsedTime >= 4000)
                    {
                        if (m.AffectedStat == Stats.attack) attackOrder[0].CurrentAttack += m.Damage;
                        else if (m.AffectedStat == Stats.defense) attackOrder[0].CurrentDefense += m.Damage;
                        else if (m.AffectedStat == Stats.speed) attackOrder[0].CurrentSpeed += m.Damage;
                        else if (m.AffectedStat == Stats.health) attackOrder[0].CurrentHP += m.Damage;

                        enemyDamage = 0;
                        elapsedTime = 0;

                        chooseMove = true;
                        attackOrder.Add(attackOrder[0]);
                        attackOrder.RemoveAt(0);
                    }
                }
                else if (m.NumTargets == 3 && m.MoveType == MoveTypes.offensive && m.AffectedStat == Stats.none)
                {

                    for (int j = 0; j < 3; j++)
                    {
                        if (j < player.Lineup.Count)
                        {
                            spriteBatch.DrawString(nameDisplay, m.ToString(), new Vector2(player.Lineup[j].Position.X, player.Lineup[j].Position.Y - 110), Color.Red);
                            if (j == 0)
                                spriteBatch.DrawString(goldDisplay, "-" + dmg1.ToString(), new Vector2(player.Lineup[j].Position.X - 75, player.Lineup[j].Position.Y - 100), Color.Red);
                            if (j == 1)
                                spriteBatch.DrawString(goldDisplay, "-" + dmg2.ToString(), new Vector2(player.Lineup[j].Position.X - 75, player.Lineup[j].Position.Y - 100), Color.Red);
                            if (j == 2)
                                spriteBatch.DrawString(goldDisplay, "-" + dmg3.ToString(), new Vector2(player.Lineup[j].Position.X - 75, player.Lineup[j].Position.Y - 100), Color.Red);
                        }
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        if (i < player.Lineup.Count)
                        {
                            spriteBatch.Draw(m.Texture, player.Lineup[i].Position, Color.White);
                        }
                    }

                    if (elapsedTime >= 4000)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (i < player.Lineup.Count)
                            {
                                if (i == 0)
                                {
                                    player.Lineup[i].CurrentHP -= dmg1;
                                    dmg1 = 0;
                                }
                                else if (i == 1)
                                {
                                    player.Lineup[i].CurrentHP -= dmg2;
                                    dmg2 = 0;
                                }
                                else if (i == 2)
                                {
                                    player.Lineup[i].CurrentHP -= dmg3;
                                    dmg3 = 0;
                                }

                            }
                        }

                        elapsedTime = 0;

                        enemyDamage = 0;
                        chooseMove = true;
                        attackOrder.Add(attackOrder[0]);
                        attackOrder.RemoveAt(0);
                    }
                }
                else if (m.NumTargets == 3 && m.MoveType == MoveTypes.offensive && m.AffectedStat != Stats.none)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (i < player.Lineup.Count)
                        {
                            IPixel pixel = player.Lineup[i];

                            spriteBatch.DrawString(goldDisplay, "-" + m.Damage.ToString(), new Vector2(pixel.Position.X - 75, pixel.Position.Y - 100), Color.Red);
                            spriteBatch.DrawString(nameDisplay, m.AffectedStat.ToString(), new Vector2(pixel.Position.X - 75, pixel.Position.Y - 175), Color.Red);

                            spriteBatch.Draw(m.Texture, pixel.Position, Color.White);
                        }
                    }

                    if (elapsedTime >= 4000)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (i < player.Lineup.Count)
                            {
                                IPixel pixel = player.Lineup[i];

                                if (m.AffectedStat == Stats.attack)
                                {
                                    pixel.CurrentAttack -= m.Damage;
                                    pixel.CurrentAttack = MathHelper.Clamp(pixel.CurrentAttack, 1, 99999999);
                                }
                                else if (m.AffectedStat == Stats.defense)
                                {
                                    pixel.CurrentDefense -= m.Damage;
                                    pixel.CurrentDefense = MathHelper.Clamp(pixel.CurrentDefense, 1, 999999999);
                                }
                                else if (m.AffectedStat == Stats.speed)
                                {
                                    pixel.CurrentSpeed -= m.Damage;
                                    pixel.CurrentSpeed = MathHelper.Clamp(pixel.CurrentSpeed, 1, 999999999);
                                }

                                //if (m.AffectedStat == Stats.attack) pixel.CurrentAttack -= m.Damage;
                                //else if (m.AffectedStat == Stats.defense) pixel.CurrentDefense -= m.Damage;
                                //else if (m.AffectedStat == Stats.speed) pixel.CurrentSpeed -= m.Damage;
                            }
                        }

                        enemyDamage = 0;
                        chooseMove = true;
                        // remove attacker from front and add it to the back
                        attackOrder.Add(attackOrder[0]);
                        attackOrder.RemoveAt(0);

                        elapsedTime = 0;
                    }
                }
                else if (m.NumTargets == 3 && m.MoveType == MoveTypes.defensive)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (i < enemyLineup.Count)
                        {
                            IPixel pixel = (IPixel)enemyLineup[i];
                            spriteBatch.Draw(m.Texture, pixel.Position, Color.White);

                            spriteBatch.DrawString(goldDisplay, "+" + m.Damage.ToString(), new Vector2(pixel.Position.X - 75, pixel.Position.Y + 100), Color.LightGreen);
                            spriteBatch.DrawString(nameDisplay, m.AffectedStat.ToString(), new Vector2(pixel.Position.X - 75, pixel.Position.Y + 175), Color.LightGreen);
                        }
                    }

                    if (elapsedTime >= 4000)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (i < enemyLineup.Count)
                            {
                                IPixel p = (IPixel)enemyLineup[i];
                                int increase = m.Damage;

                                if (m.AffectedStat == Stats.attack) p.CurrentAttack += increase;
                                else if (m.AffectedStat == Stats.defense) p.CurrentDefense += increase;
                                else if (m.AffectedStat == Stats.health) p.CurrentHP += increase;
                                else if (m.AffectedStat == Stats.speed)
                                {
                                    p.CurrentSpeed += increase;
                                    //GetAttackOrder();
                                }
                                else if (m.AffectedStat == Stats.health) attackOrder[0].CurrentHP += m.Damage;
                            }
                        }
                        elapsedTime = 0;

                        enemyDamage = 0;
                        chooseMove = true;
                        attackOrder.Add(attackOrder[0]);
                        attackOrder.RemoveAt(0);
                    }
                }
            }
        }

        private void DisplayMove(Rectangle rect, string moveName)
        {
            spriteBatch.DrawString(nameDisplay, moveName,
                new Vector2((rect.X + (rect.Width / 2)) - (moveName.Length * 8) / 2, (rect.Y + (rect.Height / 2)) - 6), Color.White);
        }

        private void DisplayInfo(int x, int y, IMove move)
        {
            // x = 374 y = 321
            spriteBatch.Draw(hoverInfo, new Vector2(x, y), Color.White);
            spriteBatch.Draw(hoverDamage, new Vector2(x - 65, y - 89), Color.White);
            spriteBatch.DrawString(nameDisplay, "Base Damage", new Vector2(x - 44, y - 88), Color.White);
            spriteBatch.DrawString(nameDisplay, move.InfoDamage(), new Vector2((x + 7) - ((move.InfoDamage().Length * 8) / 2), y - 73), Color.Red);
            spriteBatch.DrawString(nameDisplay, "Num Targets", new Vector2(x - 44, y - 58), Color.White);
            spriteBatch.DrawString(nameDisplay, move.NumTargets.ToString(),
                new Vector2((x + 7) - ((move.NumTargets.ToString().Length * 8) / 2), y - 42), Color.LightGreen);
        }

        private void GetAttackOrder()
        {
            // clear attackOrder so we don't sort pixels from old fights
            attackOrder.Clear();

            // Add each pixel in the player and enemy lineups
            for (int i = 0; i < 3; i++)
            {
                if (i < player.Lineup.Count) attackOrder.Add(player.Lineup[i]);
                if (i < enemyLineup.Count) attackOrder.Add((IPixel)enemyLineup[i]);
            }

            // go through attackOrder and multiply all speeds by a random number
            foreach (IPixel pixel in attackOrder)
            {
                pixel.CurrentSpeed = pixel.CurrentSpeed * (random.Next(1, 11));
            }

            // sort the attack order by the current speed
            attackOrder = attackOrder.OrderBy(o => o.CurrentSpeed).ToList();
            // reverse it so that the biggest current speed is first 
            attackOrder.Reverse();
        }

        private void DisplayText(string str_in, int index, int x, int y, int xdif, int ydif, bool inPlayer)
        {
            Color color = Color.Red;

            if (inPlayer) color = Color.LightGreen;

            spriteBatch.DrawString(nameDisplay, str_in, new Vector2(x + (xdif * index), y + (ydif * index)), color);
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

        /// <summary>
        /// Resets all necessary components to be ready for the next battle
        /// </summary>
        private void TeamDead(object sender, EventArgs eventArgs)
        {
            for (int i = 0; i < enemyLineup.Count; i++) IdNum--;
            foreach (IPixel pixel in player.Lineup)
            {
                pixel.CurrentAttack = pixel.Attack;
                if (player.SelectedSword != null) pixel.CurrentAttack += player.SelectedSword.Boost;
                pixel.CurrentDefense = pixel.Defense;
                if (player.SelectedShield != null) pixel.CurrentDefense += player.SelectedShield.Boost;
                pixel.CurrentSpeed = pixel.Speed;
                if (player.SelectedShoes != null) pixel.CurrentSpeed += player.SelectedShoes.Boost;
            }
            enemyLineup.Clear();
            numEnemyDead.Clear();
            numPlayerDead.Clear();
            attackOrder.Clear();
            inBattle = false;
            showBattle = true;
            battleEnd = true;
            playerDead = false;
            enemyDead = false;

            elapsedTime = 0;
            CheckLevelUp(sender, eventArgs);
        }

        private void CheckLevelUp(object sender, EventArgs e)
        {
            foreach (IPixel pixel in player.Lineup)
            {
                pixel.CurrentXP += totalXP;
                if (pixel.CurrentXP >= pixel.LevelUpValue)
                {
                    pixel.LevelUp();
                    if (player.SelectedArmor != null)
                    {
                        pixel.CurrentHP += player.SelectedArmor.Boost;
                        pixel.CurrentHP = MathHelper.Clamp(pixel.CurrentHP, pixel.CurrentHP, pixel.CurrentHP + player.SelectedArmor.Boost);
                    }
                    if (player.SelectedSword != null)  pixel.CurrentAttack += player.SelectedSword.Boost;
                    if (player.SelectedShield != null) pixel.CurrentDefense += player.SelectedShield.Boost;
                    if (player.SelectedShoes != null) pixel.CurrentSpeed += player.SelectedShoes.Boost;
                }
            }
        }

        private void CheckCapture(int pos)
        {
            IPixel target = (IPixel)enemyLineup[pos];

            int chance = target.CaptureChance;
            if (target.CurrentHP > 20)
                chance++;
            if (target.CurrentHP > 50)
                chance++;
            if (target.CurrentHP > 100)
                chance++;

            if (elapsedTime > 4000)
            {
                if (!captureDisplayStr.Contains("Failed") && !string.IsNullOrEmpty(captureDisplayStr))
                {
                    target.CurrentHP = 0;
                }

                captureDisplayStr = "";

                allowCapture = true;

                attackOrder.Add(attackOrder[0]);
                attackOrder.RemoveAt(0);

                elapsedTime = 0;

                capture1Clicked = false;
                capture2Clicked = false;
                capture3Clicked = false;
                enemy1Targeted = false; enemy2Targeted = false; enemy3Targeted = false;
                move1Clicked = false; move2Clicked = false; move3Clicked = false; move4Clicked = false;

                return;
            }
            else if (random.Next(0, chance) == 1 && allowCapture == true)
            {
                IPixel thing = (IPixel)Activator.CreateInstance(target.GetType());
                thing.Init(Content.Load<Texture2D>(thing.TextureStr), new Vector2(100, 100), IdNum, player, false);
                IdNum++;
                MovesInit(ref thing);

                captureDisplayStr = "Captured " + thing.Name + thing.ID.ToString();

                if (player.Quests.Count > 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (i < player.Quests.Count && player.Quests[i].CaptureTarget != null)
                        {
                            if (thing.Name == player.Quests[i].CaptureTarget && player.Quests[i].Progress + 1 <= player.Quests[i].Goal)
                            {
                                player.Quests[i].Progress++;
                            }
                        }
                    }
                }

                if (player.Lineup.Count < player.MaxPixels)
                {
                    player.Lineup.Add(thing);
                    regenList.Add(thing);

                    // reset locations of all pixels
                    for (int i = 0; i < player.Lineup.Count; i++)
                    {
                        if (i < 3)
                        {
                            player.Lineup[i].Position = new Vector2((100 + 190 * i), 332);
                        }
                        else if (i == 3)
                        {
                            player.Lineup[i].Position = new Vector2(1080, 332);
                        }
                        else
                        {
                            player.Lineup[i].Position = new Vector2((880 + 190 * i - 3), 332);
                        }
                    }
                }
                else
                {
                    player.Backup.Add(thing);
                }

                //target.CurrentHP = 0;
                allowCapture = false;
            }
            else if (allowCapture == true)
            {
                IPixel thing = (IPixel)enemyLineup[pos];
                captureDisplayStr = "Failed to capture " + thing.Name + " " + thing.ID.ToString();
                allowCapture = false;
            }

            spriteBatch.DrawString(nameDisplay, captureDisplayStr, new Vector2(75, 225), Color.White);
        }

        /// <summary>
        /// Gets the enemy currently at the x and y coordinate supplied
        /// </summary>
        /// <param name="x">the x coordinate to look for the enemy</param>
        /// <param name="y">the y coordinate to look for the enemy</param>
        /// <returns></returns>
        private IPixel GetEnemyTargeted(int x, int y)
        {
            List<IPixel> temp = new List<IPixel>();
            //temp.Capacity = enemyLineup.Count;
            for (int i = 0; i < enemyLineup.Count; i++)
            {
                IPixel p = (IPixel)enemyLineup[i];

                temp.Add(p);
            }

            IEnumerable<IPixel> query = temp.Where(pixel => pixel.Position.X == x && pixel.Position.Y == y);

            return query.ElementAt(0);
        }

        private void HpBar(float currentHp, float maxHp, Vector2 position)
        {
            float percent = (currentHp / maxHp) * 100;

            Texture2D tex = hpBarGood;
            if (percent <= 50) tex = hpBarMedium;
            else if (percent <= 20) tex = hpBarBad;

            for (int i = 0; i <= percent; i++)
            {
                spriteBatch.Draw(tex, new Vector2(position.X + i, position.Y), Color.White);
            }
            for (int i = 0; i < (100 - percent); i++)
            {
                spriteBatch.Draw(hpBarEmpty, new Vector2((position.X + percent) + i, position.Y), Color.White);
            }
        }

        private int DoDamage(int baseDamage, ElementType atkElement, IPixel damagee, bool doDamage)
        {
            int finalDamage = baseDamage;

            if (baseDamage - (damagee.CurrentDefense / 4) > 1)
            {
                finalDamage = baseDamage - (damagee.CurrentDefense / 4);
            }

            if ((atkElement == ElementType.Fire && damagee.Element == ElementType.Earth)
                || (atkElement == ElementType.Earth && damagee.Element == ElementType.Air)
                || (atkElement == ElementType.Air && damagee.Element == ElementType.Water)
                || (atkElement == ElementType.Water && damagee.Element == ElementType.Fire))
            {
                finalDamage = (int)(finalDamage * 1.25);
            }

            if (doDamage)
            {
                damagee.CurrentHP -= finalDamage;
            }
            return finalDamage;
        }

        public void CheckClicks(MouseState currentMouseState)
        {
            if (player.showBattleTut)
            {
                return;
            }
            // if the left mouse button is clicked, check which move and target is selected
            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                if (capture1Rect.Contains(currentMouseState.Position) && !capture1Clicked && player.Lineup.Contains(attackOrder[0]) && !wasBossBattle) capture1Clicked = true;
                else if (capture2Rect.Contains(currentMouseState.Position) && !capture2Clicked && player.Lineup.Contains(attackOrder[0]) && !wasBossBattle) capture2Clicked = true;
                else if (capture3Rect.Contains(currentMouseState.Position) && !capture3Clicked && player.Lineup.Contains(attackOrder[0]) && !wasBossBattle) capture3Clicked = true;

                if (move1Rect.Contains(currentMouseState.Position) && !move1Clicked && !move2Clicked && !move3Clicked && !move4Clicked)
                {
                    move1Clicked = true;
                    elapsedTime = 0;
                }
                else if (move2Rect.Contains(currentMouseState.Position) && !move1Clicked && !move2Clicked && !move3Clicked && !move4Clicked)
                {
                    move2Clicked = true;
                    elapsedTime = 0;
                }
                else if (move3Rect.Contains(currentMouseState.Position) && !move1Clicked && !move2Clicked && !move3Clicked && !move4Clicked)
                {
                    move3Clicked = true;
                    elapsedTime = 0;
                }
                else if (move4Rect.Contains(currentMouseState.Position) && !move1Clicked && !move2Clicked && !move3Clicked && !move4Clicked)
                {
                    move4Clicked = true;
                    elapsedTime = 0;
                }

                if (move1Clicked || move2Clicked || move3Clicked || move4Clicked)
                {
                    if (enemy1Rect.Contains(currentMouseState.Position))
                    {
                        enemy1Targeted = true;
                        elapsedTime = 0;
                    }
                    else if (enemy2Rect.Contains(currentMouseState.Position))
                    {
                        enemy2Targeted = true;
                        elapsedTime = 0;
                    }
                    else if (enemy3Rect.Contains(currentMouseState.Position))
                    {
                        enemy3Targeted = true;
                        elapsedTime = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Sets inBattle to true depending on a random number generator; used for determing random encounters
        /// </summary>
        public void GetEncounter()
        {
            int encounter = random.Next(1, currentTile.SpawnRate);
            if (encounter == 7)
            {
                inBattle = true;
            }
        }

        public bool BattleResults(MouseState currentMouseState)
        {
            bool inTownResult = false;

            if (playerWon)
            {
                if (wasBossBattle)
                {
                    spriteBatch.Draw(end, Vector2.Zero, Color.White);

                    if (currentMouseState.LeftButton == ButtonState.Released)
                    {
                        if (new Rectangle(0, 420, 60, 60).Contains(currentMouseState.Position))
                            spriteBatch.Draw(blackX, new Vector2(0, 420), Color.White);
                    }

                    if (currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (new Rectangle(0, 420, 60, 60).Contains(currentMouseState.Position))
                        {
                            player.position.X = 91; player.position.Y = 250;

                            totalXP = 0;
                            showLoot = false;
                            playerWon = false;
                            battleEnd = false;
                            showBattle = true;
                            wasBossBattle = false;
                            inBattle = false;
                            //inBattle = false;
                            results = null;
                        }
                    }
                }
                else if (evolveList.Count > 0)
                {
                    spriteBatch.Draw(evolveScreen, Vector2.Zero, Color.White);

                    DrawEvolvePixel(evolveList[evolveIndex], 145, 185);

                    IPixel next = evolveList[evolveIndex].NextForm;
                    next.Init(Content.Load<Texture2D>(next.TextureStr), Vector2.Zero, evolveList[evolveIndex].ID, player, false);
                    DrawEvolvePixel(next, 565, 185);


                    spriteBatch.DrawString(nameDisplay, (evolveList[evolveIndex].Name + " " + evolveList[evolveIndex].ID.ToString())
                        + " has evolved into a " + next.Name, new Vector2(156, 328), Color.LightGreen);

                    if (currentMouseState.LeftButton == ButtonState.Released)
                    {
                        if (!allowShift)
                            allowShift = true;

                        if (new Rectangle(0, 420, 60, 60).Contains(currentMouseState.Position))
                            spriteBatch.Draw(x, new Vector2(0, 420), Color.White);
                    }

                    if (currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (new Rectangle(0, 420, 60, 60).Contains(currentMouseState.Position))
                        {
                            allowShift = true;
                            // make sure to reset every pixel's leveledup to false so they don't keep going
                            for (int i = 0; i < evolveList.Count; i++)
                            {
                                IPixel pixel = evolveList[i];

                                IPixel nextform = (IPixel)Activator.CreateInstance(pixel.NextForm.GetType()); //pixel.NextForm;  
                                nextform.Init(Content.Load<Texture2D>(nextform.TextureStr), pixel.Position, IdNum, player, false);
                                IdNum++;

                                int index = player.Lineup.IndexOf(evolveList[i]);
                                player.Lineup.RemoveAt(index);
                                player.Lineup.Insert(index, nextform);

                                regenList.RemoveAt(index);
                                regenList.Insert(index, nextform);

                                pixel.Evolve = false;
                                pixel.LeveledUp = false;
                            }

                            for (int i = 0; i < player.Lineup.Count; i++)
                            {
                                IPixel pixel = player.Lineup[i];
                                MovesInit(ref pixel);
                            }
                                
                            evolveList.Clear();
                            showLoot = true;
                            startingFrame = 0;
                            elapsedTime = 0;
                        }
                        else if (allowShift)
                        {
                            //int firstPart = (player.Lineup.Count / 6);
                            int firstPart = evolveList.Count - 1;
                            int secondPart = evolveIndex + 1;
                            if (firstPart > secondPart)
                                evolveIndex++;
                            else
                                evolveIndex = 0;
                            allowShift = false;
                        }
                    }
                }
                else if (showLoot)
                {
                    spriteBatch.Draw(lootScreen, Vector2.Zero, Color.White);

                    if (results == null)
                        results = GetLoot.Get(totalXP, Content);

                    goldInt = results._gold;

                    spriteBatch.DrawString(goldDisplay, goldInt.ToString(), new Vector2(171, 164), Color.Yellow);

                    if (results._item != null)
                    {
                        spriteBatch.Draw(results._item.Texture, new Vector2(245, 300), Color.White);
                        spriteBatch.DrawString(nameDisplay, results._item.Name, new Vector2(245, 422), Color.Black);

                        if (results._item.AffectedStat == Stats.attack)
                        {
                            if (!player.Swords.Contains(results._item))
                            {
                                if (player.Swords.Count > 1)
                                    results._item.Id = player.Swords[player.Swords.Count - 1].Id + 1;
                                player.Swords.Add(results._item);
                            }
                        }
                        else if (results._item.AffectedStat == Stats.defense)
                        {
                            if (!player.Shields.Contains(results._item))
                            {
                                if (player.Shields.Count > 1)
                                    results._item.Id = player.Shields[player.Shields.Count - 1].Id + 1;
                                player.Shields.Add(results._item);
                            }
                        }
                        else if (results._item.AffectedStat == Stats.health)
                        {
                            if (!player.Armor.Contains(results._item))
                            {
                                if (player.Armor.Count > 1)
                                    results._item.Id = player.Armor[player.Armor.Count - 1].Id + 1;
                                player.Armor.Add(results._item);
                            }
                        }
                        else if (results._item.AffectedStat == Stats.speed)
                        {
                            if (!player.Shoes.Contains(results._item))
                            {
                                if (player.Shoes.Count > 1)
                                    results._item.Id = player.Shoes[player.Shoes.Count - 1].Id + 1;
                                player.Shoes.Add(results._item);
                            }
                        }
                    }
                    else if (results._item == null)
                    {
                        spriteBatch.DrawString(nameDisplay, "No items", new Vector2(248, 353), Color.Red);
                        spriteBatch.DrawString(nameDisplay, "were looted", new Vector2(248, 368), Color.Red);
                    }

                    if (currentMouseState.LeftButton == ButtonState.Released)
                    {
                        if (!allowShift)
                            allowShift = true;

                        if (new Rectangle(740, 0, 60, 60).Contains(currentMouseState.Position))
                            spriteBatch.Draw(x, new Vector2(740, 0), Color.White);
                    }

                    if (currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (new Rectangle(740, 0, 60, 60).Contains(currentMouseState.Position))
                        {
                            totalXP = 0;
                            showLoot = false;
                            playerWon = false;
                            battleEnd = false;
                            showBattle = true;
                            player.gold += results._gold;
                            results = null;
                            elapsedTime = 0;
                        }
                    }
                }
                else // if evolveList.Count == 0
                {
                    spriteBatch.Draw(results_screen, Vector2.Zero, Color.White);
                    spriteBatch.DrawString(nameDisplay, "page " + ((startingFrame / 6) + 1).ToString() + "/" + ((player.Lineup.Count + 6) / 6).ToString(),
                        new Vector2(50, 460), Color.White);
                    // draws the results of each pixel on the screen
                    for (int i = startingFrame; i < startingFrame + 6; i++)
                    {
                        if (i == 0 || i <= player.Lineup.Count - 1)
                        {
                            IPixel pixel = player.Lineup[i];

                            if (player.Lineup.IndexOf(pixel) == startingFrame)
                            {
                                DrawResultPixel(pixel, 45, 45);
                            }
                            else if (player.Lineup.IndexOf(pixel) == startingFrame + 1)
                            {
                                DrawResultPixel(pixel, 45, 185);
                            }
                            else if (player.Lineup.IndexOf(pixel) == startingFrame + 2)
                            {
                                DrawResultPixel(pixel, 45, 325);
                            }
                            else if (player.Lineup.IndexOf(pixel) == startingFrame + 3)
                            {
                                DrawResultPixel(pixel, 425, 45);
                            }
                            else if (player.Lineup.IndexOf(pixel) == startingFrame + 4)
                            {
                                DrawResultPixel(pixel, 425, 185);
                            }
                            else if (player.Lineup.IndexOf(pixel) == startingFrame + 5)
                            {
                                DrawResultPixel(pixel, 425, 325);
                            }
                        }
                    }

                    if (currentMouseState.LeftButton == ButtonState.Released)
                    {
                        if (!allowShift)
                            allowShift = true;

                        if (new Rectangle(740, 380, 60, 100).Contains(currentMouseState.Position))
                            spriteBatch.Draw(hoverContinue, new Vector2(740, 380), Color.White);
                    }

                    if (currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (new Rectangle(740, 380, 60, 100).Contains(currentMouseState.Position))
                        {
                            allowShift = true;
                            // make sure to reset every pixel's leveledup to false so they don't keep going
                            for (int i = 0; i < player.Lineup.Count; i++)
                            {
                                IPixel pixel = player.Lineup[i];

                                if (pixel.LeveledUp)
                                {
                                    pixel.LeveledUp = false;
                                }
                                //if (pixel.Name == "Earth Baby")
                                if (pixel.Evolve == true)
                                {
                                    evolveList.Add(pixel);
                                }
                            }

                            if (evolveList.Count == 0)
                            {
                                showLoot = true;
                                startingFrame = 0;
                                elapsedTime = 0;
                            }
                        }
                        else if (allowShift)
                        {
                            //int firstPart = (player.Lineup.Count / 6);
                            int firstPart = (player.Lineup.Count + 6) / 6;
                            int secondPart = (startingFrame + 6) / 6;
                            if (firstPart > secondPart)
                                startingFrame += 6;
                            else
                                startingFrame = 0;
                            allowShift = false;
                        }
                    }
                }
            }
            else if (!playerWon)
            {
                spriteBatch.Draw(currentTile.CurrentTexture, Vector2.Zero, Color.Black);
                spriteBatch.DrawString(nameDisplay, "All your pixels have fainted. You are being sent back to the last village.",
                    new Vector2(15, 200), Color.White);
                if (elapsedTime >= 6000)
                {
                    playerWon = false;
                    battleEnd = false;
                    inTownResult = true;
                    elapsedTime = 0;
                }
            }
            return inTownResult;
        }

        private void DrawResultPixel(IPixel pixel, int x, int y)
        {
            spriteBatch.DrawString(nameDisplay, "Level " + pixel.Level.ToString(),
                new Vector2(x, y - 20), Color.Black);
            spriteBatch.Draw(pixel.Texture, new Vector2(x, y), Color.White);
            spriteBatch.DrawString(nameDisplay, pixel.Name + " " + pixel.ID.ToString(),
                new Vector2(x - 10, y + 100), Color.Black);

            spriteBatch.DrawString(nameDisplay, "Gained " + totalXP + "XP",
                new Vector2(x + 120, y), Color.LightBlue);
            if (pixel.LeveledUp)
            {
                spriteBatch.DrawString(nameDisplay, "Is now level " + pixel.Level.ToString() + "!",
                    new Vector2(x + 120, y + 15), Color.LightGreen);
            }
        }

        private void DrawEvolvePixel(IPixel pixel, int x, int y)
        {
            spriteBatch.Draw(pixel.Texture, new Vector2(x, y), Color.White);
            spriteBatch.DrawString(nameDisplay, pixel.Name + " " + pixel.ID.ToString(),
                new Vector2(x - 10, y + 100), Color.Black);
        }
    }
}
