using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballGame.Framework;


namespace FootballGame
{
    public class Formation
    {
        const int MAX_SIDE_PLAYERS = 11;

        public string Name;
        public string GraphicFilename;
        public int[] DepthType;
        public int[] DepthPos;
        public float[] OffsetWX;
        public float[] OffsetWY;
        public bool IsSpecialTeams;
        public bool IsKickoff;

        public Formation()
        {
            this.Name = null;
            this.GraphicFilename = null;
            this.DepthType = new int[MAX_SIDE_PLAYERS];
            this.DepthPos = new int[MAX_SIDE_PLAYERS];
            this.OffsetWX = new float[MAX_SIDE_PLAYERS];
            this.OffsetWY = new float[MAX_SIDE_PLAYERS];
            this.IsSpecialTeams = false;
            this.IsKickoff = false;
        }


        public void Load(string name)
        {
            var file = new GameFile($"Formations\\{name}.txt");


            // Read the formation name and graphic file

            Name = file.ReadLine();
            GraphicFilename = file.ReadLine();


            // Read the SpecialTeams and Kickoff flag

            IsSpecialTeams = int.Parse(file.ReadLine()) == 1;
            IsKickoff = int.Parse(file.ReadLine()) == 1;


            // Read each player type and offset

            for (int i = 0; i < MAX_SIDE_PLAYERS; i++)
            {
                var p = file.ReadParams();

                if (p.Count() < 2)
                    throw new Exception("ERROR::NOT ENOUGH PARAMS FOR OFFSETS IN FORMATION FILE");
  
                this.DepthType[i] = int.Parse(p[0]);
                this.DepthPos[i] = int.Parse(p[1]);


                p = file.ReadParams();

                if (p.Count() < 2)
                    throw new Exception("ERROR::NOT ENOUGH PARAMS FOR OFFSETS IN FORMATION FILE");
                this.OffsetWX[i] = float.Parse(p[0]);
                this.OffsetWY[i] = float.Parse(p[1]);
            }
        }
    }
}
