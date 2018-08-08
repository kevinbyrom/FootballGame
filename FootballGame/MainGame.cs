using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FootballGame.Framework;
using FootballGame.Entities;
using FootballGame.GameStages;


namespace FootballGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        static public MainGame Instance;

        const int NUM_SIDES = 2;
        const int HOME = 0;
        const int AWAY = 1;
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public GameInfo GameInfo;
        public DownInfo DownInfo;
        public Team[] Teams;
        public Side[] Sides;
        public Play[] CurrentPlay;
        public Playbook[] Playbook;
        public Camera Camera;
        public Ball Ball;
        public DisplayList ObjectDispList;
        public DisplayList ShadowDispList;
        public VPad[] VPads;
        public MessageManager MsgMgr;

        public Texture2D FieldTexture;
        public Texture2D BallTexture;
        public Texture2D IconTexture;
        public Texture2D[] PlayerTexture;
        public Texture2D FontTexture;

        IStage CurrStage;
        BeginGameStage BeginGameStage;
        CheckTimeStage CheckTimeStage;
        CoinTossStage CoinTossStage;
        EndGameStage EndGameStage;
        FirstDownStage FirstDownStage;
        HalfTimeStage HalfTimeStage;
        PlayStage PlayStage;
        SelectGameModeStage SelectGameModeStage;
        SelectInputStage SelectInputStage;
        SelectPlayStage SelectPlayStage;
        SelectTeamStage SelectTeamStage;
        TitleScreenStage TitleScreenStage;
        TouchdownStage TouchdownStage;
        TurnOverStage TurnOverStage;


        //  -ENDZONE- / -0 TO 10- / -10 TO 20- / -20 TO 30- / -30 TO 40- / -40 TO 50- / -50 TO 40- / -40 TO 30- / -30 TO 20- / -20 TO 10- / -10 TO 0- / -ENDZONE-

        public int[] FieldTiles = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
                                    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
                                    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
                                    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,   5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
                                    4, 4, 4, 4, 4, 4, 4, 4, 4, 4,   4, 4, 4, 4, 4, 4, 4, 4, 4, 4,   4, 4, 4, 4, 4, 4, 4, 4, 4, 4,   4, 4, 4, 4, 4, 4, 4, 4, 4, 4,   4, 4, 4, 4, 4, 4, 4, 4, 4, 4,   4, 4, 4, 4, 4, 4, 4, 4, 4, 4,    4, 4, 4, 4, 4, 4, 4, 4, 4, 4,    4, 4, 4, 4, 4, 4, 4, 4, 4, 4,    4, 4, 4, 4, 4, 4, 4, 4, 4, 4,    4, 4, 4, 4, 4, 4, 4, 4, 4, 4,    4, 4, 4, 4, 4, 4, 4, 4, 4, 4,   4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
                                    3, 3, 3, 3, 3, 3, 3, 3, 3, 3,   3, 3, 3, 3, 3, 3, 3, 3, 3, 3,   3, 3, 3, 3, 3, 3, 3, 3, 3, 3,   3, 3, 3, 3, 3, 3, 3, 3, 3, 3,   3, 3, 3, 3, 3, 3, 3, 3, 3, 3,   3, 3, 3, 3, 3, 3, 3, 3, 3, 3,    3, 3, 3, 3, 3, 3, 3, 3, 3, 3,    3, 3, 3, 3, 3, 3, 3, 3, 3, 3,    3, 3, 3, 3, 3, 3, 3, 3, 3, 3,    3, 3, 3, 3, 3, 3, 3, 3, 3, 3,    3, 3, 3, 3, 3, 3, 3, 3, 3, 3,   3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
                                    3, 3, 3, 3, 3, 3, 3, 3, 3, 3,   3, 3, 3, 3, 3, 3, 3, 3, 3, 3,   3, 3, 3, 3, 3, 3, 3, 3, 3, 3,   3, 3, 3, 3, 3, 3, 3, 3, 3, 3,   3, 3, 3, 3, 3, 3, 3, 3, 3, 3,   3, 3, 3, 3, 3, 3, 3, 3, 3, 3,    3, 3, 3, 3, 3, 3, 3, 3, 3, 3,    3, 3, 3, 3, 3, 3, 3, 3, 3, 3,    3, 3, 3, 3, 3, 3, 3, 3, 3, 3,    3, 3, 3, 3, 3, 3, 3, 3, 3, 3,    3, 3, 3, 3, 3, 3, 3, 3, 3, 3,   3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
                                    3, 3, 3, 3, 3, 3, 3, 3, 3, 3,   3, 3, 3, 3, 3, 3, 3, 3, 3, 3,   3, 3, 3, 3, 3, 3, 3, 3, 3, 3,   3, 3, 3, 3, 3, 3, 3, 3, 3, 3,   3, 3, 3, 3, 3, 3, 3, 3, 3, 3,   3, 3, 3, 3, 3, 3, 3, 3, 3, 3,    3, 3, 3, 3, 3, 3, 3, 3, 3, 3,    3, 3, 3, 3, 3, 3, 3, 3, 3, 3,    3, 3, 3, 3, 3, 3, 3, 3, 3, 3,    3, 3, 3, 3, 3, 3, 3, 3, 3, 3,    3, 3, 3, 3, 3, 3, 3, 3, 3, 3,   3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
                                    2, 2, 2, 2, 2, 2, 2, 2, 2, 2,   2, 2, 2, 2, 2, 2, 2, 2, 2, 2,   2, 2, 2, 2, 2, 2, 2, 2, 2, 2,   2, 2, 2, 2, 2, 2, 2, 2, 2, 2,   2, 2, 2, 2, 2, 2, 2, 2, 2, 2,   2, 2, 2, 2, 2, 2, 2, 2, 2, 2,    2, 2, 2, 2, 2, 2, 2, 2, 2, 2,    2, 2, 2, 2, 2, 2, 2, 2, 2, 2,    2, 2, 2, 2, 2, 2, 2, 2, 2, 2,    2, 2, 2, 2, 2, 2, 2, 2, 2, 2,    2, 2, 2, 2, 2, 2, 2, 2, 2, 2,   2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
                                    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,   1, 1, 1, 1, 1, 1, 1, 1, 1, 1,   1, 1, 1, 1, 1, 1, 1, 1, 1, 1,   1, 1, 1, 1, 1, 1, 1, 1, 1, 1,   1, 1, 1, 1, 1, 1, 1, 1, 1, 1,   1, 1, 1, 1, 1, 1, 1, 1, 1, 1,    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,   1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                                    7, 9, 9, 9, 9, 9, 9, 9, 9, 8,  13, 0, 0, 0,12,13, 0, 0, 0,19,  24, 0, 0, 0,12,13, 0, 0, 0,20,  24, 0, 0, 0,12,13, 0, 0, 0,21,  24, 0, 0, 0,12,13, 0, 0, 0,22,  24, 0, 0, 0,12,13, 0, 0, 0,23,   24, 0, 0, 0,12,13, 0, 0, 0,22,   24, 0, 0, 0,12,13, 0, 0, 0,21,   24, 0, 0, 0,12,13, 0, 0, 0,20,   24, 0, 0, 0,12,13, 0, 0, 0,19,   24, 0, 0, 0,12,13, 0, 0, 0,12,   7, 9, 9, 9, 9, 9, 9, 9, 9, 8,
                                    7, 9, 9, 9, 9, 9, 9, 9, 9, 8,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   7, 9, 9, 9, 9, 9, 9, 9, 9, 8,
                                    7, 9, 9, 9, 9, 9, 9, 9, 9, 8,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   7, 9, 9, 9, 9, 9, 9, 9, 9, 8,
                                    7, 9, 9, 9, 9, 9, 9, 9, 9, 8,  14,15,15,15,16,14,15,15,15,16,  14,15,15,15,16,14,15,15,15,16,  14,15,15,15,16,14,15,15,15,16,  14,15,15,15,16,14,15,15,15,16,  14,15,15,15,16,14,15,15,15,16,   14,15,15,15,16,14,15,15,15,16,   14,15,15,15,16,14,15,15,15,16,   14,15,15,15,16,14,15,15,15,16,   14,15,15,15,16,14,15,15,15,16,   14,15,15,15,16,14,15,15,15,16,   7, 9, 9, 9, 9, 9, 9, 9, 9, 8,
                                    7, 9, 9, 9, 9, 9, 9, 9, 9, 8,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   7, 9, 9, 9, 9, 9, 9, 9, 9, 8,
                                    7, 9, 9, 9, 9, 9, 9, 9, 9, 8,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   7, 9, 9, 9, 9, 9, 9, 9, 9, 8,
                                    7, 9, 9, 9, 9, 9, 9, 9, 9, 8,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   7, 9, 9, 9, 9, 9, 9, 9, 9, 8,
                                    7, 9, 9, 9, 9, 9, 9, 9, 9, 8,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   7, 9, 9, 9, 9, 9, 9, 9, 9, 8,
                                    7, 9, 9, 9, 9, 9, 9, 9, 9, 8,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   7, 9, 9, 9, 9, 9, 9, 9, 9, 8,
                                    7, 9, 9, 9, 9, 9, 9, 9, 9, 8,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   7, 9, 9, 9, 9, 9, 9, 9, 9, 8,
                                    7, 9, 9, 9, 9, 9, 9, 9, 9, 8,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   7, 9, 9, 9, 9, 9, 9, 9, 9, 8,
                                    7, 9, 9, 9, 9, 9, 9, 9, 9, 8,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   7, 9, 9, 9, 9, 9, 9, 9, 9, 8,
                                    7, 9, 9, 9, 9, 9, 9, 9, 9, 8,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   7, 9, 9, 9, 9, 9, 9, 9, 9, 8,
                                    7, 9, 9, 9, 9, 9, 9, 9, 9, 8,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   7, 9, 9, 9, 9, 9, 9, 9, 9, 8,
                                    7, 9, 9, 9, 9, 9, 9, 9, 9, 8,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   7, 9, 9, 9, 9, 9, 9, 9, 9, 8,
                                    7, 9, 9, 9, 9, 9, 9, 9, 9, 8,  14,15,15,15,16,14,15,15,15,16,  14,15,15,15,16,14,15,15,15,16,  14,15,15,15,16,14,15,15,15,16,  14,15,15,15,16,14,15,15,15,16,  14,15,15,15,16,14,15,15,15,16,   14,15,15,15,16,14,15,15,15,16,   14,15,15,15,16,14,15,15,15,16,   14,15,15,15,16,14,15,15,15,16,   14,15,15,15,16,14,15,15,15,16,   14,15,15,15,16,14,15,15,15,16,   7, 9, 9, 9, 9, 9, 9, 9, 9, 8,
                                    7, 9, 9, 9, 9, 9, 9, 9, 9, 8,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   7, 9, 9, 9, 9, 9, 9, 9, 9, 8,
                                    7, 9, 9, 9, 9, 9, 9, 9, 9, 8,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,  13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   13, 0, 0, 0,12,13, 0, 0, 0,12,   7, 9, 9, 9, 9, 9, 9, 9, 9, 8,
                                    7, 9, 9, 9, 9, 9, 9, 9, 9, 8,  13, 0, 0, 0,12,13, 0, 0, 0,19,  24, 0, 0, 0,12,13, 0, 0, 0,20,  24, 0, 0, 0,12,13, 0, 0, 0,21,  24, 0, 0, 0,12,13, 0, 0, 0,22,  24, 0, 0, 0,12,13, 0, 0, 0,23,   24, 0, 0, 0,12,13, 0, 0, 0,22,   24, 0, 0, 0,12,13, 0, 0, 0,21,   24, 0, 0, 0,12,13, 0, 0, 0,20,   24, 0, 0, 0,12,13, 0, 0, 0,19,   24, 0, 0, 0,12,13, 0, 0, 0,12,   7, 9, 9, 9, 9, 9, 9, 9, 9, 8,
                                    6, 6, 6, 6, 6, 6, 6, 6, 6, 6,   6, 6, 6, 6, 6, 6, 6, 6, 6, 6,   6, 6, 6, 6, 6, 6, 6, 6, 6, 6,   6, 6, 6, 6, 6, 6, 6, 6, 6, 6,   6, 6, 6, 6, 6, 6, 6, 6, 6, 6,   6, 6, 6, 6, 6, 6, 6, 6, 6, 6,    6, 6, 6, 6, 6, 6, 6, 6, 6, 6,    6, 6, 6, 6, 6, 6, 6, 6, 6, 6,    6, 6, 6, 6, 6, 6, 6, 6, 6, 6,    6, 6, 6, 6, 6, 6, 6, 6, 6, 6,    6, 6, 6, 6, 6, 6, 6, 6, 6, 6,   6, 6, 6, 6, 6, 6, 6, 6, 6, 6};


        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.GameInfo = new GameInfo();
            this.DownInfo = new DownInfo();
            this.Teams = new Team[NUM_SIDES];
            this.Sides = new Side[NUM_SIDES];
            this.CurrentPlay = new Play[NUM_SIDES];
            this.Playbook = new Playbook[NUM_SIDES];
            this.ObjectDispList = new DisplayList();
            this.ShadowDispList = new DisplayList();
            this.VPads = new VPad[NUM_SIDES];
            this.MsgMgr = new MessageManager();

            this.BeginGameStage = new BeginGameStage(this);
            this.CheckTimeStage = new CheckTimeStage(this);
            this.CoinTossStage = new CoinTossStage(this);
            this.EndGameStage = new EndGameStage(this);
            this.FirstDownStage = new FirstDownStage(this);
            this.HalfTimeStage = new HalfTimeStage(this);
            this.PlayStage = new PlayStage(this);
            this.SelectGameModeStage = new SelectGameModeStage(this);
            this.SelectInputStage = new SelectInputStage(this);
            this.SelectPlayStage = new SelectPlayStage(this);
            this.SelectTeamStage = new SelectTeamStage(this);
            this.TitleScreenStage = new TitleScreenStage(this);
            this.TouchdownStage = new TouchdownStage(this);
            this.TurnOverStage = new TurnOverStage(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            Opening();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            this.BallTexture = Content.Load<Texture2D>("ball");
            this.IconTexture = Content.Load<Texture2D>("icons");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (this.CurrStage != null)
                this.CurrStage.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


        public void Opening()
        {
            // Setup vpads
            // Load all assets

            //this.FieldMap.Create(FieldTiles, 120, 30, 20, 20);
            //this.FieldMap.SetSprite(this.Teams[HOME].FieldSpr);


            // Set the stage to title screen

            OpenTitleScreenStage();
        }

        public void Closing()
        {

        }

        public void LoadTextures()
        {

        }


        public void LoadPlaybooks()
        {
        }


        public void SetStage(IStage stage)
        {
            if (this.CurrStage != null)
                this.CurrStage.Closing(stage);

            stage.Opening();

            this.CurrStage = stage;
        }

        public void OpenBeginGameStage() { SetStage(BeginGameStage); }
        public void OpenCoinTossStage() { SetStage(CoinTossStage); }
        public void OpenCheckTimeStage() { SetStage(CheckTimeStage); }
        public void OpenEndGameStage() { SetStage(EndGameStage); }
        public void OpenFirstDownStage() { SetStage(FirstDownStage); }
        public void OpenHalfTimeStage() { SetStage(HalfTimeStage); }
        public void OpenPlayStage() { SetStage(PlayStage); }
        public void OpenSelectGameModeStage() { SetStage(SelectGameModeStage); }
        public void OpenSelectInputStage() { SetStage(SelectInputStage); }
        public void OpenSelectPlayStage() { SetStage(SelectPlayStage); }
        public void OpenSelectTeamStage() { SetStage(SelectTeamStage); }
        public void OpenTitleScreenStage() { SetStage(TitleScreenStage); }
        public void OpenTouchdownStage() { SetStage(TouchdownStage); }
        public void OpenTurnOverStage() { SetStage(TurnOverStage); }
    }
}
