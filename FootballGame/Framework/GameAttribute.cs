using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballGame.Framework
{
    public class GameAttribute
    {
        public float Mod;
        public float Val;
        public int Exp;
        public int NextLvl;
        public int TotalExp;


        public float CurrVal
        {
            get
            {
                return this.Val + this.Mod;
            }
        }

        public int Bonus()
        {
            int value = (int)this.CurrVal;

            if (value < 10) return -5;
            if (value < 20) return -4;
            if (value < 30) return -3;
            if (value < 40) return -2;
            if (value < 50) return -1;
            if (value < 60) return 0;
            if (value < 70) return 1;
            if (value < 80) return 2;
            if (value < 90) return 3;

            return 4;
        }


        public void Load(GameFile file)
        {

            // Read next set of params from the file

            // Set the attribute values

            var p = file.ReadParams();

            this.Mod = 0;
            this.Val = float.Parse(p[0]);	
	        this.Exp = int.Parse(p[0]);	
	        this.NextLvl = int.Parse(p[0]);	
	        this.TotalExp = int.Parse(p[0]);
        }
    }
}
