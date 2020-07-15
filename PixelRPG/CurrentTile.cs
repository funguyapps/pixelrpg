using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelRPG
{
    public class CurrentTile
    {
        private string upTexture;
        public string UpTextureStr { get => upTexture; set { upTexture = value; } }

        private string downTexture;
        public string DownTextureStr { get => downTexture; set { downTexture = value; } }

        private string rightTexture;
        public string RightTextureStr { get => rightTexture; set { rightTexture = value; } }

        private string leftTexture;
        public string LeftTextureStr { get => leftTexture; set { leftTexture = value; } }

        private string currentTextureStr;
        public string CurrentTextureStr { get => currentTextureStr; set { currentTextureStr = value; } }

        private Texture2D currentTexture;
        public Texture2D CurrentTexture { get => currentTexture; set { currentTexture = value; } }

        //private int spawnRate = 251;
        private int spawnRate = 501;
        public int SpawnRate { get => spawnRate; set { spawnRate = value; } }

        private RegionType currentRegion;
        public RegionType CurrentRegion { get => currentRegion; set { currentRegion = value; } }

        private Islands currentIsland = Islands.home;
        public Islands CurrentIsland { get => currentIsland; set { currentIsland = value;  } }

        private Towns currentTown = Towns.Domus;
        public Towns CurrentTown { get => currentTown; set => currentTown = value; }
    }
}
