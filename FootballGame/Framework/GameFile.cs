using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballGame.Framework
{
    public class GameFile
    {
        private int pos;
        private string[] entries;


        public GameFile(string filename)
        {
            this.pos = 0;
            this.entries = File.ReadAllLines(filename)
                .Where(s => !String.IsNullOrEmpty(s))
                .Where(s => !s.StartsWith("/"))
                .ToArray();
        }


        public string ReadLine()
        {
            return this.entries[pos++];
        }

        public string[] ReadParams()
        {
            var line = this.ReadLine();

            return line.Split(new char[] { ',' }).Select(s => s.Trim()).ToArray();
        }
    }
}
