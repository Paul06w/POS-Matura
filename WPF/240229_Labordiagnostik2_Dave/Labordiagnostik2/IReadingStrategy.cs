using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labordiagnostik
{
    internal interface IReadingStrategy
    {
        public async void Read(ObservableList<Well> wells) { wells = new ObservableList<Well>(); }
    }
}
