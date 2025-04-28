using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _231120_Ritterturnier
{
    internal class Ritter : Person
    {
        private string rufname;
        private int ID;
        private static int nummer = 0;
        Knappe k = null;
        Waffe w = null;


        public Ritter()
        {
            
        }

        public Ritter(string name, string rufname, string telnum, int knightweapon, string knightprefix, string scarcename, string telnumscarce, double scarcegrade)
            :base(name, telnum)
        {
            this.rufname = rufname;

            nummer++;
            ID = nummer;
            

            AddWaffe(knightweapon, knightprefix);
            
            if(scarcename != null && telnumscarce != null && scarcegrade != 0)
            {
                AddKnappe(scarcename, telnumscarce, scarcegrade);
            }
            
        }

        public void AddKnappe(string name, string telnum, double grade)
        {
            k = new Knappe(name, telnum, grade);
        }

        public void AddWaffe(int knightweapon, string knightprefix)
        {

            string beschreibung = "";

            if (knightweapon == 0)
            {
                beschreibung = "Schwert";
            }
            else if (knightweapon == 1)
            {
                beschreibung = "Lanze";
            }
            else if (knightweapon == 2)
            {
                beschreibung = "Keule";
            }

            w = new Waffe(beschreibung, knightprefix);

        }


        public override string ToString()       //ToString für Ausgabe
        {
            string s = "";

            s += "Name: " + name + " " + rufname + "\n";
            s += "Phone Number: " + telnum + "\n";
            s += w.ToString();
            s += "ID: " + ID;

            if (k != null)
            {
                s += k.ToString();
            }

            return s;

            //return base.toString();
        }

        public string GetWaffeBezeichnung()         //Funktion für Ausgabe nach Waffenart
        {
            return w.Bezeichnung;
        }

        //Getter und Setter
        public string Rufname
        {
            get { return rufname; }
            set { rufname = value; }
        }

        public int Id
        {
            get { return ID; }
            set { ID = value; }
        }

        public int Nummer
        {
            get { return nummer; }
            set { nummer = value; }
        }

        public Knappe K
        {
            get { return k; }
            set { k = value; }
        }

        public Waffe W
        {
            get { return w; }
            set { w = value; }
        }
    }
}
