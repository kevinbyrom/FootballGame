using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FootballGame
{
    public struct KickAim
    {
        bool Active;
        Vector2 Scr;
        Vector2 Wld;
        Vector2 Origin;
        float Delta;
    }

    public class DownInfo
    {
        public bool Done;
        public int DoneReason;
        public int Down;
        public int YardsToGo;
        public int OffenseDir;
        public int DefenseDir;
        public float ScrimmagePos;
        public float FirstDownPos;
        public float BallSpotPos;
        public bool BallHiked;
        public bool BallPassed;
        public bool PassedScrimmage;
        public bool Turnover;
        public KickAim KickAim;
        public int[] CurrPlay;

        public DownInfo()
        {
            this.CurrPlay = new int[Constants.NUM_SIDES];
        }
    }
}
