using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _240131_Labordiagnostik
{
    class Messwert
    {
        public int wellIndex {  get; set; }
        public string wellName { get; set; }
        public double? absorbanceValue { get; set; }
        public int? fluorescenceValue { get; set; }

        public int reihenindex {  get; set; }
        public int spaltenindex { get; set; }

        public Messwert()
        {
            
        }

        public void CalculateIndexes()
        {
            string zeile = wellName.Substring(0, 1); //Zeile wird von Spalte getrennt
            int spaltenindex = Convert.ToInt32(wellName.Substring(1, wellName.Length - 1)) - 1;

            switch (zeile)      //reihenindex wird aufgrund des Buchstabens zugewiesen
            {
                case "A": reihenindex = 0; break;
                case "B": reihenindex = 1; break;
                case "C": reihenindex = 2; break;
                default: break;
            }
        }

        public string WellName
        {
            get { return wellName; }
            set { wellName = value; }
        }

        public double? AbsorbanceValue
        {
            get { return absorbanceValue; }
            set { absorbanceValue = value; }
        }

        public int? FluorescenceValue
        {
            get { return fluorescenceValue; }
            set { fluorescenceValue = value; }
        }
    }
}
