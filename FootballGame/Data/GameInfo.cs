using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballGame
{
    public struct Clock
    {
        public int Minutes;
        public int Seconds;
    }


    public class GameInfo
    {
        const int PLAY_TYPE_NORMAL = 0;
        const int PLAY_TYPE_KICKOFF = 1;
        const int PLAY_TYPE_PAT = 2;

        const int SHOW_NONE = 0;
        const int SHOW_FIRSTDOWN = 1;
        const int SHOW_TURNOVER = 2;
        const int SHOW_TOUCHDOWN = 3;
        const int SHOW_SACK = 4;
        const int SHOW_PATGOOD = 5;
        const int SHOW_PATNOGOOD = 6;
        const int SHOW_FIELDGOALGOOD = 7;
        const int SHOW_FIELDGOALNOGOOD = 8;

        const int DOWN_DONE_OUTOFBOUNDS = 0;
        const int DOWN_DONE_INCOMPLETEPASS = 1;
        const int DOWN_DONE_TACKLED = 2;
        const int DOWN_DONE_SACKED = 3;
        const int DOWN_DONE_TOUCHDOWN = 4;
        const int DOWN_DONE_PATGOOD = 5;
        const int DOWN_DONE_PATNOGOOD = 6;
        const int DOWN_DONE_FIELDGOALGOOD = 7;
        const int DOWN_DONE_FIELDGOALNOGOOD = 8;

        const int DOWN_MAX_AIM_RANGE = 40;

        public int[] Score;
        public int[] Direction;
        public int Possession;             // 0 = HOME		1 = AWAY
        public int PlayType;
        public Clock Clock;
        public bool ClockCounting;
        public int Quarter;


        public GameInfo()
        {
            this.Score = new int[Constants.NUM_SIDES];
            this.Direction = new int[Constants.NUM_SIDES];
        }


        public void SetupNewGame()
        {
            // Default the scores to zero

            this.Score[Constants.HOME] = 0;
            this.Score[Constants.AWAY] = 0;


            // Set the default directions 

            this.Direction[Constants.HOME] = Constants.DIR_RIGHT;
            this.Direction[Constants.AWAY] = Constants.DIR_LEFT;


            // Set the default possession

            this.Possession = Constants.HOME;


            // Set the quarter and clock time

            this.Quarter = 1;
            this.Clock.Minutes = 5;
            this.Clock.Seconds = 0;
            this.ClockCounting = false;
        }

        public int PlayDone()
        {
            bool ShowTurnover = false;
            bool ShowFirstdown = false;
            bool ShowTouchdown = false;
            bool ShowSack = false;
            bool ShowPATGood = false;
            bool ShowPATNoGood = false;
            bool ShowFieldGoalGood = false;
            bool ShowFieldGoalNoGood = false;


            MainGame.Instance.DownInfo.ScrimmagePos = MainGame.Instance.DownInfo.BallSpotPos;


            if (CheckForFirstDown())
            {
                FirstDown();
                ShowFirstdown = false;
            }
            else
            {
                IncrementDown();
                SetYardsToGo();
            }


            // Change possession on turnovers 

            if (MainGame.Instance.DownInfo.Turnover)
            {
                ChangePossession();
                ShowTurnover = true;
            }


            switch (MainGame.Instance.DownInfo.DoneReason)
            {
                case DOWN_DONE_OUTOFBOUNDS:
                    break;
                    
                case DOWN_DONE_INCOMPLETEPASS:
                    break;
                    
                case DOWN_DONE_TACKLED:
                    break;
                    
                case DOWN_DONE_SACKED:
                    ShowSack = true;
                    break;
                    
                case DOWN_DONE_TOUCHDOWN:
                    Touchdown();
                    ShowTouchdown = true;
                    break;
                    
                case DOWN_DONE_PATGOOD:
                    PAT1Good();
                    ShowPATGood = true;
                    break;
                    
                case DOWN_DONE_PATNOGOOD:
                    PATNoGood();
                    ShowPATNoGood = true;
                    break;
                    
                case DOWN_DONE_FIELDGOALGOOD:
                    FieldGoalGood();
                    ShowFieldGoalGood = true;
                    break;
                    
                case DOWN_DONE_FIELDGOALNOGOOD:
                    FieldGoalNoGood();
                    ShowFieldGoalGood = true;
                    break;
            }


            if (MainGame.Instance.DownInfo.Down > 4)
            {
                ChangePossession();
                ShowTurnover = true;
            }


            if (ShowTouchdown) return SHOW_TOUCHDOWN;
            if (ShowFirstdown) return SHOW_FIRSTDOWN;
            if (ShowTurnover) return SHOW_TURNOVER;
            if (ShowSack) return SHOW_SACK;
            if (ShowPATGood) return SHOW_PATGOOD;
            if (ShowPATNoGood) return SHOW_PATNOGOOD;
            if (ShowFieldGoalGood) return SHOW_FIELDGOALGOOD;
            if (ShowFieldGoalNoGood) return SHOW_FIELDGOALNOGOOD;

            return SHOW_NONE;
        }

        public bool CheckForFirstDown()
        {
            if (this.Direction[Possession] == Constants.DIR_LEFT)
            {
                if (MainGame.Instance.DownInfo.BallSpotPos <= MainGame.Instance.DownInfo.FirstDownPos)
                    return true;
            }
            else
            {
                if (MainGame.Instance.DownInfo.BallSpotPos >= MainGame.Instance.DownInfo.FirstDownPos)
                    return true;
            }


            return false;
        }


        public void FirstDown()
        {
            MainGame.Instance.DownInfo.Down = 1;
            MainGame.Instance.DownInfo.YardsToGo = 10;
            MainGame.Instance.DownInfo.FirstDownPos = this.Direction[Possession] == Constants.DIR_LEFT ? MainGame.Instance.DownInfo.ScrimmagePos - (Constants.YARD_DISTANCE * 10) : MainGame.Instance.DownInfo.ScrimmagePos + (Constants.YARD_DISTANCE * 10);
        }


        public void IncrementDown()
        {
            MainGame.Instance.DownInfo.Down++;
        }


        public void SetYardsToGo()
        {
            MainGame.Instance.DownInfo.YardsToGo = (int)(Math.Abs(MainGame.Instance.DownInfo.FirstDownPos - MainGame.Instance.DownInfo.ScrimmagePos) / Constants.YARD_DISTANCE);
        }


        public void ChangePossession()
        {
            Possession = Possession == 1 ? 0 : 1;

            MainGame.Instance.DownInfo.ScrimmagePos = MainGame.Instance.DownInfo.BallSpotPos;
            MainGame.Instance.DownInfo.BallSpotPos = MainGame.Instance.DownInfo.ScrimmagePos;

            FirstDown();
        }


        public void Touchdown()
        {
            // Increment the score for the offensive team

            Score[Possession] += 6;


            // Set the new scrimmage position for the extra point 

            MainGame.Instance.DownInfo.ScrimmagePos = Direction[Possession] == Constants.DIR_LEFT ? -Constants.PAT_POS : Constants.PAT_POS;
            MainGame.Instance.DownInfo.FirstDownPos = MainGame.Instance.DownInfo.ScrimmagePos;
            MainGame.Instance.DownInfo.Down = -1;
            MainGame.Instance.DownInfo.YardsToGo = -1;


            // Set the next play type

            PlayType = PLAY_TYPE_PAT;
        }


        public void PAT1Good()
        {
            // Increment the score by 1

            Score[Possession] += 1;


            // Set the new scrimmage position for the kickoff

            MainGame.Instance.DownInfo.ScrimmagePos = Direction[Possession] == Constants.DIR_LEFT ? -Constants.KICKOFF_POS : Constants.KICKOFF_POS;
            MainGame.Instance.DownInfo.FirstDownPos = MainGame.Instance.DownInfo.ScrimmagePos;
            MainGame.Instance.DownInfo.Down = -1;
            MainGame.Instance.DownInfo.YardsToGo = -1;


            // Set the next play type

            PlayType = PLAY_TYPE_KICKOFF;
        }


        public void PAT2Good()
        {
            // Increment the score by 2

            Score[Possession] += 2;


            // Set the new scrimmage position for the kickoff

            MainGame.Instance.DownInfo.ScrimmagePos = Direction[Possession] == Constants.DIR_LEFT ? -Constants.KICKOFF_POS : Constants.KICKOFF_POS;
            MainGame.Instance.DownInfo.FirstDownPos = MainGame.Instance.DownInfo.ScrimmagePos;
            MainGame.Instance.DownInfo.Down = -1;
            MainGame.Instance.DownInfo.YardsToGo = -1;


            // Set the next play type

            PlayType = PLAY_TYPE_KICKOFF;
        }


        public void PATNoGood()
        {
            // Set the new scrimmage position for the kickoff

            MainGame.Instance.DownInfo.ScrimmagePos = Direction[Possession] == Constants.DIR_LEFT ? -Constants.KICKOFF_POS : Constants.KICKOFF_POS;
            MainGame.Instance.DownInfo.FirstDownPos = MainGame.Instance.DownInfo.ScrimmagePos;
            MainGame.Instance.DownInfo.Down = -1;
            MainGame.Instance.DownInfo.YardsToGo = -1;


            // Set the next play type

            PlayType = PLAY_TYPE_KICKOFF;
        }


        public void FieldGoalGood()
        {
            // Increment the score by 3

            Score[Possession] += 3;


            // Set the new scrimmage position for the kickoff

            MainGame.Instance.DownInfo.ScrimmagePos = Direction[Possession] == Constants.DIR_LEFT ? -Constants.KICKOFF_POS : Constants.KICKOFF_POS;
            MainGame.Instance.DownInfo.FirstDownPos = MainGame.Instance.DownInfo.ScrimmagePos;
            MainGame.Instance.DownInfo.Down = -1;
            MainGame.Instance.DownInfo.YardsToGo = -1;


            // Set the next play type

            PlayType = PLAY_TYPE_KICKOFF;
        }


        public void FieldGoalNoGood()
        {
            // Change the possession

            ChangePossession();


            // Set the next play type

            PlayType = PLAY_TYPE_NORMAL;
        }
    }
}
