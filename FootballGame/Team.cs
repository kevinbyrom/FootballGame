using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FootballGame.Framework;


namespace FootballGame
{
    public enum StadiumType
    {
        Grass = 0,
        Turf = 1,
        Dome = 2
    }


    public struct Stadium
    {
        public string Name;
        public StadiumType Type;
    }


    public class Team
    {
        const int OFFENSE = 0;
        const int DEFENSE = 1;

        public string Name;
        public string City;
        public string State;
        public Playbook[] Playbooks;
        public Roster Roster;
        public DepthChart DepthChart;

        public string PlayerTextureName;
        public string FieldTextureName;

        public int ControlledBy;
        public int SelectedFormation;
        public int SelectedPlay;

        public Team()
        {
            this.Playbooks = new Playbook[2];
            this.Roster = new Roster();
            this.DepthChart = new DepthChart();
        }

        public void Load(string directory, string team, bool hometeam)
        {

            // Load the gamefile

            string filename = Path.Combine(directory, $"TEAMS\\{team}.txt");

            GameFile file = new GameFile(filename);

            
            // Load details

            this.Name = file.ReadLine();
            this.City = file.ReadLine();
            this.State = file.ReadLine();

            
            // Load texture names

            this.PlayerTextureName = String.Format("{0}_{1}", team, hometeam ? "home" : "away");
            this.FieldTextureName = $"{team}_field";


            // Load playbooks, roster and depth charts
            
            this.Playbooks[OFFENSE] = new Playbook();
            this.Playbooks[OFFENSE].Load("Playbooks\\Offensive\\", team);

            this.Playbooks[DEFENSE] = new Playbook();
            this.Playbooks[DEFENSE].Load("Playbooks\\Offensive\\", team);

            this.Roster = new Roster();
            this.Roster.Load(directory, team);

            this.DepthChart = new DepthChart();
            this.DepthChart.Load(directory, team);

            throw new NotImplementedException();
        }
    }
}
