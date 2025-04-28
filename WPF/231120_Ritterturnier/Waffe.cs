using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _231120_Ritterturnier
{
    internal class Waffe
    {
        private string bezeichnung;
        private string art;

        public Waffe()
        {
            
        }

        public Waffe(string bezeichnung, string art)
        {
            this.bezeichnung = bezeichnung;
            this.art = art;
        }

        public string ToString()            //toString für Ausgabe
        {
            string s = "";

            s += "Weapon: " + art + " " + bezeichnung + "\n";

            return s;
        }

        public string GetBezeichnung() {  return bezeichnung; }

        public string Bezeichnung
        {
            get { return bezeichnung; }
            set { bezeichnung = value; }
        }

        public string Art
        {
            get { return art; }
            set { art = value; }
        }
    }
}
