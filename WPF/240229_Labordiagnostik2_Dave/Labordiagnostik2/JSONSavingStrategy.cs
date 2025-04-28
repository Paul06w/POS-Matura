using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Labordiagnostik
{
    internal class JSONSavingStrategy : ISavingStrategy
    {
        public bool Save(ObservableList<Well> messwerte)
        {
            SaveFileDialog saveFileDialog = new();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";

            string fileName = "";

            if (saveFileDialog.ShowDialog() == true)
            {
                fileName = saveFileDialog.FileName;
            }
            else
            {
                return false;
            }

            File.WriteAllText(fileName, JsonSerializer.Serialize(messwerte));

            return true;
        }
    }
}
