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

        public void Load(string name, bool isHome)
        {

            // Load the gamefile

            GameFile file = new GameFile(Path.Combine($"TEAMS\\{name}.txt"));

            
            // Load details

            this.Name = file.ReadLine();
            this.City = file.ReadLine();
            this.State = file.ReadLine();

            
            // Load texture names

            this.PlayerTextureName = String.Format("{0}_{1}", name, isHome ? "home" : "away");
            this.FieldTextureName = $"{name}_field";


            // Load playbooks, roster and depth charts
            
            this.Playbooks[OFFENSE] = new Playbook();
            this.Playbooks[OFFENSE].Load("Offensive\\" + name);

            this.Playbooks[DEFENSE] = new Playbook();
            this.Playbooks[DEFENSE].Load("Defensive\\" + name);

            this.Roster = new Roster();
            this.Roster.Load(name);

            this.DepthChart = new DepthChart();
            this.DepthChart.Load(name);
        }
    }
}
