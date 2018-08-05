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
        private string filename;


        public GameFile(string filename)
        {
            this.filename = filename;
        }


        public string ReadLine()
        {
            throw new NotImplementedException();
        }

        public string[] ReadParams()
        {
            throw new NotImplementedException();
        }


        public string[] ReadAllEntries()
        {
            return File.ReadAllLines(this.filename)
                .Where(s => !String.IsNullOrEmpty(s))
                .Where(s => !s.StartsWith("/"))
                .ToArray();
        }
    }
}
