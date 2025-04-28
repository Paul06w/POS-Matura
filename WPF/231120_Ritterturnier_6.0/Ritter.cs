using _231120_Ritterturnier_6._0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _231120_Ritterturnier_6._0
{
    internal class Ritter : Person
    {
        private string rufname;
        private int ID;
        private static int nummer = 1;
        Knappe k = null;
        Waffe w = null;


        public Ritter(string name, string rufname, string telnum, int knightweapon, string knightprefix, string scarcename, string telnumscarce, double scarcegrade)
            : base(name, telnum)
        {
            this.rufname = rufname;

            ID = nummer;
            nummer++;

            addWaffe(knightweapon, knightprefix);

            if (scarcename != null && telnumscarce != null && scarcegrade != 0)
            {
                addKnappe(scarcename, telnumscarce, scarcegrade);
            }

        }

        public void addKnappe(string name, string telnum, double grade)
        {
            k = new Knappe(name, telnum, grade);
        }

        public void addWaffe(int knightweapon, string knightprefix)
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


        public override string toString()
        {
            string s = "";

            s += "Name: " + name + " " + rufname + "\n";
            s += "Phone Number: " + telnum + "\n";
            s += w.toString();
            //s += "Base Damage: " + "\n";
            //s += "Base Speed: " + "\n";
            s += "ID: " + ID;

            if (k != null)
            {
                s += k.toString();
            }

            return s;

            //return base.toString();
        }

        public string getWaffe()
        {
            return w.getBezeichnung();
        }
    }
}
