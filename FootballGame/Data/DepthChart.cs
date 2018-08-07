using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballGame.Framework;


namespace FootballGame
{
    public class DepthChart
    {
        const int NUM_DEPTH_TYPES = 17;
        const int NUM_DEPTH_SLOTS = 6;
        const int INVALID_ROSTER_POS = 255;

        public int[] NumSlotsFilled;
        public int[,] RosterPos;

        public DepthChart()
        {
            this.NumSlotsFilled = new int[NUM_DEPTH_TYPES];
            this.RosterPos = new int[NUM_DEPTH_TYPES, NUM_DEPTH_SLOTS];
        }

        public int GetRosterPos(int type, int slot)
        {
            if (type < 0 || type >= NUM_DEPTH_TYPES ||
                slot < 0 || slot >= NUM_DEPTH_SLOTS)
                return INVALID_ROSTER_POS;

            return this.RosterPos[type, slot];
        }


        public void Load(string team)
        {
            var file = new GameFile($"Depth\\{team}.txt");

            for (int i = 0; i < NUM_DEPTH_TYPES; i++)
            {
                // Read the number of slots filled

                this.NumSlotsFilled[i] = int.Parse(file.ReadLine());

                for (int ii = 0; ii < this.NumSlotsFilled[i]; ii++)
                {

                    // Read type roster pos

                    this.RosterPos[i, ii] = int.Parse(file.ReadLine());
                }
            }
        }
    }
}
