using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labordiagnostik
{
    internal interface ISavingStrategy
    {
        public bool Save(ObservableList<Well> messwerte);
    }
}
