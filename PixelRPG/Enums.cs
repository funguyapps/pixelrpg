using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG
{
    public enum Directions
    {
        up,
        down,
        left,
        right
    }

    public enum ElementType
    {
        Fire,
        Water,
        Air,
        Earth,
        Dark
    }

    public enum RegionType
    {
        forest,
        plain,
        volcano,
        ice,
        mountain,
        beach
    }

    public enum MoveTypes
    {
        offensive,
        defensive
    }

    public enum Stats
    {
        attack,
        defense,
        speed,
        health,
        none
    }

    public enum Islands
    {
        home,
        air,
        ice,
        fire,
        earth,
        dark
    }

    public enum Rarity
    {
        common,
        rare,
        legendary
    }

    public enum Towns
    {
        Domus,
        Oceanic,
        none
    }

    public enum QuestTypes
    {
        capture,
        travel,
        kill
    }
}
