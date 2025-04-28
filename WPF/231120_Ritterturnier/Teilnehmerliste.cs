using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _231120_Ritterturnier
{
    internal class Teilnehmerliste
    {
        List<Ritter> turnierList = new List<Ritter>();

        public Teilnehmerliste()
        {
            
        }

        //Hinzufügen von einem Ritter
        public void AddTeilnehmer(string knightName, string knightrufname, string telnumKnight, int knightweapon, string knightprefix, string scarceName, string telnumScarce, double scarcegrade)
        {
            Ritter r = new Ritter(knightName, knightrufname, telnumKnight, knightweapon, knightprefix, scarceName, telnumScarce, scarcegrade);

            if (CheckName(r) != false)
            {
                turnierList.Add(r);
            }
        }

        //Funktion zum überprüfen eines schon vorhandenen Namens
        public bool CheckName(Ritter r)
        {
            try
            {
                for (int i = 0; i < turnierList.Count; i++)
                {
                    if (turnierList[i].GetName() == r.GetName())
                    {
                        throw new NameSchonVorhandenException();        //Exception wird geworfen wenn schon vorhanden
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

        //Alle Teilnehmer werden ausgegeben
        public string ListeAlleTeilnehmer()
        {
            string s = "";

            foreach (Ritter p in turnierList)
            {
                s += p.ToString() + "\n\n\n\n\n\n";
            }

            return s;
        }

        //Teilnehmer mit übergebener Waffe werden ausgegeben
        public string AlleMitWaffenart(string waffenart)
        {
            string s = "";

            foreach(Ritter p in turnierList)
            {
                if (p.GetWaffeBezeichnung() == waffenart)       //wenn Waffe ist richtig --> Ausgabe
                {
                    s += p.ToString() + "\n\n\n\n\n\n";
                }
            }

            return s;
        }


        public List<Ritter> GetTurnierList()
        {
            return turnierList;
        }


        public List<Ritter> TurnierList
        {
            get { return turnierList; }
            set { turnierList = value; }
        }

    }

    
}
