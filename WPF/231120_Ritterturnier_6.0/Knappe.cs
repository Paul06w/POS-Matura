using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _231120_Ritterturnier_6._0
{
    internal class Knappe : Person
    {
        private double ausbildungsgrad = 10;



        public Knappe(string name, string telnum, double ausbildungsgrad)
            : base(name, telnum)
        {
            this.ausbildungsgrad = ausbildungsgrad;
        }

        public override string toString()
        {
            string s = "";

            s += "\n\nScarce: \n";
            s += "Name: " + name + "\n";
            s += "Phone Number: " + telnum + "\n";
            s += "Grade: " + ausbildungsgrad;

            return s;

            //return base.toString();
        }

    }
}