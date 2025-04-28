using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _231120_Ritterturnier_6._0
{
    internal class Teilnehmerliste
    {
        List<Person> turnierList = new List<Person>();

        public Teilnehmerliste()
        {

        }

        public void addTeilnehmer(string knightName, string knightrufname, string telnumKnight, int knightweapon, string knightprefix, string scarceName, string telnumScarce, double scarcegrade)
        {
            Ritter r = new Ritter(knightName, knightrufname, telnumKnight, knightweapon, knightprefix, scarceName, telnumScarce, scarcegrade);

            if (checkName(r) != false)
            {
                turnierList.Add(r);
            }
        }

        public bool checkName(Ritter r)
        {
            try
            {
                for (int i = 0; i < turnierList.Count; i++)
                {
                    if (turnierList[i].getName() == r.getName())
                    {
                        throw new NameSchonVorhandenException();
                    }

                }
                return true;
            }
            catch (NameSchonVorhandenException e)
            {
                MessageBox.Show(e.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }
        }

        public string listeAlleTeilnehmer()
        {
            string s = "";

            foreach (Person p in turnierList)
            {
                s += p.toString() + "\n\n\n\n\n\n";
            }

            return s;
        }

        public string alleMitWaffenart(string waffenart)
        {
            string s = "";

            foreach (Person p in turnierList)
            {
                if (p is Ritter)
                {
                    Ritter r = (Ritter)p;

                    if (r.getWaffe() == waffenart)
                    {
                        s += p.toString() + "\n\n\n\n\n\n";
                    }
                }
            }

            return s;
        }

        public List<Person> getTurnierList()
        {
            return turnierList;
        }

    }


}
