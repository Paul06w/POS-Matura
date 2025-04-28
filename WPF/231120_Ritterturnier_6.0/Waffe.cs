using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _231120_Ritterturnier_6._0
{
    internal class Waffe
    {
        private string bezeichnung;
        private string art;

        public Waffe(string bezeichnung, string art)
        {
            this.bezeichnung = bezeichnung;
            this.art = art;
        }

        public string toString()
        {
            string s = "";

            s += "Weapon: " + art + " " + bezeichnung + "\n";

            return s;
        }

        public string getBezeichnung() { return bezeichnung; }
    }
}
