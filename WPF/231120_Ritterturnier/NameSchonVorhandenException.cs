using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _231120_Ritterturnier
{
    class NameSchonVorhandenException : Exception
    {

        public NameSchonVorhandenException()
        {
            
        }


        public override string Message              //Message der Exception, auf die zugegriffen werden kann
        {
            get
            {
                return "Teilnehmer schon vorhanden";
            }
        }
    }
}
