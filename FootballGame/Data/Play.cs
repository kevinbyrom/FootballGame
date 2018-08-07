using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballGame.Framework;


namespace FootballGame
{
    public struct PlayerObjFlags
    {
        public bool Passer;
        public bool Receiver;
        public bool IsDefReceiver;
        public bool Blocker;
        public bool Rusher;
        public bool Kicker;
        public bool Punter;
        public bool Placeholder;
        public bool IsDefBallHolder;
    }


    public class Play
    {
        const int MAX_SIDE_PLAYERS = 11;

        public string Name;
        public string GraphicFilename;
        public PlayerObjFlags[] PlayerFlags;
        public GameActions[] PreHikeActions;
        public GameActions[] PostHikeActions;


        public Play()
        {
            this.Name = null;
            this.GraphicFilename = null;
            this.PlayerFlags = new PlayerObjFlags[MAX_SIDE_PLAYERS];
            this.PreHikeActions = new GameActions[MAX_SIDE_PLAYERS];
            this.PostHikeActions = new GameActions[MAX_SIDE_PLAYERS];
        }


        public void Load(string name)
        {
            var file = new GameFile($"Plays\\{name}.txt");


            // Read the play name & graphic file

            this.Name = file.ReadLine();
            this.GraphicFilename = file.ReadLine();


            // Load each player data

            for (int i = 0; i < MAX_SIDE_PLAYERS; i++)
            {

                // Read the player type

                var p = file.ReadParams();

                if (p.Count() < 9)
                    throw new Exception("ERROR :: NOT ENOUGH PARAMS FOR PLAYER TYPE IN PLAY FILE");

                this.PlayerFlags[i] = new PlayerObjFlags();
                this.PlayerFlags[i].Passer = bool.Parse(p[0]);
                this.PlayerFlags[i].Receiver = bool.Parse(p[1]);
                this.PlayerFlags[i].IsDefReceiver = bool.Parse(p[2]);
                this.PlayerFlags[i].Blocker = bool.Parse(p[3]);
                this.PlayerFlags[i].Rusher = bool.Parse(p[4]);
                this.PlayerFlags[i].Kicker = bool.Parse(p[5]);
                this.PlayerFlags[i].Punter = bool.Parse(p[6]);
                this.PlayerFlags[i].Placeholder = bool.Parse(p[7]);
                this.PlayerFlags[i].IsDefBallHolder = bool.Parse(p[8]);


                // Read each prehike action

                this.PreHikeActions[i] = new GameActions();

                var numActions = int.Parse(file.ReadLine());

                for (int ii = 0; ii < numActions; ii++)
                {
                    var actionParams = file.ReadParams();

                    if (actionParams.Count() < 5)
                        throw new Exception("ERROR :: NOT ENOUGH PARAMS FOR PRE HIKE ACTION IN PLAY FILE");

                    this.PreHikeActions[i].PushEnd(
                        int.Parse(actionParams[0]),
                        int.Parse(actionParams[1]),
                        float.Parse(actionParams[2]),
                        float.Parse(actionParams[3]),
                        float.Parse(actionParams[4]));
                }

                // Read each post action

                this.PostHikeActions[i] = new GameActions();

                numActions = int.Parse(file.ReadLine());

                for (int ii = 0; ii < numActions; ii++)
                {
                    var actionParams = file.ReadParams();

                    if (actionParams.Count() < 5)
                        throw new Exception("ERROR :: NOT ENOUGH PARAMS FOR POST HIKE ACTION IN PLAY FILE");

                    this.PostHikeActions[i].PushEnd(
                        int.Parse(actionParams[0]),
                        int.Parse(actionParams[1]),
                        float.Parse(actionParams[2]),
                        float.Parse(actionParams[3]),
                        float.Parse(actionParams[4]));
                }
            }
        }
    }
}
