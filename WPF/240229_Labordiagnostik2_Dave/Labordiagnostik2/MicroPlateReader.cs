using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labordiagnostik
{
    internal class MicroPlateReader
    {
        public IReadingStrategy ReadingStrategy { get; set; }
        public ISavingStrategy SavingStrategy { get; set; }

        public MicroPlateReader(IReadingStrategy readingStrategy, ISavingStrategy savingStrategy)
        {
            this.ReadingStrategy = readingStrategy;
            this.SavingStrategy = savingStrategy;
        }

        public async void ReadMesswerte(ObservableList<Well> well)
        {
            ReadingStrategy.Read(well);
        }

        public bool SaveMesswerte(ObservableList<Well> messwerte)
        {
            return SavingStrategy.Save(messwerte);
        }
    }
}
