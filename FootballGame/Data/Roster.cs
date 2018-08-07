using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballGame.Framework;


namespace FootballGame
{
    public class Roster
    {
        const int MAX_ROSTER_PLAYERS = 50;
        const int INVALID_ROSTER_POS = 255;

        public int NumPlayers;
        public PlayerInfo[] PlayerInfo;

        public Roster()
        {
            this.PlayerInfo = new PlayerInfo[MAX_ROSTER_PLAYERS];
        }

        public PlayerInfo GetPlayerInfo(int pos)
        {
            if (pos < 0 || pos >= MAX_ROSTER_PLAYERS || pos == INVALID_ROSTER_POS)
                return null;

            return this.PlayerInfo[pos];
        }

        public void RemoveAllFromPlay()
        {
            for (int pos = 0; pos < MAX_ROSTER_PLAYERS; pos++)
                PlayerInfo[pos].InPlay = false;
        }

        public void Load(string name)
        {
            var file = new GameFile($"Rosters\\{name}.txt");

            // Read the number of players

            this.NumPlayers = int.Parse(file.ReadLine());


            // Load each player

            for (int i = 0; i < this.NumPlayers; i++)
            {
                var playerName = file.ReadLine();

                this.PlayerInfo[i].Load(playerName);
            }
        }
    }
}
