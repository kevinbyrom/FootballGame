using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballGame.Framework;


namespace FootballGame
{
    public class PlayerAttributes
    {
        public GameAttribute Stamina = new GameAttribute();
        public GameAttribute Aware = new GameAttribute();
        public GameAttribute Strength = new GameAttribute();
        public GameAttribute ThrowPow = new GameAttribute();
        public GameAttribute ThrowAcc = new GameAttribute();
        public GameAttribute Catch = new GameAttribute();
        public GameAttribute Accel = new GameAttribute();
        public GameAttribute Speed = new GameAttribute();
        public GameAttribute BreakTak = new GameAttribute();
        public GameAttribute Tackle = new GameAttribute();
        public GameAttribute Block = new GameAttribute();
        public GameAttribute BreakBlock = new GameAttribute();
        public GameAttribute KickPow = new GameAttribute();
        public GameAttribute KickAcc = new GameAttribute();
    }

    public class PlayerStats
    {
    }

    public class PlayerInfo
    {
        public string FirstName;
        public string LastName;
        public int Age;
        public int Position;
        public int Number;
        public int SkinColor;
        public PlayerAttributes Attrib;
        public bool InPlay;
        public bool Injured;
        public int InjWeeks;
        public int InjType;
        public string FacePic;

        public PlayerInfo()
        {
            this.Attrib = new PlayerAttributes();
        }

        public void Load(string name)
        {
            var file = new GameFile($"Players\\{name}.txt");

            this.FirstName = file.ReadLine();
            this.LastName = file.ReadLine();
            this.Age = int.Parse(file.ReadLine());
            this.Position = int.Parse(file.ReadLine());
            this.Number = int.Parse(file.ReadLine());
            this.SkinColor = int.Parse(file.ReadLine());

            this.Attrib.Aware.Load(file);
            this.Attrib.Strength.Load(file);
            this.Attrib.ThrowPow.Load(file);
            this.Attrib.ThrowAcc.Load(file);
            this.Attrib.Catch.Load(file);
            this.Attrib.Accel.Load(file);
            this.Attrib.Speed.Load(file);
            this.Attrib.BreakTak.Load(file);
            this.Attrib.Tackle.Load(file);
            this.Attrib.Block.Load(file);
            this.Attrib.BreakBlock.Load(file);
            this.Attrib.KickPow.Load(file);
            this.Attrib.KickAcc.Load(file);
        }
    }
}
