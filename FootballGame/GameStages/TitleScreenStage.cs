using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using FootballGame.Framework;


namespace FootballGame.GameStages
{
    public class TitleScreenStage : StageBase
    {
        public TitleScreenStage(MainGame game) : base(game)
        {

        }

        public override void Update(GameTime gameTime)
        {
            MainGame.Instance.GameInfo.SetupNewGame();
            MainGame.Instance.DownInfo.ScrimmagePos = 0;
            MainGame.Instance.DownInfo.FirstDownPos = Constants.YARD_DISTANCE * 10;
            MainGame.Instance.DownInfo.Down = 1;
            MainGame.Instance.DownInfo.YardsToGo = 10;

            MainGame.Instance.OpenSelectGameModeStage();
        }
    }
}
