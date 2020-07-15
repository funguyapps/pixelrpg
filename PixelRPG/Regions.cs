using PixelRPG.Pixels;
using PixelRPG.Pixels.Air;
using PixelRPG.Pixels.Earth;
using PixelRPG.Pixels.Fire;
using PixelRPG.Pixels.Water;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG
{
    static class Regions
    {
        // used to get a random pixel from each List of possible enemies
        static Random rand = new Random();

        // List of all possible enemies found in a forest region
        private static List<IPixel> possibleEnemies = new List<IPixel>();

        /// <summary>
        /// Used to get a random enemy from the current region
        /// </summary>
        /// <param name="currentRegion">The current region, used to determine what enemy to return</param>
        /// <returns>An instance of a random enemy of type IPixel</returns>
        public static IPixel GetEnemies(Islands island, RegionType currentRegion, Rarity rarity, int x, int y)
        {
            if (island == Islands.home)
            {
                possibleEnemies = GetPossible(rarity, currentRegion, x, y);
            }
            //if (currentRegion == RegionType.forest)
            //{
            //    var pixel = forestEnemies[rand.Next(0, forestEnemies.Count - 1)];
            //    IPixel pix = (IPixel)Activator.CreateInstance(pixel.GetType());

            //    return pix;
            //}

            IPixel pixel = null;
            if (possibleEnemies.Count > 1)
            {
                pixel = possibleEnemies[rand.Next(0, possibleEnemies.Count)];
            }
            else if (possibleEnemies.Count == 1)
            {
                pixel = possibleEnemies[0];
            }
            else
            {
                return null;
            }
            IPixel pix = (IPixel)Activator.CreateInstance(pixel.GetType());
            return pix;
        }

        private static List<IPixel> GetPossible(Rarity rarity, RegionType region, int x, int y)
        {
            List<IPixel> returnList = new List<IPixel>();

            if (region == RegionType.plain || region == RegionType.forest)
            {
                if (rarity == Rarity.common)
                {
                    if (x <= 4 && y <= 3)
                    {
                        returnList.Add(new ChipBaby());
                        returnList.Add(new BlossBaby());
                    }
                }
                else if (rarity == Rarity.rare)
                {
                    returnList.Add(new Arbaby());
                    returnList.Add(new Chombaby());
                }
                else if (rarity == Rarity.legendary)
                {
                    returnList.Add(new EarthBaby());
                }
            }
            else if (region == RegionType.mountain)
            {
                if (rarity == Rarity.common)
                {
                    returnList.Add(new Nimbaby());
                    returnList.Add(new Drobaby());
                }
                else if (rarity == Rarity.rare)
                {
                    returnList.Add(new Wisbaby());
                    returnList.Add(new GreenBaby());
                }
                else if (rarity == Rarity.legendary)
                {
                    returnList.Add(new AirBaby());
                }
            }
            else if (region == RegionType.beach)
            {
                if (rarity == Rarity.common)
                {
                    if (x < 8)
                    {
                        returnList.Add(new Crababy());
                        returnList.Add(new Spoubaby());
                    }
                    else
                    {
                        returnList.Add(new FoxBaby());
                        returnList.Add(new Eruption());
                    }
                        
                }
                else if (rarity == Rarity.rare)
                {
                    if (x < 8)
                    {
                        returnList.Add(new Mantbaby());
                        returnList.Add(new Narbaby());
                    }
                    else
                    {
                        returnList.Add(new Salababy());
                        returnList.Add(new Ignis());
                    }
                }
                else if (rarity == Rarity.legendary)
                {
                    if (y > 2 && x < 8)
                        returnList.Add(new WaterBaby());
                    else
                        returnList.Add(new FireBaby());
                }
            }
            else if (region == RegionType.ice)
            {
                if (rarity == Rarity.common)
                {
                    returnList.Add(new Crababy());
                    returnList.Add(new Spoubaby());
                }
                else if (rarity == Rarity.rare)
                {
                    returnList.Add(new Mantbaby());
                    returnList.Add(new Narbaby());
                }
                else if (rarity == Rarity.legendary)
                {
                    returnList.Add(new WaterBaby());
                }
            }
            return returnList;
        }
    }
}
