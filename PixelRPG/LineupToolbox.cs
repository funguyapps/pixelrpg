using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelRPG.Pixels;

namespace PixelRPG
{
    public class LineupToolbox
    {
        // these used for LineupScreen
        int startingFrame = 0;
        int bottomStarting = 0;
        bool allowMove = true;
        SpriteFont nameDisplay;
        public bool inLineup;
        bool allowShift = true;

        // split screen
        public Vector2 up1Vector = new Vector2(125, 65);
        public Vector2 up2Vector = new Vector2(265, 65);
        public Vector2 up3Vector = new Vector2(445, 65);
        public Vector2 up4Vector = new Vector2(585, 65);
        Vector2 bottom1Vector = new Vector2(125, 325);
        Vector2 bottom2Vector = new Vector2(265, 325);
        Vector2 bottom3Vector = new Vector2(445, 325);
        Vector2 bottom4Vector = new Vector2(585, 325);

        // all the Textures
        Texture2D leftArrow;
        Texture2D rightArrow;
        Texture2D shiftLeft;
        Texture2D shiftRight;
        Texture2D downArrow;
        Texture2D upArrow;
        Texture2D x;
        
        // all the rects used
        Rectangle xRect = new Rectangle(740, 0, 59, 59);
        public Rectangle rightUpShift = new Rectangle(700, 60, 58, 100);
        public Rectangle leftUpShift = new Rectangle(40, 60, 58, 160);
        Rectangle rightBottomShift = new Rectangle(700, 320, 59, 100);
        Rectangle leftBottomShift = new Rectangle(40, 320, 59, 100);
        Rectangle downArrow1 = new Rectangle(120, 180, 100, 40);
        Rectangle downArrow2 = new Rectangle(260, 180, 100, 40);
        Rectangle downArrow3 = new Rectangle(440, 180, 100, 40);
        Rectangle downArrow4 = new Rectangle(580, 180, 100, 40);
        Rectangle upArrow1 = new Rectangle(120, 260, 100, 40);
        Rectangle upArrow2 = new Rectangle(260, 260, 100, 40);
        Rectangle upArrow3 = new Rectangle(440, 260, 100, 40);
        Rectangle upArrow4 = new Rectangle(580, 260, 100, 40);
        public Rectangle upRHalf1 = new Rectangle(170, 60, 50, 100);
        public Rectangle upRHalf2 = new Rectangle(310, 60, 50, 100);
        public Rectangle upRHalf3 = new Rectangle(490, 60, 50, 100);
        public Rectangle upRHalf4 = new Rectangle(630, 60, 50, 100);
        public Rectangle upLHalf1 = new Rectangle(120, 60, 50, 100);
        public Rectangle upLHalf2 = new Rectangle(260, 60, 50, 100);
        public Rectangle upLHalf3 = new Rectangle(440, 60, 50, 100);
        public Rectangle upLHalf4 = new Rectangle(580, 60, 50, 100);
        Rectangle bottomRHalf1 = new Rectangle(170, 320, 50, 100);
        Rectangle bottomRHalf2 = new Rectangle(310, 320, 50, 100);
        Rectangle bottomRHalf3 = new Rectangle(490, 320, 50, 100);
        Rectangle bottomRHalf4 = new Rectangle(630, 320, 50, 100);
        Rectangle bottomLHalf1 = new Rectangle(120, 320, 50, 100);
        Rectangle bottomLHalf2 = new Rectangle(260, 320, 50, 100);
        Rectangle bottomLHalf3 = new Rectangle(440, 320, 50, 100);
        Rectangle bottomLHalf4 = new Rectangle(580, 320, 50, 100);

        public LineupToolbox(SpriteFont n, bool l, ContentManager Content)
        {
            nameDisplay = n;
            inLineup = l;

            leftArrow = Content.Load<Texture2D>("Graphics\\UI\\left_hover_arrow");
            rightArrow = Content.Load<Texture2D>("Graphics\\UI\\right_hover_arrow");
            shiftLeft = Content.Load<Texture2D>("Graphics\\UI\\left_shift_arrow");
            shiftRight = Content.Load<Texture2D>("Graphics\\UI\\right_shift_arrow");
            downArrow = Content.Load<Texture2D>("Graphics\\UI\\down_hover_arrow");
            upArrow = Content.Load<Texture2D>("Graphics\\UI\\up_hover_arrow");
            x = Content.Load<Texture2D>("Graphics\\UI\\x");
        }

        public void LineupScreen(SpriteBatch spriteBatch, MouseState currentMouseState, ContentManager Content, Player player)
        {
            // redraw black background
            spriteBatch.Draw(Content.Load<Texture2D>("Graphics\\UI\\lineup_ui_2"), Vector2.Zero, Color.White);

            if (!allowMove && currentMouseState.LeftButton == ButtonState.Released) allowMove = true;

            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                // check if X is pressed
                if (xRect.Contains(currentMouseState.Position))
                    inLineup = false;

                // check if lineup should be moved right or left
                else if (rightUpShift.Contains(currentMouseState.Position) && (startingFrame + 4) <= player.Lineup.Count - 1 && (startingFrame + 4) >= 0 && allowShift)
                {
                    startingFrame += 4;
                    allowShift = false;
                }
                else if (leftUpShift.Contains(currentMouseState.Position) && (startingFrame - 4) <= player.Lineup.Count - 1 && (startingFrame - 4) >= 0 && allowShift)
                {
                    startingFrame -= 4;
                    allowShift = false;
                }
                // check if backup should be moved right or left 
                else if (rightBottomShift.Contains(currentMouseState.Position) && (bottomStarting + 4) <= player.Backup.Count - 1 && (bottomStarting + 4) >= 0)
                    bottomStarting += 4;
                else if (leftBottomShift.Contains(currentMouseState.Position) && (bottomStarting - 4) <= player.Backup.Count - 1 && (bottomStarting - 4) >= 0)
                    bottomStarting -= 4;
                // check if any lineup member should be moved to backup
                else if (downArrow1.Contains(currentMouseState.Position) && allowMove && player.Lineup.Count - 1 > 0)
                    TransferToBackup(0, player);
                else if (downArrow2.Contains(currentMouseState.Position) && allowMove && player.Lineup.Count - 1 > 0)
                    TransferToBackup(1, player);
                else if (downArrow3.Contains(currentMouseState.Position) && allowMove && player.Lineup.Count - 1 > 0)
                    TransferToBackup(2, player);
                else if (downArrow4.Contains(currentMouseState.Position) && allowMove && player.Lineup.Count - 1 > 0)
                    TransferToBackup(3, player);
                // check if any backup member should be moved to lineup
                else if (upArrow1.Contains(currentMouseState.Position) && allowMove)
                    TransferToLineup(0, player);
                else if (upArrow2.Contains(currentMouseState.Position) && allowMove)
                    TransferToLineup(1, player);
                else if (upArrow3.Contains(currentMouseState.Position) && allowMove)
                    TransferToLineup(2, player);
                else if (upArrow4.Contains(currentMouseState.Position) && allowMove)
                    TransferToLineup(3, player);
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
                // check if any backup member should be moved right
                else if (bottomRHalf1.Contains(currentMouseState.Position) && allowMove)
                    MoveRight(player.Backup, bottomStarting);
                else if (bottomRHalf2.Contains(currentMouseState.Position) && allowMove)
                    MoveRight(player.Backup, bottomStarting + 1);
                else if (bottomRHalf3.Contains(currentMouseState.Position) && allowMove)
                    MoveRight(player.Backup, bottomStarting + 2);
                else if (bottomRHalf4.Contains(currentMouseState.Position) && allowMove)
                    MoveRight(player.Backup, bottomStarting + 3);
                // check if any backup member should be moved left
                else if (bottomLHalf1.Contains(currentMouseState.Position) && allowMove)
                    MoveLeft(player.Backup, bottomStarting);
                else if (bottomLHalf2.Contains(currentMouseState.Position) && allowMove)
                    MoveLeft(player.Backup, bottomStarting + 1);
                else if (bottomLHalf3.Contains(currentMouseState.Position) && allowMove)
                    MoveLeft(player.Backup, bottomStarting + 2);
                else if (bottomLHalf4.Contains(currentMouseState.Position) && allowMove)
                    MoveLeft(player.Backup, bottomStarting + 3);
            }

            spriteBatch.DrawString(nameDisplay, "Active Lineup", new Vector2(100, 20), Color.White);
            spriteBatch.DrawString(nameDisplay, "Storage", new Vector2(100, 449), Color.Red);
            if (startingFrame <= player.Lineup.Count - 1)
            {
                // draw the pixels to screen
                for (int i = startingFrame; i < startingFrame + 4; i++)
                {
                    if (i <= player.Lineup.Count - 1 && i >= 0)
                    {
                        //spriteBatch.DrawString(nameDisplay, (startingFrame + 1).ToString(),
                        //    new Vector2(up1Vector.X, up1Vector.Y - 25), Color.Black);
                        //spriteBatch.DrawString(nameDisplay, (startingFrame + 2).ToString(),
                        //    new Vector2(up2Vector.X, up2Vector.Y - 25), Color.Black);
                        //spriteBatch.DrawString(nameDisplay, (startingFrame + 3).ToString(),
                        //    new Vector2(up3Vector.X, up3Vector.Y - 25), Color.Black);
                        //spriteBatch.DrawString(nameDisplay, (startingFrame + 4).ToString(),
                        //    new Vector2(up4Vector.X, up4Vector.Y - 25), Color.Black);
                        var pixel = player.Lineup[i];
                        if (player.Lineup.IndexOf(pixel) == startingFrame)
                        {
                            spriteBatch.DrawString(nameDisplay, "Level " + (pixel.Level).ToString(),
                            new Vector2(up1Vector.X, up1Vector.Y - 25), Color.Black);
                            spriteBatch.Draw(pixel.Texture, up1Vector, Color.White);
                            spriteBatch.DrawString(nameDisplay, (ShortenName(pixel.Name, pixel.ID)),
                                new Vector2(up1Vector.X - 10, up1Vector.Y + 95), Color.Black);
                        }
                        else if (player.Lineup.IndexOf(pixel) == startingFrame + 1)
                        {
                            spriteBatch.DrawString(nameDisplay, "Level " + (pixel.Level).ToString(),
                            new Vector2(up2Vector.X, up2Vector.Y - 25), Color.Black);
                            spriteBatch.Draw(pixel.Texture, up2Vector, Color.White);
                            spriteBatch.DrawString(nameDisplay, (ShortenName(pixel.Name, pixel.ID)),
                               new Vector2(up2Vector.X - 10, up2Vector.Y + 95), Color.Black);
                        }
                        else if (player.Lineup.IndexOf(pixel) == startingFrame + 2)
                        {
                            spriteBatch.DrawString(nameDisplay, "Level " + (pixel.Level).ToString(),
                            new Vector2(up3Vector.X, up3Vector.Y - 25), Color.Black);
                            spriteBatch.Draw(pixel.Texture, up3Vector, Color.White);
                            spriteBatch.DrawString(nameDisplay, (ShortenName(pixel.Name, pixel.ID)),
                               new Vector2(up3Vector.X - 10, up3Vector.Y + 95), Color.Black);
                        }
                        else if (player.Lineup.IndexOf(pixel) == startingFrame + 3)
                        {
                            spriteBatch.DrawString(nameDisplay, "Level " + (pixel.Level).ToString(),
                            new Vector2(up4Vector.X, up4Vector.Y - 25), Color.Black);
                            spriteBatch.Draw(pixel.Texture, up4Vector, Color.White);
                            spriteBatch.DrawString(nameDisplay, (ShortenName(pixel.Name, pixel.ID)),
                               new Vector2(up4Vector.X - 10, up4Vector.Y + 95), Color.Black);
                        }
                    }
                }
            }
            if (bottomStarting <= player.Backup.Count - 1)
            {
                for (int i = bottomStarting; i < bottomStarting + 4; i++)
                {
                    if (i <= player.Backup.Count - 1 && i >= 0)
                    {
                        spriteBatch.DrawString(nameDisplay, (bottomStarting + 1).ToString(),
                            new Vector2(bottom1Vector.X, bottom1Vector.Y - 25), Color.White);
                        spriteBatch.DrawString(nameDisplay, (bottomStarting + 2).ToString(),
                            new Vector2(bottom2Vector.X, bottom2Vector.Y - 25), Color.White);
                        spriteBatch.DrawString(nameDisplay, (bottomStarting + 3).ToString(),
                            new Vector2(bottom3Vector.X, bottom3Vector.Y - 25), Color.White);
                        spriteBatch.DrawString(nameDisplay, (bottomStarting + 4).ToString(),
                            new Vector2(bottom4Vector.X, bottom4Vector.Y - 25), Color.White);
                        var pixel = player.Backup[i];
                        if (player.Backup.IndexOf(pixel) == bottomStarting)
                        {
                            spriteBatch.Draw(pixel.Texture, bottom1Vector, Color.White);
                            spriteBatch.DrawString(nameDisplay, (ShortenName(pixel.Name, pixel.ID)),
                                new Vector2(bottom1Vector.X - 10, bottom1Vector.Y + 95), Color.White);
                        }
                        else if (player.Backup.IndexOf(pixel) == bottomStarting + 1)
                        {

                            spriteBatch.Draw(pixel.Texture, bottom2Vector, Color.White);
                            spriteBatch.DrawString(nameDisplay, (ShortenName(pixel.Name, pixel.ID)),
                                new Vector2(bottom2Vector.X - 10, bottom2Vector.Y + 95), Color.White);
                        }
                        else if (player.Backup.IndexOf(pixel) == bottomStarting + 2)
                        {
                            spriteBatch.Draw(pixel.Texture, bottom3Vector, Color.White);
                            spriteBatch.DrawString(nameDisplay, (ShortenName(pixel.Name, pixel.ID)),
                                new Vector2(bottom3Vector.X - 10, bottom3Vector.Y + 95), Color.White);
                        }
                        else if (player.Backup.IndexOf(pixel) == bottomStarting + 3)
                        {
                            spriteBatch.Draw(pixel.Texture, bottom4Vector, Color.White);
                            spriteBatch.DrawString(nameDisplay, (ShortenName(pixel.Name, pixel.ID)),
                                new Vector2(bottom4Vector.X - 10, bottom4Vector.Y + 95), Color.White);
                        }
                    }
                }
            }

            if (currentMouseState.LeftButton == ButtonState.Released)
            {
                if (!allowShift) allowShift = true;

                // draw the hover arrows for the lineup 
                if (upLHalf1.Contains(currentMouseState.Position))
                    spriteBatch.Draw(leftArrow, new Vector2(120, 60), Color.White);
                else if (upRHalf1.Contains(currentMouseState.Position))
                    spriteBatch.Draw(rightArrow, new Vector2(170, 60), Color.White);
                else if (upLHalf2.Contains(currentMouseState.Position))
                    spriteBatch.Draw(leftArrow, new Vector2(260, 60), Color.White);
                else if (upRHalf2.Contains(currentMouseState.Position))
                    spriteBatch.Draw(rightArrow, new Vector2(310, 60), Color.White);
                else if (upLHalf3.Contains(currentMouseState.Position))
                    spriteBatch.Draw(leftArrow, new Vector2(440, 60), Color.White);
                else if (upRHalf3.Contains(currentMouseState.Position))
                    spriteBatch.Draw(rightArrow, new Vector2(490, 60), Color.White);
                else if (upLHalf4.Contains(currentMouseState.Position))
                    spriteBatch.Draw(leftArrow, new Vector2(580, 60), Color.White);
                else if (upRHalf4.Contains(currentMouseState.Position))
                    spriteBatch.Draw(rightArrow, new Vector2(630, 60), Color.White);
                // draw the hover arrows for backup
                else if (bottomLHalf1.Contains(currentMouseState.Position))
                    spriteBatch.Draw(leftArrow, new Vector2(120, 320), Color.White);
                else if (bottomRHalf1.Contains(currentMouseState.Position))
                    spriteBatch.Draw(rightArrow, new Vector2(170, 320), Color.White);
                else if (bottomLHalf2.Contains(currentMouseState.Position))
                    spriteBatch.Draw(leftArrow, new Vector2(260, 320), Color.White);
                else if (bottomRHalf2.Contains(currentMouseState.Position))
                    spriteBatch.Draw(rightArrow, new Vector2(310, 320), Color.White);
                else if (bottomLHalf3.Contains(currentMouseState.Position))
                    spriteBatch.Draw(leftArrow, new Vector2(440, 320), Color.White);
                else if (bottomRHalf3.Contains(currentMouseState.Position))
                    spriteBatch.Draw(rightArrow, new Vector2(490, 320), Color.White);
                else if (bottomLHalf4.Contains(currentMouseState.Position))
                    spriteBatch.Draw(leftArrow, new Vector2(580, 320), Color.White);
                else if (bottomRHalf4.Contains(currentMouseState.Position))
                    spriteBatch.Draw(rightArrow, new Vector2(630, 320), Color.White);
                // draw the hover down arrows
                else if (downArrow1.Contains(currentMouseState.Position))
                    spriteBatch.Draw(downArrow, new Vector2(120, 180), Color.White);
                else if (downArrow2.Contains(currentMouseState.Position))
                    spriteBatch.Draw(downArrow, new Vector2(260, 180), Color.White);
                else if (downArrow3.Contains(currentMouseState.Position))
                    spriteBatch.Draw(downArrow, new Vector2(440, 180), Color.White);
                else if (downArrow4.Contains(currentMouseState.Position))
                    spriteBatch.Draw(downArrow, new Vector2(580, 180), Color.White);
                // draw the hover up arrows
                else if (upArrow1.Contains(currentMouseState.Position))
                    spriteBatch.Draw(upArrow, new Vector2(120, 260), Color.White);
                else if (upArrow2.Contains(currentMouseState.Position))
                    spriteBatch.Draw(upArrow, new Vector2(260, 260), Color.White);
                else if (upArrow3.Contains(currentMouseState.Position))
                    spriteBatch.Draw(upArrow, new Vector2(440, 260), Color.White);
                else if (upArrow4.Contains(currentMouseState.Position))
                    spriteBatch.Draw(upArrow, new Vector2(580, 260), Color.White);
                // draw the shift right/left arrows
                else if (rightUpShift.Contains(currentMouseState.Position))
                    spriteBatch.Draw(shiftRight, new Vector2(700, 60), Color.White);
                else if (leftUpShift.Contains(currentMouseState.Position))
                    spriteBatch.Draw(shiftLeft, new Vector2(40, 60), Color.White);
                else if (rightBottomShift.Contains(currentMouseState.Position))
                    spriteBatch.Draw(shiftRight, new Vector2(700, 320), Color.White);
                else if (leftBottomShift.Contains(currentMouseState.Position))
                    spriteBatch.Draw(shiftLeft, new Vector2(40, 320), Color.White);
                // draw the x button
                else if (xRect.Contains(currentMouseState.Position))
                    spriteBatch.Draw(x, new Vector2(740, 0), Color.White);
            }

        }

        public string ShortenName(string name, int id)
        {
            string final = name + " " + id.ToString();
            if (name.Length > 11)
            {
                final = name.Substring(0, 11) + " " + id.ToString();
            }
            return final;
        }

        private void TransferToBackup(int offset, Player player)
        {
            allowMove = false;
            if (player.Lineup.Count - 1 >= startingFrame + offset)
            {
                if (player.Backup.Count - 1 >= startingFrame + offset)
                    player.Backup.Insert(startingFrame + offset, player.Lineup[startingFrame + offset]);
                else
                    player.Backup.Add(player.Lineup[startingFrame + offset]);
                player.Lineup.RemoveAt(startingFrame + offset);
                player.RegenList.RemoveAt(startingFrame + offset);
            }
        }

        private void TransferToLineup(int offset, Player player)
        {
            allowMove = false;
            if (player.Backup.Count - 1 >= bottomStarting + offset && player.Lineup.Count + 1 <= player.MaxPixels)
            {
                if (player.Lineup.Count - 1 > bottomStarting + offset)
                {
                    player.Lineup.Insert(bottomStarting + offset, player.Backup[bottomStarting + offset]);
                    player.RegenList.Insert(bottomStarting + offset, player.Backup[bottomStarting + offset]);
                }
                else
                {
                    player.Lineup.Add(player.Backup[bottomStarting + offset]);
                    player.RegenList.Add(player.Backup[bottomStarting + offset]);
                }
                player.Backup.RemoveAt(bottomStarting + offset);
            }
        }

        public void MoveRight(List<IPixel> ls, int index, List<IPixel> secondary = null)
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

        public void MoveLeft(List<IPixel> ls, int index, List<IPixel> secondary = null)
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
