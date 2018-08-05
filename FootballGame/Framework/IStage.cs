using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace FootballGame.Framework
{
    public interface IStage
    {
        void Opening();
        void Closing(IStage nextStage);
        void Update(GameTime gameTime);
    }
}
