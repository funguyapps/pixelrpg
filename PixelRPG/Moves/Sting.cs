﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG.Moves
{
    public class Sting : IMove
    {
        public override int NumTargets => 1;
        public override MoveTypes MoveType => MoveTypes.offensive;
        public override Stats AffectedStat => Stats.none;
        public override string TextureStr { get => "Graphics\\Moves\\sting"; }

        public override int CalculateDamage()
        {
            return AttackStat;
        }

        public override string ToString()
        {
            //return "Bite Damage: " + Damage;
            return "Sting";
        }

        public override string enemyToString()
        {
            return "Sting Damage: ";
        }

        public override string InfoDamage()
        {
            return this.Damage.ToString();
        }
    }
}
