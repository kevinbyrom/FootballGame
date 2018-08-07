using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballGame
{
    public static class Constants
    {
        public static readonly int DIR_NONE = 0;
        public static readonly int DIR_UP = 1;
        public static readonly int DIR_DOWN = 2;
        public static readonly int DIR_LEFT = 3;
        public static readonly int DIR_RIGHT = 4;
        public static readonly int DIR_UPLEFT = 5;
        public static readonly int DIR_UPRIGHT = 6;
        public static readonly int DIR_DOWNLEFT = 7;
        public static readonly int DIR_DOWNRIGHT = 8;

        public static readonly int NUM_SIDES = 2;

        public static readonly int OFFENSE = 0;
        public static readonly int DEFENSE = 1;

        public static readonly int HOME = 0;
        public static readonly int AWAY = 1;

        public static readonly int MAX_SIDE_PLAYERS = 11;
        public static readonly int MAX_PLAYS = 20;
        public static readonly int MAX_FORMATIONS = 20;

        public static readonly int MAX_TEAMS = 40;

        public static readonly int TEAM_49ERS = 0;
        public static readonly int TEAM_COWBOYS = 1;

        public static readonly int THROW_TYPE_LOB = 0;
        public static readonly int THROW_TYPE_NORMAL = 1;
        public static readonly int THROW_TYPE_BULLET = 2;

        public static readonly int THROW_SPEED_LOB = 60;
        public static readonly int THROW_SPEED_NORMAL = 30;
        public static readonly int THROW_SPEED_BULLET = 10;

        public static readonly int DIVE_SPEED = 1;

        public static readonly int CONTROL_NONE = -1;
        public static readonly int CONTROL_USER = 0;
        public static readonly int CONTROL_ACTIONS = 1;
        public static readonly int CONTROL_TRACKBALL = 2;
        public static readonly int CONTROL_TRACKDEST = 3;
        public static readonly int CONTROL_TRACKOBJECT = 4;
        public static readonly int CONTROL_BLOCK = 5;

        public static readonly int NUM_DEPTH_TYPES = 17;
        public static readonly int NUM_DEPTH_SLOTS = 6;

        public static readonly int DEPTH_QB = 0;
        public static readonly int DEPTH_WR = 1;
        public static readonly int DEPTH_TE = 2;
        public static readonly int DEPTH_HB = 3;
        public static readonly int DEPTH_FB = 4;
        public static readonly int DEPTH_OL = 5;
        public static readonly int DEPTH_C = 6;
        public static readonly int DEPTH_CB = 7;
        public static readonly int DEPTH_SS = 8;
        public static readonly int DEPTH_FS = 9;
        public static readonly int DEPTH_LB = 10;
        public static readonly int DEPTH_DL = 11;
        public static readonly int DEPTH_K = 12;
        public static readonly int DEPTH_P = 13;
        public static readonly int DEPTH_KR = 14;
        public static readonly int DEPTH_PR = 15;
        public static readonly int DEPTH_PH = 16;

        public static readonly int MAX_ROSTER_PLAYERS = 50;

        public static readonly int MAX_ROSTER_QB = 3;
        public static readonly int MAX_ROSTER_WR = 6;
        public static readonly int MAX_ROSTER_TE = 3;
        public static readonly int MAX_ROSTER_HB = 3;
        public static readonly int MAX_ROSTER_FB = 3;
        public static readonly int MAX_ROSTER_OL = 6;
        public static readonly int MAX_ROSTER_C = 3;
        public static readonly int MAX_ROSTER_CB = 6;
        public static readonly int MAX_ROSTER_SS = 3;
        public static readonly int MAX_ROSTER_FS = 3;
        public static readonly int MAX_ROSTER_LB = 5;
        public static readonly int MAX_ROSTER_DL = 7;
        public static readonly int MAX_ROSTER_K = 2;
        public static readonly int MAX_ROSTER_P = 2;
        public static readonly int MAX_ROSTER_KR = 2;
        public static readonly int MAX_ROSTER_PR = 2;
        public static readonly int MAX_ROSTER_PH = 2;

        public static readonly int INVALID_ROSTER_POS = 255;

        public static readonly int MSG_BALL_HIKED = 1;
        public static readonly int MSG_BALL_PASSED_SCRIMMAGE = 2;
        public static readonly int MSG_BALL_THROWN = 3;
        public static readonly int MSG_BALL_CAUGHT = 4;
        public static readonly int MSG_BALL_INRANGE = 5;
        public static readonly int MSG_BALL_FUMBLED = 6;
        public static readonly int MSG_BALL_HIT_GROUND = 7;
        public static readonly int MSG_PLAY_DEAD = 8;
        public static readonly int MSG_GRAPPLED = 9;
        public static readonly int MSG_GRAPPLE_BROKEN = 10;
        public static readonly int MSG_TACKLED = 11;
        public static readonly int MSG_KNOCKED_DOWN = 12;
        public static readonly int MSG_TAKEN_DOWN = 13;
        public static readonly int MSG_HANDOFF_GIVEN = 14;
        public static readonly int MSG_FAKE_HANDOFF_GIVEN = 15;
        public static readonly int MSG_START_POWER = 16;
        public static readonly int MSG_END_POWER = 17;
        public static readonly int MSG_START_AIM = 18;
        public static readonly int MSG_END_AIM = 19;


        // DOWN ENDING MESSAGES //

        public static readonly int MSG_BALL_HOLDER_OUT_OF_BOUNDS = 20;
        public static readonly int MSG_BALL_HOLDER_DOWN = 21;
        public static readonly int MSG_INCOMPLETE_PASS = 22;
        public static readonly int MSG_LOOSE_BALL_OUT_OF_BOUNDS = 23;
        public static readonly int MSG_TOUCHDOWN = 24;

        public static readonly float YARD_DISTANCE = 20.0f;
        public static readonly float NEUTRAL_ZONE_DISTANCE = 20.0f;
        public static readonly float ENDZONE_POS = 1020.0f;
        public static readonly float SIDELINE_POS = 200.0f;
        public static readonly float ENDZONE_SIDELINE_POS = 1200.0f;
        public static readonly float KICKOFF_POS = -220.0f;
        public static readonly float PAT_POS = 1000.0f;

        public static readonly int NUM_SPRTILES_X = 24;
        public static readonly int NUM_SPRTILES_Y = 9;

        public static readonly int MAX_GRAPPLERS = 5;

        public static readonly int CONTROL_PLAYER1 = 0;
        public static readonly int CONTROL_PLAYER2 = 1;
        public static readonly int CONTROL_COMPUTER = -1;

        public static readonly int ROUTE_DIR_HI = -1;
        public static readonly int ROUTE_DIR_MIDDLE = 0;
        public static readonly int ROUTE_DIR_LO = 1;

    }
}
