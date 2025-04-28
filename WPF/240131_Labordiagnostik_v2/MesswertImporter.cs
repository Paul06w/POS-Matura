using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _240131_Labordiagnostik
{
    internal class MesswertImporter
    {
        private IImportStrategy importStrategy;

        public MesswertImporter(IImportStrategy strategy)
        {
            importStrategy = strategy;
        }

        public async Task<List<Messwert>> ImportMesswerte()
        {
            return await importStrategy.ImportData();         //Aufruf der Funktion der jeweiligen Strategy
        }
    }
}
