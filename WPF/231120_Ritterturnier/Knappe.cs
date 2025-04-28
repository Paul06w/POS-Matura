using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _231120_Ritterturnier
{
    internal class Knappe : Person
    {
        private double ausbildungsgrad = 10;


        public Knappe()
        {
            
        }


        public Knappe(string name, string telnum, double ausbildungsgrad)
            :base(name, telnum)
        {
            this.ausbildungsgrad = ausbildungsgrad;
        }

        public override string ToString()       //toSTring für Ausgabe
        {
            string s = "";

            s += "\n\nScarce: \n";
            s += "Name: " + name + "\n";
            s += "Phone Number: " + telnum + "\n";
            s += "Grade: " + ausbildungsgrad;

            return s;

            //return base.toString();
        }

        public double Ausbildungsgrad
        {
            get { return ausbildungsgrad; }
            set { ausbildungsgrad = value; }
        }

    }
}
