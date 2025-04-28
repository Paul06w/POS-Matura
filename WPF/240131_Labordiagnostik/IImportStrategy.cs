using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _240131_Labordiagnostik
{
    internal interface IImportStrategy
    {
        List<Messwert> ImportData();            //Funktion für alle erbenden Klassen
    }
}
