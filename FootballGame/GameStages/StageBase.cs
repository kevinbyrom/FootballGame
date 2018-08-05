using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using FootballGame.Framework;


namespace FootballGame.GameStages
{
    public abstract class StageBase : IStage
    {
        public MainGame Game;

        public StageBase(MainGame game)
        {
            this.Game = game;
        }

        public virtual void Opening()
        {

        }

        public virtual void Closing(IStage nextStage)
        {

        }

        public abstract void Update(GameTime gameTime);
    }
}
