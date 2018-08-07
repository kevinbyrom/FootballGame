using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using FootballGame.Framework;
using FootballGame.Entities;

namespace FootballGame
{
    public class Side
    {
        const int MAX_PLAYERS = 11;

        public int TeamIndex;
        public int Direction;
        public Player[] Players;
        public Player Control;
        public Player TargetRec;
        public Player Passer;
        public Player Rusher;
        public Player Kicker;
        public Player Punter;
        public Player PlaceHolder;
        public Formation Formation;
        public Play Play;

        public Side()
        {
            this.TeamIndex = 0;
            this.Direction = 0;
            this.Players = new Player[MAX_PLAYERS];
        }


        public void Clear()
        {
            this.Control = null;
            this.TargetRec = null;
            this.Passer = null;
            this.Rusher = null;
            this.Kicker = null;
            this.Punter = null;
            this.PlaceHolder = null;
        }


        public void Update(GameTime gameTime)
        {
            foreach (var player in this.Players)
                player.Update(gameTime);
        }


        public void ChangeUserControl(Player player)
        {

            // Set the previous player's control type to NONE 

           /* if (Control != null)
            {
                Control.ControlType = CONTROL_NONE;
                Control.InputEnabled = false;
            }


            // Set the pointer to the new player 

            Control = player;


            // Set the player's control type to USER 

            if (Control != null)
            {
                Control->ControlType = CONTROL_USER;
                Control->InputEnabled = true;
            }*/
        }


        public void ChangeTargetReceiver()
        {
            /*int first = -1;
            int next = -1;
            bool playerfound = false;


            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                if (first == -1 && this.Players[i].Flag.Receiver)
                    first = i;

                if (next == -1 && playerfound && this.Players[i].Flag.Receiver)
                    next = i;

                if (this.TargetRec == this.Player[i])
                    playerfound = true;
            }

            if (next != -1)
                this.TargetRec = this.Players[next];
            else if (first != -1)
                this.TargetRec = this.Players[first];
            else
                this.TargetRec = null;*/
        }
    }
}
