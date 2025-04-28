using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _231120_Ritterturnier_6._0
{
    class NameSchonVorhandenException : Exception
    {

        public NameSchonVorhandenException()
        {

        }


        public override string Message
        {
            get
            {
                return "Teilnehmer schon vorhanden";
            }
        }
    }
}
