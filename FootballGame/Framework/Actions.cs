using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballGame.Framework
{
    public class GameAction
    {
        public int Type;
        public int Time;
        public float Param1;
        public float Param2;
        public float Param3;
    }

    public class GameActions
    {
        const int MAX_ACTIONS = 100;

        Stack<GameAction> Actions;


        public GameActions()
        {
            this.Actions = new Stack<GameAction>();
        }


        public void PushEnd(int type, int time, float param1, float param2, float param3)
        {
            var action = new GameAction()
            {
                Type = type,
                Time = time,
                Param1 = param1,
                Param2 = param2,
                Param3 = param3
            };

            this.Actions.Push(action);
        }
    }
}
