using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballGame.Framework;

namespace FootballGame
{
    public class Playbook
    {
        const int MAX_PLAYS = 20;
        const int MAX_FORMATIONS = 20;

        public int NumFormations;
        public Formation[] Formations;
        public int[] NumPlays;
        public Play[,] Plays;
        
        public Playbook()
        {
            this.Formations = new Formation[MAX_FORMATIONS];
            this.NumPlays = new int[MAX_FORMATIONS];
            this.Plays = new Play[MAX_FORMATIONS, MAX_PLAYS];
        }

        public void Load(string directory, string filename)
        {
            var file = new GameFile(Path.Combine(directory, $"Rosters\\{filename}.txt"));

            // Read the number of formations

            this.NumFormations = int.Parse(file.ReadLine());


            // For each formation

            for (int i = 0; i < this.NumFormations; i++)
            {


                // Read the formation name and load the formation

                var text = file.ReadLine();

                this.Formations[i] = new Formation();
                this.Formations[i].Load(text);



                // Read the number of plays

                this.NumPlays[i] = int.Parse(file.ReadLine());



                // Read each play and load the play

                for (int ii = 0; ii < NumPlays[i]; ii++)
                {
                    var playFile = file.ReadLine().Trim();

                    this.Plays[i, ii] = new Play();
                    this.Plays[i, ii].Load(playFile);
                }
            }
        }
    }
}
