using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using FootballGame.Framework;


namespace FootballGame.GameStages
{
    public class SelectTeamStage : StageBase
    {
        public SelectTeamStage(MainGame game) : base(game)
        {

        }

        public override void Opening()
        {
            //LoadTeams();
            
            MainGame.Instance.Teams[Constants.HOME].Load(MainGame.Instance.Teams[Constants.HOME].Filename, true);
            MainGame.Instance.Teams[Constants.AWAY].Load(MainGame.Instance.Teams[Constants.AWAY].Filename, false);

            MainGame.Instance.GameInfo.SetupNewGame();
            MainGame.Instance.OpenSelectInputStage();
        }


        public override void Update(GameTime gameTime)
        {

        }


        public void LoadTeams()
        {

        }
    }
}
